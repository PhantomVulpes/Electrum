using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Vulpes.Electrum.Storage.Data;

namespace Vulpes.Electrum.Storage.Mongo;

public interface IMongoProvider
{
    IMongoDatabase GetDatabase(CqrsType cqrsType);
    IMongoCollection<TCollection> GetCollection<TCollection>(CqrsType cqrsType);
    IMongoQueryable<TCollection> GetQuery<TCollection>(CqrsType cqrsType);
    Task<IEnumerable<TCollection>> ExecuteQueryAsync<TCollection>(string queryDocument);
}