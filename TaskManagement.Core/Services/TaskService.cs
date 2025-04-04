using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return MapToDto(task);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return MapToDtos(tasks);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var task = new TaskItem
            {
                Title = createTaskDto.Title,
                IsCompleted = createTaskDto.IsCompleted
            };

            var createdTask = await _taskRepository.AddAsync(task);
            return MapToDto(createdTask);
        }

        public async Task<IEnumerable<TaskDto>> CreateTasksAsync(IEnumerable<CreateTaskDto> createTaskDtos)
        {
            var tasks = new List<TaskItem>();
            foreach (var dto in createTaskDtos)
            {
                tasks.Add(new TaskItem
                {
                    Title = dto.Title,
                    IsCompleted = dto.IsCompleted
                });
            }

            var createdTasks = await _taskRepository.AddRangeAsync(tasks);
            return MapToDtos(createdTasks);
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return null;

            task.Title = updateTaskDto.Title;
            task.IsCompleted = updateTaskDto.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);
            return MapToDto(task);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task DeleteTasksAsync(IEnumerable<Guid> ids)
        {
            await _taskRepository.DeleteRangeAsync(ids);
        }

        private TaskDto MapToDto(TaskItem task)
        {
            if (task == null)
                return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        private IEnumerable<TaskDto> MapToDtos(IEnumerable<TaskItem> tasks)
        {
            var dtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                dtos.Add(MapToDto(task));
            }
            return dtos;
        }
    }
} 