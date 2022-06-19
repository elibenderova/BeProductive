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
    public partial class AddTaskView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private TaskService _taskService;
        private CategoryService _categoryService;
        private PriorityService _priorityService;
        private List<Category> _categories;
        private List<Priority> _priorities;

        public AddTaskView()
        {
            InitializeComponent();
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            _taskService = new TaskService();
            _categoryService = new CategoryService();
            _priorityService = new PriorityService();
        }
        protected override async void OnAppearing()
        {
            _categories = await _db.Table<Category>().ToListAsync();
            categoryPicker.ItemsSource = _categories;

            _priorities = await _db.Table<Priority>().ToListAsync();
            priorityPicker.ItemsSource = _priorities;

            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || categoryPicker.SelectedItem == null || priorityPicker.SelectedItem == null)
            {
                await DisplayAlert("Invalid Data", "Please enter all fields!", "OK");
            }
            else
            {
                Task task = new Task
                {
                    Name = nameEntry.Text,
                    CategoryId = _categories[categoryPicker.SelectedIndex].Id,
                    PriorityId = _priorities[priorityPicker.SelectedIndex].Id,
                    DateAndTime = datePicker.Date + timePicker.Time,
                    NotificationAllowed = notificationSwitcher.IsToggled
                };
            
                await _taskService.AddTaskAsync(task);
                await Navigation.PopAsync();

            }
        }
    }
}
