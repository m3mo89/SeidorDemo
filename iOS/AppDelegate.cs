using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace SeidorDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		void WireUpDownloadTask()
		{
			MessagingCenter.Unsubscribe<DownloadMessage>(this, "Download");
			MessagingCenter.Subscribe<DownloadMessage>(this, "Download", async message =>
			{
				var downloader = new Downloader(message.Url);
				await downloader.DownloadFile();
			});
		}

		public static Action BackgroundSessionCompletionHandler;

		public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
		{
			Console.WriteLine("HandleEventsForBackgroundUrl(): " + sessionIdentifier);
			// We get a completion handler which we are supposed to call if our transfer is done.
			BackgroundSessionCompletionHandler = completionHandler;
		}
		

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            WireUpDownloadTask ();

            return base.FinishedLaunching(app, options);
        }
    }
}
