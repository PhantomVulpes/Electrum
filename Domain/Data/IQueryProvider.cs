namespace Vulpes.Electrum.Storage.Data;

public interface IQueryProvider<TResponse>
{
    Task<IQueryable<TResponse>> BeginQueryAsync();
}