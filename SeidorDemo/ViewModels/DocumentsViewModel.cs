using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using SeidorDemo.Views;
using Xamarin.Forms;

namespace SeidorDemo.ViewModels
{
    public class DocumentsViewModel:BaseViewModel
    {
        public ObservableCollection<Document> Documents { get; set; }
		public Command GetDocumentsCommand { get; set; }
		public Command ShowDetailCommand { get; private set; }

        private INavigation _navigation;

        public DocumentsViewModel(INavigation navigation)
		{
            Title = "Documents";

			this._navigation = navigation;

            Documents = new ObservableCollection<Document>();
			GetDocumentsCommand = new Command(
				async () => await GetDocuments(),
				() => !IsBusy);

            ShowDetailCommand = new Command<string>(
                async (string obj) => await ShowDetail(obj));
		}

		bool isBusy;

		public bool IsBusy
		{
			get 
            { 
                return isBusy; 
            }
			set
			{
				isBusy = value;
                OnPropertyChanged(nameof(IsBusy));

				GetDocumentsCommand.ChangeCanExecute();
			}
		}

        async Task ShowDetail(string url)
		{
            await _navigation.PushModalAsync(new DownloadProgressPage(url));
		}

		async Task GetDocuments()
		{
			if (IsBusy)
				return;

			Exception error = null;
			try
			{
				IsBusy = true;

				
				var items =  await DataStore.GetItemsAsync();

                Documents.Clear();
				foreach (var item in items)
					Documents.Add(item);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: " + ex);
				error = ex;
			}
			finally
			{
				IsBusy = false;
			}

			if (error != null)
				await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
		}
    }
}
