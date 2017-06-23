using System;
using Xamarin.Forms;

namespace SeidorDemo.ViewModels
{
    public class DownloadProgressViewModel:BaseViewModel
    {
        private double _percentage;
        private string _message;

        public double Percentage
		{
			get 
            { 
                return _percentage; 
            }
			set
			{
				_percentage = value;
				OnPropertyChanged(nameof(Percentage));
                OnPropertyChanged(nameof(ProgressPercent));
			}
		}

		public string ProgressPercent
		{
			get 
            { 
                return string.Format("Progress {0}", Percentage.ToString("P2")); 
            }
			
		}

		public string Message
		{
			get
			{
                return _message;
			}
            set
            {
                _message = value;
				OnPropertyChanged(nameof(Message));
            }

		}

        public DownloadProgressViewModel(string url)
        {
            Title = "Downloading File";

			var file = new DownloadMessage
			{
                Url = url
			};

            Message = string.Format("Downloading file from {0}",url);

			MessagingCenter.Send(file, "Download");

            MessagingCenter.Unsubscribe<DownloadProgressMessage>(Application.Current,"DownloadProgressMessage");
			MessagingCenter.Subscribe<DownloadProgressMessage>(Application.Current, "DownloadProgressMessage", message =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Percentage = message.Percentage;
				});
			});

			MessagingCenter.Unsubscribe<DownloadFinishedMessage>(Application.Current, "DownloadFinishedMessage");
            MessagingCenter.Subscribe<DownloadFinishedMessage>(Application.Current, "DownloadFinishedMessage", message =>
			{
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var msj = string.Format("The file was saved in {0}", message.FilePath);
                    await Application.Current.MainPage.DisplayAlert("Message", msj, "OK");

                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
                });
			});
        }

       
    }
}
