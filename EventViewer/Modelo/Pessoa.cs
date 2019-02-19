using System;
using System.Data;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Pessoa.
    /// </summary>
    public class Pessoa
    {
        public Pessoa()
        {
            //
            // TODO: Logica do construtor
            //
            Endereco = new Endereco();
        }
        public byte[] Caminho_foto { get; set; }
        public string Status_pessoa { get; set; }
        public string Cpf { get; set; }
        public string Telefone_residencial { get; set; }
        public string Complemento_residencia { get; set; }
        public int Id_do_usuario { get; set; }
        public string Telefone_movel { get; set; }
        public string Nome_pessoa
        {
            get
            ;
            set
            ;
        }
        public int Id_do_endereco { get; set; }
        public int Id_pessoa { get; set; }
        public string Numero_residencia { get; set; }
        public string Identidade { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
        public bool Status_pesso
        {
            get
            {
                if (Status_pessoa.Equals("1"))
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
                if (Status_pessoa.Equals("1"))
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }

        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Pessoa outraPessoa = (Pessoa)obj;

            if (!Id_pessoa.Equals(outraPessoa.Id_pessoa))
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
            // TODO: write your implementation of GetHashCode() here
               return base.GetHashCode();
        }
    }
}