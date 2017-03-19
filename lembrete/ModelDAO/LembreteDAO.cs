using System;
using System.Collections.Generic;
using Realms;

namespace lembrete
{
	public class LembreteDAO : RealmObject
	{
		[PrimaryKey,]
		public int Id { get; set; }

		public string Titulo { get; set; }

		public string Descricao { get; set; }

		public string DataLimite { get; set; }

		[Ignored]
		public string Cor { get; set; }

	}


}
