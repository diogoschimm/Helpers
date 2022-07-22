using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Integrations.Apis
{
    public static class Requisicao
    {
        public static async Task<TResult> Get<TResult>(string url, Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var response = await httpClient.GetAsync(url);
            var jsonResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(jsonResult);
        }

        public static async Task<TResult> Post<TResult>(string url, string body, Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            var jsonResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(jsonResult);
        }
    }
}
