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
    public partial class PriorityView : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private PriorityService _priorityService;

        public PriorityView()
        {
            InitializeComponent();
            _db = DependencyService.Get<ISQLiteHelper>().GetConnection();
            _priorityService = new PriorityService();
        }

        protected override async void OnAppearing()
        {
            await _db.CreateTableAsync<Priority>();

            Priority highPriority = new Priority { Level = "High" };
            Priority mediumPriority = new Priority { Level = "Medium" };
            Priority lowPriority = new Priority { Level = "Low" };

            await _priorityService.AddPriorityAsync(highPriority);
            await _priorityService.AddPriorityAsync(mediumPriority);
            await _priorityService.AddPriorityAsync(lowPriority);

            base.OnAppearing();
        }
    }
}