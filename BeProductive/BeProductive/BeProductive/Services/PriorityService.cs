using BeProductive.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeProductive.Services
{
    public class PriorityService : IPriorityService
    {
        private static SQLiteAsyncConnection _db;

        public PriorityService()
        {
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
        }

        public Task<int> AddPriorityAsync(Priority priority)
        {
            return _db.InsertAsync(priority);
        }

        public Task<Priority> GetPriorityAsync(int id)
        {
            return _db.Table<Priority>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Priority>> GetPrioritiesAsync()
        {
            return _db.Table<Priority>().ToListAsync();
        }

        public Task<Priority> FindPriorityByLevelAsync(string level)
        {
            return _db.Table<Priority>().Where(p => p.Level == level).FirstOrDefaultAsync();
        }
    }
}
