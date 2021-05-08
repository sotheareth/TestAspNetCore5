using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore_Core.Interfaces;

namespace WebApplicationInfrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        private static IList<Translation> translations = null;
        public LocalizationService()
        {
            Init();
        }

        private void Init()
        {
            try
            {
                if (translations == null)
                {
                    translations = new List<Translation>();
                    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var resourcePath = Path.Combine(assemblyFolder, "Resources");
                    if (Directory.Exists(resourcePath))
                    {
                        var directory = new DirectoryInfo(resourcePath);
                        foreach (var file in directory.GetFiles("*.json"))
                        {
                            var t = new Translation()
                            {
                                Culture = Path.GetFileNameWithoutExtension(file.Name)
                            };

                            string jsonPath = Path.Combine(assemblyFolder, "Resources", file.Name);

                            using (var sr = new StreamReader(jsonPath))
                            {

                                var json = sr.ReadToEnd();
                                t.Translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                                translations.Add(t);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public async Task<string> GetTranslation(string key, string culture)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrEmpty(culture)) culture = "en_GB";
                if (translations == null) Init();

                var translation = translations.Where(x => x.Culture == culture).FirstOrDefault();

                if (translation != null)
                {
                    if (!translation.Translations.TryGetValue(key, out string value))
                    {
                        return key;
                    }
                    return value ?? key;
                }
                return key;
            }
            catch
            {
                return key;
            }
        }

        private class Translation
        {
            public string Culture { get; set; }
            public Dictionary<string, string> Translations { get; set; }
        }
    }
}
