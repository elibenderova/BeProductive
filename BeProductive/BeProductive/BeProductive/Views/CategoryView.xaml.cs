using BeProductive.Models;
using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private CategoryService _categoryService;
        private ObservableCollection<Category> _category;

        public CategoryView()
        {
            InitializeComponent();
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            _categoryService = new CategoryService();
        }

        protected override async void OnAppearing()
        {
            await _db.CreateTableAsync<Category>();
            var categories = await _db.Table<Category>().ToListAsync();
            _category = new ObservableCollection<Category>(categories);
            categoriesListView.ItemsSource = _category;
            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var currentCategory = await _categoryService.FindCategoryByNameAsync(nameEntry.Text);

            if(currentCategory != null)
            {
                await DisplayAlert("Invalid Data", "This category already exists!", "OK");
                nameEntry.Text = "";
                return;
            }

            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await DisplayAlert("Invalid Data", "Please enter a category name!", "OK");
            }
            else
            {
                Category category = new Category { Name = nameEntry.Text };
                await _categoryService.AddCategoryAsync(category);

                _category.Add(category);
                categoriesListView.ItemsSource = _category;

                nameEntry.Text = "";
            }
        }

        private async void categoriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = e.SelectedItem as Category;
            var editCategoryView = new EditCategoryView(category.Name);

            await Navigation.PushAsync(editCategoryView);
        }
    }
}