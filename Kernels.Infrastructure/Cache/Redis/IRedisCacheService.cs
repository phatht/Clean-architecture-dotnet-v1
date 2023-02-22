using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernels.Infrastructure.Cache.Redis
{
    public interface IRedisCacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan expiration);
        Task<T> HashGetOrSetAsync<T>(string key, string hashField, Func<Task<T>> func);
        Task<IEnumerable<string>> GetKeysAsync(string pattern);
        Task<IEnumerable<T>> GetValuesAsync<T>(string key);
        Task<bool> RemoveAllKeysAsync(string pattern = "*");
        Task RemoveAsync(string key);
        Task ResetAsync();
    }
}
