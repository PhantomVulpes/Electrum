namespace Vulpes.Electrum.Core.Domain.Extensions;
public static class TaskExtensions
{
    public static Task<TResult> FromResult<TResult>(this TResult item) => Task.FromResult(item);
}
