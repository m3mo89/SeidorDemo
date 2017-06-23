using System;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ACanvas = Android.Graphics.Canvas;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Frame), typeof(SeidorDemo.Droid.CustomRenderers.FrameRenderer))]
namespace SeidorDemo.Droid.CustomRenderers
{
	public class FrameRenderer : Xamarin.Forms.Platform.Android.FrameRenderer
	{
		public override void Draw(ACanvas canvas)
		{
			base.Draw(canvas);

			DrawOutline(canvas, canvas.Width, canvas.Height, 4f);
		}


		void DrawOutline(ACanvas canvas, int width, int height, float cornerRadius)
		{
			using (var paint = new Paint { AntiAlias = true })
			using (var path = new Path())
			using (Path.Direction direction = Path.Direction.Cw)
			using (Paint.Style style = Paint.Style.Stroke)
			using (var rect = new RectF(0, 0, width, height))
			{
				float rx = Forms.Context.ToPixels(cornerRadius);
				float ry = Forms.Context.ToPixels(cornerRadius);
				path.AddRoundRect(rect, rx, ry, direction);

				paint.StrokeWidth = 2f;  
				paint.SetStyle(style);
				paint.Color = Color.ParseColor("#4CAF50");
				canvas.DrawPath(path, paint);
			}
		}
	}
}
