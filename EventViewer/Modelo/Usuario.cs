using System;
using System.Data;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Usuario.
    /// </summary>
    public class Usuario
    {
        public Usuario()
        {
            //
            // TODO: Logica do construtor
            //
        }
        public string Tipo_usuario { get; set; }
        public string Status_usuario { get; set; }
        public string Nome_usuario { get; set; }
        public int Id_usuario { get; set; }
        public string Senha { get; set; }
        public string Usuario_login { get; set; }

        public Tipo Tipo_usuari
        {
            get
            {
                if (Tipo_usuario.Equals("0"))
                {
                    return Tipo.Comum;
                }
                else
                {
                    return Tipo.Avançado;
                }
            }
            set
            {
                if (Tipo_usuario.Equals("0"))
                {
                    value = Tipo.Comum;
                }
                else
                {
                    value = Tipo.Avançado;
                }
            }
        }

        public bool Status_usuari
        {
            get
            {
                if (Status_usuario.Equals("1"))
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
                if (Status_usuario.Equals("1"))
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