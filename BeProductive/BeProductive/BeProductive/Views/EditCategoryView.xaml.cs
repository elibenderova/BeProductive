using BeProductive.Models;
using BeProductive.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryView : ContentPage
    {
        private CategoryService _categoryService;
        private Category _category;

        public EditCategoryView(string name)
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            nameEntry.Text = name;
        }

        protected override async void OnAppearing()
        {
            _category = await _categoryService.FindCategoryByNameAsync(nameEntry.Text);
            base.OnAppearing();
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            _category.Name = nameEntry.Text;
            _category.LastModified18118003 = DateTime.Now;
            await _categoryService.UpdateCategoryAsync(_category);
            await Navigation.PopAsync();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var answear = await DisplayAlert("Delete Category", "Are you sure you want to delete this category?", "Yes", "No");

            if(answear)
            {
                await _categoryService.DeleteCategoryAsync(_category);
                await Navigation.PopAsync();
            }
        }
    }
}