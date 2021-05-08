using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestAspNetCore.Helper
{
    public class HttpRequestHelper
    {
        public static async Task<string> GetAsync(string url, Dictionary<string, object> content = null)
        {
            var httpClient = new HttpClient();

            if (content == null)
                content = new Dictionary<string, object>();

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
           
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }

        }

        public static async Task<string> PostAsync(string url, Dictionary<string, object> content)
        {
            var httpClient = new HttpClient();

            if (content == null)
                content = new Dictionary<string, object>();

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            HttpContent body = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, body);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }

        }
    }
}
