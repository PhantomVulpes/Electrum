using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Vulpes.Electrum.Domain.Logging;
using Vulpes.Electrum.Storage.Data;
using Vulpes.Electrum.Storage.Models;
using Vulpes.Electrum.Domain.Extensions;
using Vulpes.Electrum.Storage.Exceptions;

namespace Vulpes.Electrum.Storage.Mongo;
public class MongoRepository<TAggregateRoot> : IModelRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    private readonly IMongoProvider mongoProvider;
    private readonly ILogger<MongoRepository<TAggregateRoot>> logger;

    public MongoRepository(IMongoProvider mongoProvider, ILogger<MongoRepository<TAggregateRoot>> logger)
    {
        this.mongoProvider = mongoProvider;
        this.logger = logger;
    }

    public async Task DeleteAsync(TAggregateRoot record)
    {
        try
        {
            var deleteResult = await mongoProvider.GetCollection<TAggregateRoot>(CqrsType.Command)
                .DeleteOneAsync(
                value => value.Key.Equals(record.Key));

            logger.LogInformation($"{ElectrumLogTags.EntityDeleted} Successfully deleted {typeof(TAggregateRoot).Name}, {record.ToLogName()}.");
        }
        catch (Exception ex)
        {
            logger.LogError($"{ElectrumLogTags.Error} Failed to delete {typeof(TAggregateRoot).Name}, {record.ToLogName()}: {ex.Message}");
        }
    }

    public Task<TAggregateRoot> GetAsync(Guid key)
    {
        var result = mongoProvider.GetQuery<TAggregateRoot>(CqrsType.Query).Where(record => record.Key.Equals(key)).FirstOrPerhaps();

        return result.ElseThrow($"Could not find object {typeof(TAggregateRoot).Name} with key {key}.").FromResult();
    }

    public async Task InsertAsync(TAggregateRoot record)
    {
        // TODO: Try catch around this.
        var collection = mongoProvider.GetCollection<TAggregateRoot>(CqrsType.Command);
        await collection.InsertOneAsync(record);

        logger.LogDebug($"{ElectrumLogTags.EntityInserted} Inserted object {typeof(TAggregateRoot).Name}, {record.ToLogName()}.");
    }

    public async Task SaveAsync(string editingToken, TAggregateRoot record)
    {
        var entityName = typeof(TAggregateRoot).Name;

        // TODO: Try catch around these and finish implementation.
        var oldEditingToken = record.EditingToken;

        try
        {
            if (string.IsNullOrEmpty(oldEditingToken))
            {
                throw new ModificationException($"Empty editing token. {entityName} {record.ToLogName()} did not have an editing token.", new() { ["entityType"] = entityName });
            }

            if (editingToken == oldEditingToken)
            {
                throw new ModificationException($"Stale editing token. {entityName} {record.ToLogName()} did not have its editing token updated with its changes.", new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = record.Key.ToString(),
                });
            }

            var saveResult = await mongoProvider
                .GetCollection<TAggregateRoot>(CqrsType.Command)
                .ReplaceOneAsync(value => value.Key == record.Key && value.EditingToken == oldEditingToken, record)
                ;

            if (saveResult.ModifiedCount <= 0)
            {
                throw new ModificationException($"{entityName} {record.ToLogName()} was modified since operations were performed on this instance.",
                new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = record.Key.ToString(),
                });
            }
            else if (!saveResult.IsAcknowledged)
            {
                throw new ModificationException($"Failed to save {entityName} {record.ToLogName()}: the operation was not acknowledged by the database.",
                new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = record.Key.ToString(),
                });
            }

            logger.LogInformation($"{ElectrumLogTags.EntityUpdated} Successfully updated {entityName}, {record.ToLogName()}.");
        }
        catch (ModificationException)
        {
            // Rethrow to allow higher-level handling or logging.
            throw;
        }
        catch (Exception ex)
        {
            throw new ModificationException(
                $"Failed to save {entityName} {record.ToLogName()}: {ex.Message}. Exception was unhandled.",
                new Dictionary<string, string>
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = record.Key.ToString(),
                    ["innerException"] = ex.Message
                });
        }
    }
}