using System;
using System.Threading.Tasks;

namespace lembrete
{
	public class AlertService : IAlertService
	{

		public async Task ShowAsync(string title, string message, string buttonOk)
		{
			await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, buttonOk);
		}

		public async Task ShowAsync(string title, string message, string buttonOk, string buttonCancel)
		{
			await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, buttonOk, buttonCancel);
		}

	
	}
}
