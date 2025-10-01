using System.Runtime.CompilerServices;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net;

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
            _mapper = mapper;
        }

        // read all tasks
        [HttpGet("get_all_tasks")]
        public async Task<IActionResult> GetAll()
        {
          
            var tasks = await _taskService.GetAllTasksAsync();
            var response = _mapper.Map<List<ResponseDTO>>(tasks);
            return Ok(response);

        }

   
        [HttpGet("get_tasks_by_theme")]
        public async Task<IActionResult> GetByTheme(string themeName)
        {
            var tasks = await _taskService.GetTasksByThemeAsync(themeName);
            var response = _mapper.Map<List<ResponseDTO>>(tasks);
            return Ok(response);
        }

        
        [HttpGet("get_tasks_by_id")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            var response = _mapper.Map<ResponseDTO>(task);
            return Ok(response);
        }

       
        [HttpPost("create_new_task")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
        {
            var task = _mapper.Map<TaskEntity>(dto);
            
            var created = await _taskService.CreateTaskAsync(task, dto.ThemeName);
            if (created == null) return BadRequest("Invalid theme");
            var response = _mapper.Map<ResponseDTO>(created);
        
            return CreatedAtAction(nameof(GetById), new { id = response.Id, themeName = dto.ThemeName, isCompleted = response.IsCompleted}, response);
            
        }

        [HttpPut("update_existing_theme")]
        public async Task<IActionResult> Update(int id, string themeName, [FromBody] CreateTaskDTO dto)
        {
            var updatedTask = _mapper.Map<TaskEntity>(dto);
            var task = await _taskService.UpdateTaskAsync(id, updatedTask, themeName);
            if (task == null) return NotFound();
            var response = _mapper.Map<ResponseDTO>(task);
            return Ok(response);
        }

        [HttpDelete("delete_all_task_at_current_theme")]
        public async Task<IActionResult> Delete(string themeName)
        {
            var success = await _taskService.DeleteTaskAsync(themeName);
            if (!success) return NotFound();

            return Ok();
        }
    }
}
