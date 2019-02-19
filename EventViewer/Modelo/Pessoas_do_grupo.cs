using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Pessoas_do_grupo.
	/// </summary>
	public class Pessoas_do_grupo
	{
		public Pessoas_do_grupo()
		{
			//
			// TODO: Logica do construtor
			//
		}
		public string Contato_no_evento { get; set; }
		public int Id_do_grupo_de_participante { get; set; }
		public int Id_da_pessoa { get; set; }
	}
}