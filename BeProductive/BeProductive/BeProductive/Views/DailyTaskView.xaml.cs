using Android.App;
using Android.Content;
using BeProductive.Models;
using BeProductive.Services;
using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Task = BeProductive.Models.Task;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTaskView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private ObservableCollection<Task> _task;
        private List<Task> _tasks;
        private ObservableCollection<Task> _myItems;
        private TaskService _taskService;
        private CategoryService _categoryService;
        private PriorityService _priorityService;
        private bool isDescSort = false;
        private bool isSearchBtnClick = false;
        private int counter = 0;

        public DailyTaskView()
        {
            InitializeComponent();
            _taskService = new TaskService();
            _categoryService = new CategoryService();
            _priorityService = new PriorityService();
        }

        protected override async void OnAppearing()
        {
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            _tasks = await _db.Table<Task>().ToListAsync();
            _task = new ObservableCollection<Task>(_tasks);
            tasks.ItemsSource = _task;
            FillData();
            PushLocalNotification();
            base.OnAppearing();
        }

     
        private void FillData()
        {
            _myItems = _task;

            var todayDate = DateTime.Now.ToShortDateString();
            var filteredTasks = _tasks.Where(task => task.DateAndTime.ToShortDateString() == todayDate);

            dateLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            foreach (var task in _tasks)
            {
                task.Category = _categoryService.GetCategoryAsync(task.CategoryId).Result;
                task.Priority = _priorityService.GetPriorityAsync(task.PriorityId).Result;
                     
                if (!filteredTasks.Contains(task))
                {
                    _myItems.Remove(task);
                }
            }

            tasks.ItemsSource = _myItems.OrderBy(i => i.DateAndTime.Hour).ThenBy(i => i.DateAndTime.Minute);
        }

 

        private void Search_Clicked(object sender, EventArgs e)
        {
            if (isSearchBtnClick)
            {
                searchBar.IsVisible = false;
                isSearchBtnClick = false;
            }
            else
            {
                searchBar.IsVisible = true;
                isSearchBtnClick = true;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            tasks.ItemsSource = _myItems.Where(t => t.Name.StartsWith(e.NewTextValue));
        }

        private void Sort_Clicked(object sender, EventArgs e)
        {
            if (isDescSort)
            {
                tasks.ItemsSource = _myItems.OrderBy(i => i.DateAndTime.Hour).ThenBy(i => i.DateAndTime.Minute);
                isDescSort = false;
            }
            else
            {
                tasks.ItemsSource = _myItems.OrderByDescending(i => i.DateAndTime.Hour).ThenByDescending(i => i.DateAndTime.Minute);
                isDescSort = true;
            }
        }

        private async void CompleteTask_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var task = checkbox.BindingContext as Task;
            await _taskService.DeleteTaskAsync(task);
        }

        private void PushLocalNotification()
        {
            foreach (var item in _myItems)
            {
                if (item.NotificationAllowed && DateTime.Now <= item.DateAndTime)
                {
                    item.NotificationAllowed = false;

                    CrossLocalNotifications.Current.Show("Notification", $"It's time to {item.Name}", ++counter, item.DateAndTime);
                }
            }
        }
    }
 
}