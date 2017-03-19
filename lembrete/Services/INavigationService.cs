using System;
using System.Threading.Tasks;

namespace lembrete
{
	public interface INavigationService
	{

		Task NavigateToFormularioLembrete(ListaLembretesViewModel listaLembretesViewModel,LembreteDAO lembrete);

		Task NavigateToFormularioLembreteEditar(ListaLembretesViewModel listaLembretesViewModel,LembreteDAO lembrete);

		Task NavigateToPopupVisualizar(LembreteDAO lembrete);

	}
}
