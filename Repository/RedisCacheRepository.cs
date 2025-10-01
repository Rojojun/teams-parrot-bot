using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace EchoBot.Repository;

public class RedisCacheRepository(IConnectionMultiplexer redis) : ICacheRepository
{
    private readonly IDatabase _database = redis.GetDatabase();

    public void Save(string key, string value)
    {
        var existingValues = FindByKey(key);
        existingValues.Add(value);

        var serialized = JsonConvert.SerializeObject(existingValues);
        _database.StringSet(key, serialized);
    }

    public List<string> FindByKey(string key)
    {
        var value = _database.StringGet(key);

        if (value.IsNullOrEmpty)
        {
            return [];
        }

        return JsonConvert.DeserializeObject<List<string>>(value) ?? [];
    }
}
