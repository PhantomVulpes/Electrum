namespace Vulpes.Electrum.Storage.Data;

public interface IIndexDefinition
{
    string Name { get; }
    Task CreateIndexAsync();
    Task<bool> ExistsAsync();
}
