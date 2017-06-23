using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Xamarin.Forms;

namespace SeidorDemo.Droid
{
	[Service]
	public class DownloaderService : Service
	{
		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			var url = intent.GetStringExtra("url");

			Task.Run(() =>
			{
				var fileHelper = new FileHelper();
				fileHelper.DownloadFileAsync(url)
						.ContinueWith(filePath =>
						{
							var message = new DownloadFinishedMessage
							{
								FilePath = filePath.Result
							};
							MessagingCenter.Send(message, "DownloadFinishedMessage");
						});
			});

			return StartCommandResult.Sticky;
		}	
	}
}
