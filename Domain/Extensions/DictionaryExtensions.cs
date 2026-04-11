namespace Vulpes.Electrum.Domain.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<TKey, TValue> AddAndReturn<TKey, TValue>(this Dictionary<TKey, TValue> baseDictionary, KeyValuePair<TKey, TValue> pair)
        where TKey : notnull
    {
        baseDictionary.Add(pair);
        return baseDictionary;
    }

    public static Dictionary<TKey, TValue> AddAndReturn<TKey, TValue>(this Dictionary<TKey, TValue> baseDictionary, TKey key, TValue value)
        where TKey : notnull
        => baseDictionary.AddAndReturn(new(key, value));

    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> baseDictionary, KeyValuePair<TKey, TValue> pair)
        where TKey : notnull
        => baseDictionary.Add(pair.Key, pair.Value);

    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> baseDictionary, params (TKey, TValue)[] pairs)
        where TKey : notnull
    {
        foreach (var pair in pairs)
        {
            baseDictionary.Add(pair.Item1, pair.Item2);
        }
    }

    public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionaryEnumerable)
        where TKey : notnull
        where TValue : notnull
        => dictionaryEnumerable.ToDictionary(f => f.Key, f => f.Value);
}