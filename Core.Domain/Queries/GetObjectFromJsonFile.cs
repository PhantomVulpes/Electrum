using System.Text.Json;
using Vulpes.Electrum.Core.Domain.Monads;
using Vulpes.Electrum.Core.Domain.Querying;

namespace Vulpes.Electrum.Core.Domain.Queries;
public record GetObjectFromJsonFile(Type SerializeType, string ObjectName) : Query;
public class GetObjectFromJsonFileHandler : QueryHandler<GetObjectFromJsonFile, object>
{
    protected async override Task<object> InternalRequestAsync(GetObjectFromJsonFile query)
    {
        using var streamReader = new StreamReader(query.ObjectName);
        var text = Relic<string>.Conceal(await streamReader.ReadToEndAsync()).RevealOrHoax($"Failed to retrieve data from {query.ObjectName}.");
        var result = JsonSerializer.Deserialize(text, query.SerializeType)!;

        return result;
    }
}