using AutoMapper;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Controllers
{
    [ApiController]
    [Route("api/v1/task")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _taskService = taskService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all existing tasks
        /// </summary>
        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _taskService.GetAllTasksAsync();
            return Ok(response);

        }

        /// <summary>
        /// Get tasks by theme
        /// </summary>
        [HttpGet("GetTask")]
        public async Task<IActionResult> GetByTheme(string themeName)
        {
            var tasks = await _taskService.GetTasksByThemeAsync(themeName);
            var response = _mapper.Map<List<ResponseDTO>>(tasks);
            return Ok(response);
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {

            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            var response = _mapper.Map<ResponseDTO>(task);
            return Ok(response);
        }

        /// <summary>
        /// Create new task for existing theme
        /// </summary>
        [HttpPost("CreateNewTask")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
        {
            var task = _mapper.Map<TaskEntity>(dto);

            var created = await _taskService.CreateTaskAsync(task, dto.ThemeName);
            if (created == null) return BadRequest("Invalid theme");

            var response = _mapper.Map<ResponseDTO>(created);
            return CreatedAtAction(nameof(GetById), new { id = response.Id, themeName = dto.ThemeName, isCompleted = response.IsCompleted }, response);

        }

        /// <summary>
        /// Update existing task and theme
        /// </summary>
        [HttpPut("UpdateExistingTheme")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTaskDTO dto)
        {
            var updatedTask = _mapper.Map<TaskEntity>(dto);
            var task = await _taskService.UpdateTaskAsync(id, updatedTask, dto.ThemeName);

            if (task == null) return NotFound();

            var response = _mapper.Map<ResponseDTO>(task);
            return Ok(response);
        }

        /// <summary>
        /// Delete all tasks that belong`s to current theme
        /// </summary>
        [HttpDelete("DeleteTasks")]
        public async Task<IActionResult> Delete(string themeName)
        {
            var success = await _taskService.DeleteTaskAsync(themeName);
            if (!success) return NotFound();

            return Ok();
        }

        /// <summary>
        /// Get all uncompleted tasks by user
        /// </summary>
        [HttpGet("GetUncompletedTasks")]
        public async Task<IActionResult> GetAllUncompleted()
        {
            var result = await _taskService.GetAllUncompletedTasksAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get task`s by priorities, available via the drop-down menu
        /// </summary>
        [HttpGet("GetTaskByPriority")]
        public async Task<IActionResult> GetByPriority([FromQuery] string priority)
        {
            var tasks = await _taskService.GetTasksByPrority(priority);
            return Ok(tasks);
        }

    }
}
