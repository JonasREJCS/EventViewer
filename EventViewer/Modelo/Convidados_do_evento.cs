using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Convidados_do_evento.
	/// </summary>
	public class Convidados_do_evento
	{
		public Convidados_do_evento()
		{
			//
			// TODO: Logica do construtor
			//
		}
		public int Id_do_convidado { get; set; }
		public int Id_do_evento_agendado { get; set; }
	}
}