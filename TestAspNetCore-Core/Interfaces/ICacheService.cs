using System.Threading.Tasks;

namespace TestAspNetCore_Core.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key);
        Task SetCacheValueAsync(string key, string value);
    }
}
