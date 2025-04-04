using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public Task<TaskItem> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_tasks.Find(t => t.Id == id));
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TaskItem>>(_tasks);
        }

        public Task<TaskItem> AddAsync(TaskItem task)
        {
            _tasks.Add(task);
            return Task.FromResult(task);
        }

        public Task<IEnumerable<TaskItem>> AddRangeAsync(IEnumerable<TaskItem> tasks)
        {
            _tasks.AddRange(tasks);
            return Task.FromResult(tasks);
        }

        public Task UpdateAsync(TaskItem task)
        {
            var existingTask = _tasks.Find(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.IsCompleted = task.IsCompleted;
                existingTask.UpdatedAt = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _tasks.RemoveAll(t => t.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            _tasks.RemoveAll(t => ids.Contains(t.Id));
            return Task.CompletedTask;
        }
    }
} 