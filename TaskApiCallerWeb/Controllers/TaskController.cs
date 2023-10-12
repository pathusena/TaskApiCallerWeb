using Microsoft.AspNetCore.Mvc;
using SampleTaskWeb.Models;
using SampleTaskWeb.Services;

namespace SampleTaskWeb.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskApiService _taskApiService;

        public TaskController(TaskApiService taskApiService) { 
            _taskApiService = taskApiService;
        }

        public async Task<IActionResult> Index()
        {
            if (ModelState.IsValid) {
                try
                {
                    var tasks = await _taskApiService.GetAllTasks();
                    return View(tasks);
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "API request failed. Check the API status.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel task) {
            var createdTask = await _taskApiService.SaveTask(task);
            if (createdTask != null)
            {
                return Ok(createdTask);
            }
            else {
                ModelState.AddModelError(string.Empty, "Failed to create task");
            }
            return NoContent();
        }
    }
}
