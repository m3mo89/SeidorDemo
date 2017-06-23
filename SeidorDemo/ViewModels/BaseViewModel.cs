using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SeidorDemo
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Document> DataStore => DependencyService.Get<IDataStore<Document>>();

        string title = string.Empty;
        public string Title
        {
            get 
            { 
                return title; 
            }
            set 
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

		public event PropertyChangedEventHandler PropertyChanged;


		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
    }
}
