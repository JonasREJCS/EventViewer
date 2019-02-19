using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Municipio.
	/// </summary>
	public class Municipio
	{
		public Municipio()
		{
			//
			// TODO: Logica do construtor
			//
            Estado = new Estado();
		}
		public int Id_municipio { get; set; }
		public string Status_municipio { get; set; }
		public string Nome_municipio { get; set; }
		public int Id_do_estado { get; set; }
        public Estado Estado { get; set; }

	}
}