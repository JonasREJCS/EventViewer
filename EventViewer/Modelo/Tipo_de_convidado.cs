using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Tipo_de_convidado.
	/// </summary>
	public class Tipo_de_convidado
	{
		public Tipo_de_convidado()
		{
			//
			// TODO: Logica do construtor
			//
		}
		public int Id_tipo_de_convidado { get; set; }
		public string Descricao_tipo_convidado { get; set; }
		public string Nome_tipo_convidado { get; set; }
		public string Status_tipo_convidado { get; set; }
	}
}