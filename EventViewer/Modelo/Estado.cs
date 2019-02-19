using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Estado.
	/// </summary>
	public class Estado
	{
		public Estado()
		{
			//
			// TODO: Logica do construtor
			//
            Pais = new Pais();
		}
		public int Id_do_pais { get; set; }
        public Pais Pais { get; set; }
		public string Status_estado { get; set; }
		public int Id_estado { get; set; }
		public string Nome_estado { get; set; }
	}
}