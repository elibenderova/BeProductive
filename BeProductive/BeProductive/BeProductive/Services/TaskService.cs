using BeProductive.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = BeProductive.Models.Task;

namespace BeProductive.Services
{
    public class TaskService : ITaskService
    {
        private static SQLiteAsyncConnection _db;

        public TaskService()
        {
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
        }

        public Task<int> AddTaskAsync(Task task)
        {
            return _db.InsertAsync(task);
        }

        public Task<int> UpdateTaskAsync(Task task)
        {
            return _db.UpdateAsync(task);
        }

        public Task<int> DeleteTaskAsync(Task task)
        {
            return _db.DeleteAsync(task);
        }

        public Task<Task> GetTaskAsync(int id)
        {
            return _db.Table<Task>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Task>> GetTasksAsync()
        {
            return _db.Table<Task>().ToListAsync();
        }
        public Task<Task> FindTaskByNameAsync(string name)
        {
            return _db.Table<Task>().Where(c => c.Name == name).FirstOrDefaultAsync();
        }
    }
}
