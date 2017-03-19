using System;
namespace lembrete
{
	public class VisualizarLembretePopupViewModel : ViewModelBase
	{

		private LembreteDAO lembrete;

		public VisualizarLembretePopupViewModel(LembreteDAO _lembrete)
		{
			// recebe o lembrete
			lembrete = _lembrete;

			// exibe os valores
			CarregaValues();

		}

		private void CarregaValues()
		{
			// recebe os valores
			Titulo = lembrete.Titulo;
			Descricao = lembrete.Descricao;
			Data = lembrete.DataLimite;

			// status do lembrete
			if (Convert.ToDateTime(lembrete.DataLimite) < DateTime.Now)
			{
				Concluido = "CONCLUIDO";
			}
			else { 
				Concluido = "A EXECUTAR";
			}

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

		private string data;
		public string Data
		{
			get { return data; }
			set
			{
				data = value;
				this.Notify(nameof(Data));
			}
		}

		private string concluido;
		public string Concluido
		{
			get { return concluido; }
			set
			{
				concluido = value;
				this.Notify(nameof(Concluido));
			}
		}



	}
}
