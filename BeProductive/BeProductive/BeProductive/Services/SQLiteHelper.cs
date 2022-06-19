using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace BeProductive.Services
{
    public class SQLiteHelper : ISQLiteHelper
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BeProductive.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}