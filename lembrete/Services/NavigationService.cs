using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace lembrete
{
	public class NavigationService : INavigationService
	{

		public async Task NavigateToFormularioLembrete(ListaLembretesViewModel listaLembretesViewModel, LembreteDAO lembrete)
		{
			try
			{
				// Abre nova view
				await Application.Current.MainPage.Navigation.PushAsync(new FormularioLembreteView(listaLembretesViewModel,null));

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return;
			}
		}

		public async Task NavigateToFormularioLembreteEditar(ListaLembretesViewModel listaLembretesViewModel, LembreteDAO lembrete)
		{
			try
			{
				// Abre view mas para editar
				await Application.Current.MainPage.Navigation.PushAsync(new FormularioLembreteView(listaLembretesViewModel,lembrete));

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return;
			}
		}

		public async Task NavigateToPopupVisualizar(LembreteDAO lembrete)
		{
			try
			{
				await Application.Current.MainPage.Navigation.PushPopupAsync(new VisualizarLembretePopupView(lembrete));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return;
			}
		}
	}
}
