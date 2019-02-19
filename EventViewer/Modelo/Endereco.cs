using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Endereco.
	/// </summary>
	public class Endereco
	{
		public Endereco()
		{
            Bairro = new Bairro();
		}
		public string Cep { get; set; }
		public int Id_endereco { get; set; }
		public string Logradouro { get; set; }
		public int Id_do_bairro { get; set; }
        public Bairro Bairro { get; set; }
		public string Status_endereco { get; set; }
	}
}