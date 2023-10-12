using Newtonsoft.Json;
using SampleTaskWeb.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace SampleTaskWeb.Services
{
    public class TaskApiService
    {
        private readonly HttpClient _httpClient;

        public TaskApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskModel>> GetAllTasks() {
            try {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Task");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        return JsonConvert.DeserializeObject<List<TaskModel>>(content);
                    }
                }
                return new List<TaskModel>();
            } 
            catch (HttpRequestException){
                throw new Exception("API request failed. Check the API status.");
            }
            catch (Exception ex)
            {
                throw new Exception($"API request failed: {ex.Message}", ex);
            }

        }

        public async Task<TaskModel> SaveTask(TaskModel task) {
            try {
                var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Task", content);

                if (response.IsSuccessStatusCode)
                {
                    var createdTaskContent = await response.Content.ReadAsStringAsync();
                    var createdTask = JsonConvert.DeserializeObject<TaskModel>(createdTaskContent);
                    return createdTask;
                }
                else { 
                    throw new Exception($"API request failed with status code {response.StatusCode}");
                }
            } 
            catch (HttpRequestException ex)
            {
                throw new Exception("API request failed. Check the API status.");
            }
            catch (Exception ex)
            {
                throw new Exception($"API request failed: {ex.Message}", ex);
            }

        }
    }
}
