using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.App;

namespace lembrete.Droid
{
	[BroadcastReceiver]
	public class AlarmReceiver : BroadcastReceiver
	{

		public override void OnReceive(Context context, Intent intent)
		{

			var message = intent.GetStringExtra("message");
			var title = intent.GetStringExtra("title");

			var notIntent = new Intent(context, typeof(MainActivity));
			var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
			var manager = NotificationManagerCompat.From(context);

			var style = new NotificationCompat.BigTextStyle();
			style.BigText(message);

			// seto o icone da notificação
			int resourceId = Resource.Drawable.abc_btn_check_material;

			var wearableExtender = new NotificationCompat.WearableExtender()
	.SetBackground(BitmapFactory.DecodeResource(context.Resources, resourceId))
				;

			//Generate a notification with just short text and small icon
			var builder = new NotificationCompat.Builder(context)
							.SetContentIntent(contentIntent)
			                                    .SetSmallIcon(Resource.Drawable.abc_btn_check_material)
							.SetContentTitle(title)
							.SetContentText(message)
							.SetStyle(style)
							.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
							.SetAutoCancel(true)
							.Extend(wearableExtender);

			var notification = builder.Build();
			manager.Notify(0, notification);
		}
	}
}
