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
                throw new HttpRequestException($"API call failed: {response.StatusCode}");
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(result);
        }
    }
}
