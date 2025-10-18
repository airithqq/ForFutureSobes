using AutoMapper;
using ForFutureSobes.Application.DTOs;
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Infrastructure.Interfaces;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Services
{
    public class ManageTaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public ManageTaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName) => await _taskRepository.CreateAsync(task, themeName);

        public async Task<TaskEntity> GetTaskByIdAsync(int id) => await _taskRepository.GetByIdAsync(id);


        public async Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName) => await _taskRepository.GetByThemeAsync(themeName);

        public async Task<bool> DeleteTaskAsync(string themeName)
        {
            var task = await GetTasksByThemeAsync(themeName);
            return await _taskRepository.DeleteByThemeAsync(task);
        }
        public async Task<TaskEntity> UpdateTaskAsync(int id, TaskEntity updatedTask, string updatedTheme)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.IsCompleted = updatedTask.IsCompleted;
            updatedTask.Id = id;

            task.Theme.Name = updatedTheme;

            await _taskRepository.SaveAsync();
            return updatedTask;
        }

        public async Task<List<ResponseDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<List<ResponseDTO>>(tasks);
        }

        public async Task<List<ResponseDTO>> GetAllUncompletedTasksAsync()
        {
            var tasks = await _taskRepository.GetAllUncompletedTasksAsync();
            var response = _mapper.Map<List<ResponseDTO>>(tasks);
            return response;
        }

        public async Task<List<ResponseDTO>> GetTasksByPrority(string priority)

        {
            var tasks = await _taskRepository.GetTasksByPriority(priority);
            var response = _mapper.Map<List<ResponseDTO>>(tasks);
            return response;

        }
    }
}
