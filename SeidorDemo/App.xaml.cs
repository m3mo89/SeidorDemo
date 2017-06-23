using System.Collections.Generic;
using SeidorDemo.Views;
using Xamarin.Forms;

namespace SeidorDemo
{
    public partial class App : Application
    {
        public static string BackendUrl = "http://seidormobileappservice.azurewebsites.net";

        public App()
        {
            InitializeComponent();

			DependencyService.Register<CloudDataStore>();

			var content = new DocumentsPage();

			MainPage = new NavigationPage(content);
        }
    }
}
