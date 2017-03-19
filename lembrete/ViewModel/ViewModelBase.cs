using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace lembrete
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected ViewModelBase()
		{
			MessagingCenter.Subscribe<ViewModelBase>(this, "OnAppearing", (sender) =>
			{
				this.OnAppearing();
			});

			MessagingCenter.Subscribe<ViewModelBase>(this, "OnDisappearing", (sender) =>
			{
				this.OnDisappearing();
			});

			MessagingCenter.Subscribe<ViewModelBase>(this, "OnLayoutChanged", (sender) =>
			{
				this.OnLayoutChanged();
			});
		}

		protected virtual void OnAppearing()
		{
		}

		protected virtual void OnDisappearing()
		{
		}

		protected virtual void OnLayoutChanged()
		{
		}
	}
}
