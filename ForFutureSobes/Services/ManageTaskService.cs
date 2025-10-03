using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using AutoMapper;
using ForFutureSobes.DTOs;
using ForFutureSobes.Repository;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;

namespace ForFutureSobes.Services
{
    public class ManageTaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
       
        public ManageTaskService( IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName) => await _taskRepository.Create(task, themeName);
        
        public async Task<TaskEntity> GetTaskByIdAsync(int id) => await _taskRepository.GetById(id);
        

        public async Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName) => await _taskRepository.GetByTheme(themeName);
   
        public async Task<bool> DeleteTaskAsync(string themeName)
        {
            var task = await GetTasksByThemeAsync(themeName);
            return await _taskRepository.DeleteByTheme(task);
        }
        public async Task<TaskEntity> UpdateTaskAsync(int id, TaskEntity updatedTask, string updatedTheme)
        {
            var task = await _taskRepository.GetById(id);
            
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.IsCompleted = updatedTask.IsCompleted;
            updatedTask.Id = id;

            task.Theme.Name = updatedTheme;

            await _taskRepository.Save();
            return updatedTask;
        }

        public async Task<List<ResponseTaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAll();
            return _mapper.Map<List<ResponseTaskDTO>>(tasks);
        }
    }
}
