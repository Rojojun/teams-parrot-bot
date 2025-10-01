using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EchoBot.Repository;

public class CacheRepository : ICacheRepository
{
    readonly ConcurrentDictionary<string, List<string>> _cache;
    
    public void Save(string key, string value)
    {
        throw new System.NotImplementedException();
    }

    public List<string> FindByKey(string key)
    {
        throw new System.NotImplementedException();
    }
}