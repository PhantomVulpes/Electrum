using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Vulpes.Electrum.Domain.Logging;
using Vulpes.Electrum.Storage.Data;
using Vulpes.Electrum.Domain.Extensions;
using Vulpes.Electrum.Storage.Exceptions;
using Vulpes.Electrum.Validation.Models;

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

    public async Task InsertAsync(ValidateModel<InsertModel<TAggregateRoot>> validatedInsertModel)
    {
        var entityName = typeof(TAggregateRoot).Name;

        var model = validatedInsertModel.DataModel.Entity;

        // TODO: Try catch around this.
        try
        {
            var collection = mongoProvider.GetCollection<TAggregateRoot>(CqrsType.Command);
            await collection.InsertOneAsync(model);

            logger.LogDebug($"{ElectrumLogTags.EntityInserted} Inserted object {typeof(TAggregateRoot).Name}, {model.ToLogName()}.");
        }
        catch (MongoWriteException ex) when (ex.WriteError.Code == 11000)
        {
            // Handle duplicate key error (e.g., unique constraint violation)
            logger.LogError($"{ElectrumLogTags.Error} {ElectrumLogTags.DuplicateStorageInsert} Duplicate key error while inserting {entityName}, {model.ToLogName()}: {ex.Message}");
            throw new ModificationException($"Failed to insert {entityName} {model.ToLogName()}: Duplicate key error.", new Dictionary<string, string>
            {
                ["entityType"] = entityName,
                ["entityKey"] = model.Key.ToString(),
                ["innerException"] = ex.Message
            });
        }
        catch (Exception ex)
        {
            logger.LogError($"{ElectrumLogTags.Error} Failed to insert {entityName}, {model.ToLogName()}: {ex.Message}", ex);
        }
    }

    public async Task SaveAsync(string editingToken, ValidateModel<SaveModel<TAggregateRoot>> validatedSaveModel)
    {
        var entityName = typeof(TAggregateRoot).Name;
        var model = validatedSaveModel.DataModel.Entity;

        var oldEditingToken = model.EditingToken;

        try
        {
            if (string.IsNullOrEmpty(oldEditingToken))
            {
                throw new ModificationException($"Empty editing token. {entityName} {model.ToLogName()} did not have an editing token.", new() { ["entityType"] = entityName });
            }

            if (editingToken == oldEditingToken)
            {
                throw new ModificationException($"Stale editing token. {entityName} {model.ToLogName()} did not have its editing token updated with its changes.", new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = model.Key.ToString(),
                });
            }

            var saveResult = await mongoProvider
                .GetCollection<TAggregateRoot>(CqrsType.Command)
                .ReplaceOneAsync(value => value.Key == model.Key && value.EditingToken == oldEditingToken, model)
                ;

            if (saveResult.ModifiedCount <= 0)
            {
                throw new ModificationException($"{entityName} {model.ToLogName()} was modified since operations were performed on this instance.",
                new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = model.Key.ToString(),
                });
            }
            else if (!saveResult.IsAcknowledged)
            {
                throw new ModificationException($"Failed to save {entityName} {model.ToLogName()}: the operation was not acknowledged by the database.",
                new()
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = model.Key.ToString(),
                });
            }

            logger.LogInformation($"{ElectrumLogTags.EntityUpdated} Successfully updated {entityName}, {model.ToLogName()}.");
        }
        catch (ModificationException)
        {
            // Rethrow to allow higher-level handling or logging.
            throw;
        }
        catch (Exception ex)
        {
            throw new ModificationException(
                $"Failed to save {entityName} {model.ToLogName()}: {ex.Message}. Exception was unhandled.",
                new Dictionary<string, string>
                {
                    ["entityType"] = entityName,
                    ["entityKey"] = model.Key.ToString(),
                    ["innerException"] = ex.Message
                });
        }
    }
}