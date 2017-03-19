using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace lembrete
{
	public partial class FormularioLembreteView : ContentPage
	{
		public FormularioLembreteView(ListaLembretesViewModel listaLembretesViewModel, LembreteDAO lembrete)
		{
			InitializeComponent();

			this.BindingContext = new FormularioLembreteViewModel(listaLembretesViewModel,lembrete);

		}
	}
}
