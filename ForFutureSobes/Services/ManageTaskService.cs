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
        private readonly IMapper mapper;
        private readonly ITaskRepository taskRepository;
       
        public ManageTaskService( IMapper mapper, ITaskRepository taskRepository)
        {
            this.mapper = mapper;
            this.taskRepository = taskRepository;
        }
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName) => await taskRepository.CreateAsync(task, themeName);
        
        public async Task<TaskEntity> GetTaskByIdAsync(int id) => await taskRepository.GetByIdAsync(id);
        

        public async Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName) => await taskRepository.GetByThemeAsync(themeName);
   
        public async Task<bool> DeleteTaskAsync(string themeName)
        {
            var task = await GetTasksByThemeAsync(themeName);
            return await taskRepository.DeleteByThemeAsync(task);
        }
        public async Task<TaskEntity> UpdateTaskAsync(int id, TaskEntity updatedTask, string updatedTheme)
        {
            var task = await taskRepository.GetByIdAsync(id);
            
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.IsCompleted = updatedTask.IsCompleted;
            updatedTask.Id = id;

            task.Theme.Name = updatedTheme;

            await taskRepository.SaveAsync();
            return updatedTask;
        }

        public async Task<List<ResponseDTO>> GetAllTasksAsync()
        {
            var tasks = await taskRepository.GetAllAsync();
            return mapper.Map<List<ResponseDTO>>(tasks);
        }
    }
}
