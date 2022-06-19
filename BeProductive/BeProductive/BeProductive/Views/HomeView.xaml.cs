using BeProductive.Models;
using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private List<Task> _tasks;

        public HomeView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            findCountOfTasksForTheDay();
            base.OnAppearing();
        }

        private async void findCountOfTasksForTheDay()
        {
            _tasks = await _db.Table<Task>().ToListAsync();

            var counter = 0;
            var date = DateTime.Now.ToShortDateString();

            var tasksForTheDay = _tasks.Where(task => task.DateAndTime.ToShortDateString() == date);

            foreach (var task in tasksForTheDay)
            {
                counter++;
            }

            message.Text = $"You have {counter} tasks for the day. Let's be productive together!";

        }
    }
}