using Newtonsoft.Json;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SeatManagementConsole
{
    public class APIService<T> : IAPIService<T> where T : class
    {
        private readonly string _endPoint;
        private readonly HttpClient _client;

        public APIService(string ep)
        {
            _endPoint = ep;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7224/api/");
        }
        public List<T> GetData()
        {
            var response = _client.GetAsync(_endPoint).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var getResponse = JsonConvert.DeserializeObject<List<T>>(responseContent);
                return getResponse;
            }
            else
            {
                return null;
            }
        }

        public int PostData(T data)
        {
            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PostAsync(_endPoint, content).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            if (int.TryParse(responseContent, out int res))
            {
                return res;
            }
            return 0;
        }

        public void PutData(T data)
        {
            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PostAsync(_endPoint, content).Result;

            Console.WriteLine(response);
        }
    }
}