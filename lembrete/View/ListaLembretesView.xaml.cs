using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace lembrete
{
	public partial class ListaLembretesView : ContentPage
	{

		private ListaLembretesViewModel listaLembretesViewModel;

		public ListaLembretesView()
		{
			InitializeComponent();

			listaLembretesViewModel = new ListaLembretesViewModel();

			this.BindingContext = listaLembretesViewModel;
		}
	}
}
