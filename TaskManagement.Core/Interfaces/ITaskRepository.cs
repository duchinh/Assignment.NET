using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem> AddAsync(TaskItem task);
        Task<IEnumerable<TaskItem>> AddRangeAsync(IEnumerable<TaskItem> tasks);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(Guid id);
        Task DeleteRangeAsync(IEnumerable<Guid> ids);
    }
} 