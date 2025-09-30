using System.Runtime.CompilerServices;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // 🔹 Отримати всі таски
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // 🔹 Отримати таски по темі
        [HttpGet("by-theme/{themeName}")]
        public async Task<IActionResult> GetByTheme(string themeName)
        {
            var tasks = await _taskService.GetTasksByThemeAsync(themeName);
            return Ok(tasks);
        }

        // 🔹 Отримати одну таску по ID і темі
        [HttpGet("{id}/theme/{themeName}")]
        public async Task<IActionResult> GetById(int id, string themeName)
        {
            var task = await _taskService.GetTaskByIdAsync(id, themeName);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // 🔹 Створити нову таску
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
        {
            var task = new TaskEntity
            {
                Title = dto.Title,
                IsCompleted = dto.IsCompleted
            };

            var created = await _taskService.CreateTaskAsync(task, dto.ThemeName);
            if (created == null) return BadRequest("Invalid theme");
            return CreatedAtAction(nameof(GetById), new { id = created.Id, themeName = dto.ThemeName }, created);
        }

        // 🔹 Оновити таску
        [HttpPut("{id}/theme/{themeName}")]
        public async Task<IActionResult> Update(int id, string themeName, [FromBody] CreateTaskDTO dto)
        {
            var updatedTask = new TaskEntity
            {
                Title = dto.Title,
                IsCompleted = dto.IsCompleted
            };

            var success = await _taskService.UpdateTaskAsync(id, updatedTask, themeName);
            if (!success) return NotFound();

            return NoContent();
        }

        // 🔹 Видалити таску
        [HttpDelete("{id}/theme/{themeName}")]
        public async Task<IActionResult> Delete(int id, string themeName)
        {
            var success = await _taskService.DeleteTaskAsync(id, themeName);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
