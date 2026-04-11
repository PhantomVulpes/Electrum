namespace Vulpes.Electrum.Domain.Data;

public interface IQueryProvider<TResponse>
{
    Task<IQueryable<TResponse>> BeginQueryAsync();
}