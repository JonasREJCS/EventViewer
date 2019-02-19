using System;
using System.Data;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Evento.
    /// </summary>
    public class Evento
    {
        public Evento()
        {
            //
            // TODO: Logica do construtor
            //
        }
        public string Descricao_evento { get; set; }
        //public string Logotipo_evento { get; set; }
        public byte[] Logotipo_evento { get; set; }
        public string Nome_evento { get; set; }
        public int Id_evento { get; set; }
        public string Status_evento { get; set; }

        public bool Status_event
        {
            get
            {
                if (Status_evento.Equals("1"))
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
                if (Status_evento.Equals("1"))
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
