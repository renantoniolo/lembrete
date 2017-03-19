using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Plugin.LocalNotifications;
using Realms;
using Xamarin.Forms;

namespace lembrete
{
	public class FormularioLembreteViewModel : ViewModelBase
	{

		private ListaLembretesViewModel listaLembretesViewModel;
		private LembreteDAO lembreteEditar;

		public FormularioLembreteViewModel(ListaLembretesViewModel _listaLembretesViewModel, LembreteDAO _lembrete)
		{
			// recebo o objeto para atualizar atela de list depois
			listaLembretesViewModel = _listaLembretesViewModel;

			// recebemos um lembrete para editar
			lembreteEditar = _lembrete;

			this.CadastrarCommand = new Command(this.Cadastrar);

			// é para editar o lembrete ou para criar um novo?
			if (lembreteEditar != null) {

				this.ExibeValoresLembrete();
			}

		}

		private void ExibeValoresLembrete() {

			Titulo = lembreteEditar.Titulo;
			Descricao = lembreteEditar.Descricao;
		
		}

		private string titulo;
		public string Titulo
		{
			get { return titulo; }
			set
			{
				titulo = value;
				this.Notify(nameof(Titulo));
			}
		}

		private string descricao;
		public string Descricao
		{
			get { return descricao; }
			set
			{
				descricao = value;
				this.Notify(nameof(Descricao));
			}
		}

		private DateTime dataAtual;
		public DateTime DataAtual
		{
			get { return DateTime.Now; }
			set
			{
				dataAtual = value;
				this.Notify(nameof(DataAtual));
			}
		}

		private TimeSpan data;
		public TimeSpan Data
		{
			get { return data; }
			set
			{
				data = value;
				this.Notify(nameof(Data));
			}
		}

		private TimeSpan hora;
		public TimeSpan Hora
		{
			get { return hora; }
			set
			{
				hora = value;
				this.Notify(nameof(Hora));
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

		public ICommand CadastrarCommand
		{
			get;
			set;
		}

		private async void Cadastrar()
		{
			try
			{
				Load = true;

				//verifica se preencheu os campos corretamente
				if (string.IsNullOrEmpty(Titulo) || string.IsNullOrEmpty(Descricao))
				{
					await App._alertService.ShowAsync("Atenção", "Informe pelo menos um Título e uma Descrição.", "Ok");
					return;
				}

				// converto a hora timespam em datetime
				DateTime HrLimite = Convert.ToDateTime(Hora.ToString());

				// Data limite
				DateTime dataLimite = Convert.ToDateTime(Data.ToString());


				// gero a data/hora do lembrete a ser executado
				DateTime dataHoraLimite = new DateTime(dataLimite.Year,
				                                       dataLimite.Month,
				                                       dataLimite.Day,
				                                       HrLimite.Hour,
				                                       HrLimite.Minute,01);

				int idLembrete = 0;

				// instancio o realm
				var realm = Realm.GetInstance();

				var myLembreteCount = realm.All<LembreteDAO>();

				// esta populada a base de dados
				if (myLembreteCount.Count() > 0)
				{
					var lemb = realm.All<LembreteDAO>().OrderByDescending(l => l.Id).First();
					idLembrete = lemb.Id+1;
				}
				else { // primeiro cadasatro...
					idLembrete = 1;
				}

				if (lembreteEditar == null)
				{

					// vamos gravar um novo 
					realm.Write(() =>
					{
						realm.Add(new LembreteDAO
						{
							Id = idLembrete,
							Titulo = Titulo,
							Descricao = Descricao,
							DataLimite = dataHoraLimite.ToString(),
							Cor = ""
						});
					});

				}
				else { // vamos editar

					LembreteDAO lem = new LembreteDAO
					{

						Id = lembreteEditar.Id,
						Titulo = Titulo,
						Descricao = Descricao,
						DataLimite = dataHoraLimite.ToString(),
						Cor = ""
					};
					// update na base de dados
					realm.Write(() => realm.Add(lem, update: true));
				
				}

				// Registra o Lembrete para ser notificado!
				CrossLocalNotifications.Current.Show(Titulo,Descricao,idLembrete,dataHoraLimite);

				// agendo um lembrete no operacional conforme a sua plataforma
				//DependencyService.Get<IReminderService>().Remind(Convert.ToDateTime(lembrete.DataLimite),lembrete.Titulo,lembrete.Descricao);

				// exibe para a interface
				Debug.WriteLine("Adicionado um novo lembrete: " + Titulo);
				await App._alertService.ShowAsync("Informativo", "Lembrete Adicionado com sucesso", "Ok");

			}
			catch (Exception ex)
			{
				await App._alertService.ShowAsync("Atenção", "Erro ao gravar o lembrete: " + ex.Message, "Ok");
				Debug.WriteLine("Erro ao cadastrar um Lembrete: " + ex.Message);
			}
			finally {
				
				Load = false;
				// atualiza a lista de lembretes
				listaLembretesViewModel.CarregarListaLembretes();

			}

		}

	}
}
