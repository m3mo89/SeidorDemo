using Foundation;
using UIKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeidorDemo.iOS
{
	public class Downloader
	{
		private string targetFilename = string.Empty;

        //private string sessionId = string.Empty;

		private NSUrlSession session;

		readonly string _downloadFileUrl;

		public Downloader(string downloadFileUrl)
		{
			this._downloadFileUrl = downloadFileUrl;
		}

		public async Task DownloadFile()
		{
            var fileName = _downloadFileUrl.Substring(_downloadFileUrl.LastIndexOf("/") + 1);
			targetFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            this.InitializeSession(fileName);

			var pendingTasks = await this.session.GetTasksAsync();
			if (pendingTasks != null && pendingTasks.DownloadTasks != null)
			{
				foreach (var task in pendingTasks.DownloadTasks)
				{
					task.Cancel();
				}
			}

			if (File.Exists(targetFilename))
			{
				File.Delete(targetFilename);
			}

			this.EnqueueDownload();
		}

		void InitializeSession(string sessionId)
		{
			using (var sessionConfig = UIDevice.CurrentDevice.CheckSystemVersion(8, 0)
				? NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(sessionId)
				: NSUrlSessionConfiguration.BackgroundSessionConfiguration(sessionId))
			{
				sessionConfig.AllowsCellularAccess = true;

				sessionConfig.NetworkServiceType = NSUrlRequestNetworkServiceType.Default;

				sessionConfig.HttpMaximumConnectionsPerHost = 2;

				var sessionDelegate = new CustomSessionDownloadDelegate(targetFilename);
				this.session = NSUrlSession.FromConfiguration(sessionConfig, sessionDelegate, null);
			}
		}

		void EnqueueDownload()
		{
			var downloadTask = this.session.CreateDownloadTask(NSUrl.FromString(_downloadFileUrl));

			if (downloadTask == null)
			{
				new UIAlertView(string.Empty, "Failed to create download task! Please retry.", null, "OK").Show();
				return;
			}

			downloadTask.Resume();
			Console.WriteLine("Starting download. State of task: '{0}'. ID: '{1}'", downloadTask.State, downloadTask.TaskIdentifier);
		}
	}
}
