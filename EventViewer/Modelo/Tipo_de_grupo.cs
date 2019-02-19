using System;
using System.Data;

namespace EventViewer.Modelo
{
	/// <summary>
	/// Classe gerada para a tabela : Tipo_de_grupo.
	/// </summary>
	public class Tipo_de_grupo
	{
		public Tipo_de_grupo()
		{
			//
			// TODO: Logica do construtor
			//
		}
		public int Id_tipo_de_grupo { get; set; }
		public string Status_tipo_de_grupo { get; set; }
		public string Descricao_tipo_de_grupo { get; set; }
		public string Nome_tipo_de_grupo { get; set; }

        public bool Status_tipo_de_grup
        {
            get
            {
                if (Status_tipo_de_grup.Equals("1"))
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
                if (Status_tipo_de_grup.Equals("1"))
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