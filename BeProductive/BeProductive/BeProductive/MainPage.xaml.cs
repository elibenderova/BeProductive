using BeProductive.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeProductive
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new HomeView());
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as String;

            if(item == "Home")
            {
                Detail = new NavigationPage(new HomeView());
            }
            if(item == "Categories")
            {
                Detail = new NavigationPage(new CategoryView());
            }
            if(item == "Tasks")
            {
                Detail = new NavigationPage(new TaskView());
            }
            if (item == "Daily Tasks")
            {
                Detail = new NavigationPage(new DailyTaskView());
            }
            //if (item == "Priority")
            //{
            //    Detail = new NavigationPage(new PriorityView());
            //}

            IsPresented = false;
        }
    }
}
