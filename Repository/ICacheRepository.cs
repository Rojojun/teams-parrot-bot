using System;
using System.Collections.Generic;

namespace EchoBot.Repository;

public interface ICacheRepository
{
    void Save(string key, string value);
    
    List<string> FindByKey(string key);
}