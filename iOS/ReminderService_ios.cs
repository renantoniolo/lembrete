using System;
using Foundation;
using lembrete.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ReminderService_ios))]
namespace lembrete.iOS
{
	public class ReminderService_ios : IReminderService
	{
		

		public void Remind(DateTime dateTime, string title, string message)
		{
			
			var notification = new UILocalNotification();

			// converte para nsdata o datetime
			NSDate nsDate = (NSDate)DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

			//---- set the fire date (the date time in which it will fire)
			notification.FireDate = nsDate;

			//---- configure the alert stuff
			notification.AlertAction = title;
			notification.AlertBody = message;

			//---- modify the badge
			notification.ApplicationIconBadgeNumber = 1;

			//---- set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			//---- schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);

		}

	}
}
