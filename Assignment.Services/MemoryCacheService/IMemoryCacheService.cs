using Assignment.Dto.Dto;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.MemoryCacheServices
{
    public interface IMemoryCacheService
    {
        bool TryGetValue<T>(string Key, out T cache);
        MemoryCacheResultDto Set<T>(string key, T cache, MemoryCacheEntryOptions expirtaion);
    }
}
