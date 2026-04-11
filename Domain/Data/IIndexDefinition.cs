namespace Vulpes.Electrum.Domain.Data;

public interface IIndexDefinition
{
    string Name { get; }
    Task CreateIndexAsync();
    Task<bool> ExistsAsync();
}
