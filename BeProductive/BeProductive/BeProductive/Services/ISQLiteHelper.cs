using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeProductive.Services
{
    public interface ISQLiteHelper
    {
        SQLiteAsyncConnection GetConnection();
    }
}
