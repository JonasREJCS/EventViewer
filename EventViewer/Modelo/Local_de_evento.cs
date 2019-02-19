using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Local_de_evento.
	/// </summary>
	public class Local_de_evento
	{
		public Local_de_evento()
		{
            Endereco = new Endereco();
		}
		public string Endereco_virtual { get; set; }
		public int Id_do_endereco { get; set; }
		public string Complemento_do_local { get; set; }
		public int Id_local_de_evento { get; set; }
		public string Numero_local { get; set; }
		public string Status_local_evento { get; set; }
		public string Nome_do_local { get; set; }
        public Endereco Endereco { get; set; }
        public bool Status_loca
        {
            get
            {
                if (Status_local_evento.Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (Status_local_evento.Equals("1"))
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
        }
	}
}