using System.Net.Http.Headers;
using BowlingMVC.Servicelayer.Interfaces;
using Newtonsoft.Json;

namespace BowlingMVC.Servicelayer
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://localhost:7197/api/";

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get data from API, status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<List<T>>(responseContent);

            return responseData;
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var requestData = new StringContent(JsonConvert.SerializeObject(data));
            requestData.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(endpoint, requestData);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to post data to API, status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            
            var responseData = JsonConvert.DeserializeObject<T>(responseContent);

            return responseData;
        }

        // Other methods for PUT, DELETE, etc. can be added as needed.
    }
}
