using System.Threading.Tasks;

namespace TestAspNetCore_Core.Interfaces
{
    public interface ILocalizationService
    {
        Task<string> GetTranslation(string key, string culture);
    }
}
