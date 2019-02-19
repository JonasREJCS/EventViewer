using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Bairro.
	/// </summary>
	public class Bairro
	{
		public Bairro()
		{
			//
			// TODO: Logica do construtor
			//
            Municipio = new Municipio();
		}
		public string Status_bairro { get; set; }
		public string Nome_bairro { get; set; }
		public int Id_bairro { get; set; }
		public int Id_do_municipio { get; set; }
        public Municipio Municipio { get; set; }
	}
}