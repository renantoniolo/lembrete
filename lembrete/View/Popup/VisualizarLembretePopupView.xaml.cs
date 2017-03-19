using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace lembrete
{
	public partial class VisualizarLembretePopupView : PopupPage
	{
		public VisualizarLembretePopupView(LembreteDAO lembrete)
		{
			InitializeComponent();

			BindingContext = new VisualizarLembretePopupViewModel(lembrete);

		}

		private void OnClose(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync();
		}


	}

}

