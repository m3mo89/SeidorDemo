using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Util;
using Xamarin.Forms;

namespace SeidorDemo.Droid
{
	public class FileHelper
	{
		private string Path
		{
			get
			{
				string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				return baseDir;
			}
		}

		public Task<string> DownloadFileAsync(string url)
		{
			return Task.Run(() => DownloadFile(url));
		}

		private async Task<string> DownloadFile(string url)
		{
			string path = string.Empty;

			try
			{
				var fileName = GetFileName(url);
				path = System.IO.Path.Combine(Path, fileName);

				int totalBytes = GetFileSize(url);

				using (var fileUrl = new Java.Net.URL(url))
				{
					using (var stream = fileUrl.OpenStream())
					{
						byte[] bytes = new byte[totalBytes];
						int numBytesToRead = totalBytes;
						int numBytesRead = 0;

						while (numBytesToRead > 0)
						{
							// Read may return anything from 0 to numBytesToRead.
							int n = await stream.ReadAsync(bytes, numBytesRead, numBytesToRead);

							// Break when the end of the file is reached.
							if (n == 0)
							{
								//await Task.Yield();
								break;
							}

							numBytesRead += n;
							numBytesToRead -= n;

							float percentage = (float)numBytesRead / (float)totalBytes;

							var message = new DownloadProgressMessage()
							{
								Percentage = percentage
							};

							MessagingCenter.Send<DownloadProgressMessage>(message, "DownloadProgressMessage");
						}

						File.WriteAllBytes(path, bytes);
					}
				}
			}
			catch (Exception ex)
			{
				Log.WriteLine(LogPriority.Error, "GetImageFromBitmap Error", ex.Message);
			}

			return path;
		}

		private string GetFileName(string url)
		{
			string[] parts = url.Split('/');
			string fileName = "";

			if (parts.Length > 0)
				fileName = parts[parts.Length - 1];
			else
				fileName = url;

			return fileName;
		}

		private int GetFileSize(string fileUrl)
		{
			using (var url = new Java.Net.URL(fileUrl))
			{
				Java.Net.HttpURLConnection conn = null;
				try
				{
					conn = (Java.Net.HttpURLConnection)url.OpenConnection();
					conn.RequestMethod = "HEAD";

					return conn.ContentLength;
				}
				catch (Java.IO.IOException e)
				{
					return -1;
				}
				finally
				{
					conn.Disconnect();
				}
			}
		}
	}
}
