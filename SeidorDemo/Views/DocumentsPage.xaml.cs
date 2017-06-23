using System;
using System.Collections.Generic;
using SeidorDemo.ViewModels;
using Xamarin.Forms;

namespace SeidorDemo.Views
{
    public partial class DocumentsPage : ContentPage
    {
		DocumentsViewModel vm;

		public DocumentsPage()
		{
			InitializeComponent();
			vm = new DocumentsViewModel(this.Navigation);
			BindingContext = vm;
		}	
    }
}
