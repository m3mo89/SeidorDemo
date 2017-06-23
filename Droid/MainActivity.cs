using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace SeidorDemo.Droid
{
    [Activity(Label = "SeidorDemo", Icon = "@drawable/ic_launcher",  Theme = "@style/MyTheme", MainLauncher = false,LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            WireUpLongDownloadTask ();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

		void WireUpLongDownloadTask()
		{
            MessagingCenter.Unsubscribe<DownloadMessage>(this, "Download");
			MessagingCenter.Subscribe<DownloadMessage>(this, "Download", message =>
			{
				var intent = new Intent(this, typeof(DownloaderService));
				intent.PutExtra("url", message.Url);
				StartService(intent);
			});
		}
    }
}
