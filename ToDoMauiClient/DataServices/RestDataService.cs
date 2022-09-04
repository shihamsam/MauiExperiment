using System.Text;
using System.Text.Json;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:7149" : "https://localhost:7149";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddTodoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("==> No internet access");
                return;
            }

            try
            {
                var jsonTodo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, encoding: Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Succssfully Created Todo");
                }
                else
                {
                    Console.WriteLine("==> Nn Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops: {ex.Message}");
                return;
            }
        }

        public async Task DeleteTodoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("==> No internet access");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Succssfully Created Todo");
                }
                else
                {
                    Console.WriteLine("==> Nn Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops: {ex.Message}");
                return;
            }
        }

        public async Task<List<ToDo>> GetAllToDosAsync()
        {
            List<ToDo> toDos = new();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("==> No internet access");
                return toDos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    toDos = JsonSerializer.Deserialize<List<ToDo>>(json, _jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("==> Nn Http 2xx response");
                }
                return toDos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops: {ex.Message}");
                return toDos;
            }
        }

        public async Task UpdateDoto(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("==> No internet access");
                return;
            }

            try
            {
                var jsonTodo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, encoding: Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{toDo.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Succssfully Updated Todo");
                }
                else
                {
                    Console.WriteLine("==> Nn Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops: {ex.Message}");
                return;
            }
        }
    }
}