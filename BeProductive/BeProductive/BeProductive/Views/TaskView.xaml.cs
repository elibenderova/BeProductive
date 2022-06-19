using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BeProductive.Models.Task;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BeProductive.Models;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private ObservableCollection<Task> _task;
        private List<Task> _tasks;
        private ObservableCollection<Task> _myItems;

        public TaskView()
        {
            InitializeComponent();
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _db.CreateTableAsync<Task>();

            _tasks = await _db.Table<Task>().ToListAsync();
            _task = new ObservableCollection<Task>(_tasks);
            tasksListView.ItemsSource = _task;
            
            base.OnAppearing();
        }


        private async void tasksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var task = e.SelectedItem as Task;
            var editTaskView = new EditTaskView(task.Name, task.CategoryId, task.PriorityId, task.DateAndTime, task.NotificationAllowed);

            await Navigation.PushAsync(editTaskView);
        }

        private async void AddTask_Clicked(object sender, EventArgs e)
        {
            var addTaskView = new AddTaskView();
            await Navigation.PushAsync(addTaskView);
        }

        private void ResizeCalendar_Clicked(object sender, EventArgs e)
        {
            if (calendar.AutoRows)
            {
                calendar.AutoRows = false;
                calendar.Rows = 1;
            }
            else
            {
                calendar.AutoRows = true;
            }
        }

        private void CalendarView_DateSelectionChanged(object sender, XCalendar.Models.DateSelectionChangedEventArgs e)
        {
            if (calendar.SelectionType == XCalendar.Enums.SelectionType.Single)
            {
                FillData();

                calendar.SelectionType = XCalendar.Enums.SelectionType.None;
                calendar.SelectedDates.RemoveAt(0);
            }
            else
            {
                calendar.SelectionType = XCalendar.Enums.SelectionType.Single;
            }
        }

        private void FillData()
        {
            _myItems = new ObservableCollection<Task>(_tasks);

            var selectedDate = calendar.SelectedDates[0].ToShortDateString();
            var filteredTasks = _tasks.Where(task => task.DateAndTime.ToShortDateString() == selectedDate);

            foreach (var task in _tasks)
            {
                if (!filteredTasks.Contains(task))
                {
                    _myItems.Remove(task);
                }
            }

            tasksListView.ItemsSource = _myItems;

            if (_myItems.Count == 0)
            {
                taskLabel.Text = $"Woohoo! You don't have any tasks for {calendar.SelectedDates[0].ToShortDateString()}. Enjoy your day!";
            }
            else
            {
                taskLabel.Text = "Tasks for " + calendar.SelectedDates[0].ToShortDateString();
            }

        }
    }
}