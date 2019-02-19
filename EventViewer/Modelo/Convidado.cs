using System;
using System.Data;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Convidado.
    /// </summary>
    public class Convidado
    {
        public Convidado()
        {
            //
            // TODO: Logica do construtor
            //
            Tipo = new Tipo_de_convidado();
        }
        public int Id_do_tipo_de_convidado { get; set; }
        public string Nome_convidado { get; set; }
        public int Id_convidado { get; set; }
        public string Status_convidado { get; set; }
        public bool Status_convidad
        {
            get
            {
                if (Status_convidado.Equals("1"))
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
                if (Status_convidado.Equals("1"))
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
        }
        public string Descricao_convidado { get; set; }
        public Tipo_de_convidado Tipo { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Convidado outroConvidado = (Convidado)obj;
            if (!Id_convidado.Equals(outroConvidado.Id_convidado))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}