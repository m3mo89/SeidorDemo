using System;
using System.Collections.Generic;
using SeidorDemo.ViewModels;
using Xamarin.Forms;

namespace SeidorDemo.Views
{
    public partial class DownloadProgressPage : ContentPage
    {
        DownloadProgressViewModel vm;

        public DownloadProgressPage(string url)
        {
            InitializeComponent();
            vm = new DownloadProgressViewModel(url);
            BindingContext = vm;
        }


    }
}
