using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;

namespace TaskManagement.Core.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<IEnumerable<TaskDto>> CreateTasksAsync(IEnumerable<CreateTaskDto> createTaskDtos);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto);
        Task DeleteTaskAsync(Guid id);
        Task DeleteTasksAsync(IEnumerable<Guid> ids);
    }
} 