using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestAspNetCore_XUnitTest
{
    public static class ApiTestBuilder
    {
        public static async Task<T> GetRequest<T>(this HttpClient client, string uri)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
            catch (Exception)
            {

            }

            return await Task.FromResult(default(T));
        }

        public static async Task<TOut> PostRequest<TIn, TOut>(this HttpClient client, string uri, TIn content)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
            catch (Exception)
            {
            }

            return await Task.FromResult(default(TOut));
        }

        public static HttpRequestMessage BuildPostRequest<TIn>(this HttpClient client, string uri, TIn content)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            };
            return request;
        }
        public static async Task<TOut> PutRequest<TIn, TOut>(this HttpClient client, string uri, TIn content)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync(uri, serialized))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
            catch (Exception)
            {
            }

            return await Task.FromResult(default(TOut));
        }
        public static HttpRequestMessage BuildPutRequest<TIn>(this HttpClient client, string uri, TIn content)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            };
            return request;
        }

        public static async Task<T> DeleteRequest<T>(this HttpClient client, string uri)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.DeleteAsync(uri))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
            catch (Exception)
            {

            }

            return await Task.FromResult(default(T));
        }


    }
}