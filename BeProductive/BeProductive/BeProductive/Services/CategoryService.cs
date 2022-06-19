using BeProductive.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeProductive.Services
{
    public class CategoryService : ICategoryService
    {
        private static SQLiteAsyncConnection _db;

        public CategoryService()
        {
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
        }

        public Task<int> AddCategoryAsync(Category category)
        {
            return _db.InsertAsync(category);
        }

        public Task<int> UpdateCategoryAsync(Category category)
        {
            return _db.UpdateAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return _db.DeleteAsync(category);
        }

        public Task<Category> GetCategoryAsync(int id)
        {
            return _db.Table<Category>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return _db.Table<Category>().ToListAsync();
        }
        public Task<Category> FindCategoryByNameAsync(string name)
        {
            return _db.Table<Category>().Where(c => c.Name == name).FirstOrDefaultAsync();
        }
    }
}
