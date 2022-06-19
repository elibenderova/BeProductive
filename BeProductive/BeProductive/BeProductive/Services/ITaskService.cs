using BeProductive.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task = BeProductive.Models.Task;

namespace BeProductive.Services
{
    public interface ITaskService
    {
        Task<int> AddTaskAsync(Task task);
        Task<int> UpdateTaskAsync(Task task);
        Task<int> DeleteTaskAsync(Task task);
        Task<Task> GetTaskAsync(int id);
        Task<List<Task>> GetTasksAsync();
        Task<Task> FindTaskByNameAsync(string name);
    }
}
