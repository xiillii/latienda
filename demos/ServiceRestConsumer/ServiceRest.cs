using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceRestConsumer
{
    public class ServiceRest
    {
        private readonly string _baseUri;

        public ServiceRest(string baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request, string action, HttpMethod verb)
            where TRequest : class where TResponse : class, new()
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var body = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            string jsonApiResponse;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiResponse = await client.SendAsync(new HttpRequestMessage(verb, action)
                {
                    Content = body
                });
                apiResponse.EnsureSuccessStatusCode();

                jsonApiResponse = await apiResponse.Content.ReadAsStringAsync();
            }

            var response = JsonConvert.DeserializeObject<TResponse>(jsonApiResponse);

            return response;
        }

        public async Task<TResponse> Execute<TResponse>(string action, HttpMethod verb) where TResponse : class, new()
        {                       
            string jsonApiResponse;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiResponse = await client.SendAsync(new HttpRequestMessage(verb, action));
                apiResponse.EnsureSuccessStatusCode();

                jsonApiResponse = await apiResponse.Content.ReadAsStringAsync();
            }

            var response = JsonConvert.DeserializeObject<TResponse>(jsonApiResponse);

            return response;
        }
    }
}