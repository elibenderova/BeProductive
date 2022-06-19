using BeProductive.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeProductive.Services
{
    public interface IPriorityService
    {
        Task<int> AddPriorityAsync(Priority priority);
        Task<Priority> GetPriorityAsync(int id);
        Task<List<Priority>> GetPrioritiesAsync();
        Task<Priority> FindPriorityByLevelAsync(string level);
    }
}
