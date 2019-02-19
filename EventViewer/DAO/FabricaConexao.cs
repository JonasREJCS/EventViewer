using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace EventViewer.DAO
{

    //Classe responsável para ebir e encerrar conexões com a base de dados
    class FabricaConexao
    {
        //Objeto para conexão à base de dados
        private static MySqlConnection conexao;
        //Propriedade com get para retonar o objeto de conexao
        public static MySqlConnection Conexao
        {
            get
            {
                return conexao;
            }
        }
        //Método para abrir conexão com a base de dados
        public static String AbrirConexao()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Database = "event_viewer";
            stringBuilder.UserID = "event";
            //stringBuilder.Password = "root";
            stringBuilder.Server = "localhost";
            stringBuilder.Port = 3306;
            try
            {
                conexao = new MySqlConnection(stringBuilder.ConnectionString);
                conexao.Open();
                return "Conectou porraaaaa aki é minha quebrada³.";
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }
        //Método para fechar conexão com a base de dados
        public static void FecharConexao()
        {
            try
            {
                conexao.Close();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
