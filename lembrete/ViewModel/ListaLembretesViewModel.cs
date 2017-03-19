using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.LocalNotifications;
using Realms;
using Xamarin.Forms;

namespace lembrete
{
	public class ListaLembretesViewModel : ViewModelBase
	{


		public ListaLembretesViewModel()
		{
			
			// adicionar command
			this.AdicionarLembreteCommand = new Command(this.AdicionarLembrete);
			this.EditarLembreteCommand = new Command<LembreteDAO>(this.EditarLembrete);
			this.DeletarLembreteCommand = new Command<LembreteDAO>(this.DeletarLembrete);

			// vamos carregar a lista de lembretes
			this.CarregarListaLembretes();

			// verifico toda hr os lembretes
			VerificaStatusLembretes();

		}

		public async void CarregarListaLembretes()
		{
			try
			{
				Load = true;

				ListaLembretes = new ObservableCollection<LembreteDAO>();

				var realm = Realm.GetInstance();

				var myUsersCount = realm.All<LembreteDAO>().OrderByDescending(a => a.Id);

				if (myUsersCount.Count() > 0)
				{

					var AllLembrete = realm.All<LembreteDAO>();

					foreach (LembreteDAO lembrete in AllLembrete)
					{
						Debug.WriteLine("Titulo: " + lembrete.Titulo);
						lembrete.Cor = "#F44336";

						// caso esteja vencido, pinta de vermelho
						if (Convert.ToDateTime(lembrete.DataLimite) > DateTime.Now)
						{
							// Atualiza o lembrete para finalizado
							lembrete.Cor = "#3F51B5";

						}

						// adiciono o lembre a lista a ser exibida
						ListaLembretes.Add(lembrete);

					}

				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine("Erro ao carregar a lista: " +  ex.ToString());
			}
			finally {
				
				Load = false;
			}

		}

		private async void VerificaStatusLembretes()
		{

			bool verifica = true;
			int condicao = 0;

			while (verifica)
			{

				switch (condicao)
				{

					case 0:

						// aguardo 1 minutos
						await Task.Delay(60000);
						condicao++;

						break;

					case 1:
						// carrego a lista e verifico
						this.CarregarListaLembretes();
						condicao = 0;
						break;

				}
			}

		}


		private bool load;
		public bool Load
		{

			get { return load; }
			set
			{
				load = value;
				this.Notify(nameof(Load));
			}
		}


		private LembreteDAO selectedItem;
		public LembreteDAO SelectedItem
		{

			get { return selectedItem; }
			set
			{
				selectedItem = value; // pego item selecionado
				App._navigationService.NavigateToPopupVisualizar(selectedItem);
				this.Notify(nameof(SelectedItem));
				selectedItem = null; // desselct o item da lista
			}
		}


		private ObservableCollection<LembreteDAO> listaLembretes;
		public ObservableCollection<LembreteDAO> ListaLembretes
		{
			get { return listaLembretes; }
			set
			{
				listaLembretes = value;
				this.Notify(nameof(ListaLembretes));
			}
		}


		public ICommand AdicionarLembreteCommand
		{
			get;
			set;
		}

		private async void AdicionarLembrete()
		{

			await App._navigationService.NavigateToFormularioLembrete(this,null);

		}

		public ICommand EditarLembreteCommand
		{
			get;
			set;
		}

		private async void EditarLembrete(LembreteDAO lembreteSelecionado)
		{
			Debug.WriteLine("Editar: " + lembreteSelecionado.Titulo);

			// Vamos editar
			await App._navigationService.NavigateToFormularioLembreteEditar(this,lembreteSelecionado);

		}

		public ICommand DeletarLembreteCommand
		{
			get;
			set;
		}

		private async void DeletarLembrete(LembreteDAO lembreteSelecionado)
		{
			Debug.WriteLine("Deletar: " + lembreteSelecionado.Titulo);

			var realm = Realm.GetInstance();

			var lembreteDel = realm.All<LembreteDAO>().First(b => b.Titulo == lembreteSelecionado.Titulo);

			// Delete an object with a transaction
			using (var trans = realm.BeginWrite())
			{
				realm.Remove(lembreteDel);
				trans.Commit();
			}

			// atualzia a lista
			this.CarregarListaLembretes();

		}


	}
}
