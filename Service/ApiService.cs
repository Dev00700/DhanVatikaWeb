using Newtonsoft.Json;
using System.Text;

namespace DhanVatikaWeb.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<TResponse> SendAsync<TRequest, TResponse>(string url, TRequest requestObj, string method = "POST")
        {
            HttpResponseMessage response;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

            if (method.ToUpper() == "GET")
            {
                response = await _httpClient.GetAsync(url);
            }
            else
            {
                string json = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(url, content);
            }

            if (!response.IsSuccessStatusCode)
            {
                // Optionally: log error here
                return default(TResponse); // Return default value instead of throwing exception
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(result);
        }
    }
}
