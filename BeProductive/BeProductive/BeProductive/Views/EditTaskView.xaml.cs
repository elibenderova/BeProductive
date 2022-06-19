using BeProductive.Models;
using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private TaskService _taskService;
        private Task _task;

        public EditTaskView(string name, int categoryId, int priorityId, DateTime dateAndTime, bool notificationAllowed)
        {
            InitializeComponent();
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            _taskService = new TaskService();

            FillCategoryPicker(categoryId);
            FillPriorityPicker(priorityId);

            nameEntry.Text = name;
            datePicker.Date = dateAndTime.Date;
            timePicker.Time = dateAndTime.TimeOfDay;
            notificationSwitcher.IsToggled = notificationAllowed;
        }

        async void FillCategoryPicker(int categoryId)
        {
            var categories = await _db.Table<Category>().ToListAsync();
            categoryPicker.ItemsSource = categories;
            categoryPicker.SelectedIndex = categoryId - 1;
        }

        async void FillPriorityPicker(int priorityId)
        {
            var priorities = await _db.Table<Priority>().ToListAsync();
            priorityPicker.ItemsSource = priorities;
            priorityPicker.SelectedIndex = priorityId - 1;
        }

        protected override async void OnAppearing()
        {
            _task = await _taskService.FindTaskByNameAsync(nameEntry.Text);

            base.OnAppearing();
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            _task.Name = nameEntry.Text;
            _task.CategoryId = categoryPicker.SelectedIndex + 1;
            _task.PriorityId = priorityPicker.SelectedIndex + 1;
            _task.DateAndTime = datePicker.Date + timePicker.Time;
            _task.NotificationAllowed = notificationSwitcher.IsToggled;

            _task.LastModified18118003 = DateTime.Now;

            await _taskService.UpdateTaskAsync(_task);
            await Navigation.PopAsync(); 
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var answear = await DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No");

            if (answear)
            {
                await _taskService.DeleteTaskAsync(_task);
                await Navigation.PopAsync();
            }
        }
    }
}