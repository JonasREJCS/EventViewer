using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EventViewer.Modelo;

namespace EventViewer.DAO
{
    /// <summary>
    /// Classe gerada para a tabela : Endereco.
    /// </summary>
    class DAOEndereco
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOEndereco(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        //método para buscar itens de endereços já registrados
        public List<Endereco> BuscarRegistro(string whereClause)
        {
            List<Endereco> listaDeEndereco = new List<Endereco>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            string SQL = "Select * From Endereco where " + whereClause + " LIMIT 1000 ";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Endereco Endereco = new Endereco();
                    Endereco.Cep = (string)leitor["cep"];
                    Endereco.Id_endereco = (int)leitor["id_endereco"];
                    Endereco.Logradouro = (string)leitor["logradouro"];
                    Endereco.Id_do_bairro = (int)leitor["id_do_bairro"];
                    Endereco.Status_endereco = (string)leitor["status_endereco"];
                    listaDeEndereco.Add(Endereco);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDeEndereco;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para salvar novos registros de itens de endereços
        public long Inserir(Endereco endereco)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Endereco (cep, id_endereco, logradouro, id_do_bairro, status_endereco) values(@cep, @id_endereco, @logradouro, @id_do_bairro, @status_endereco)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@cep", endereco.Cep);
                comando.Parameters.AddWithValue("@id_endereco", endereco.Id_endereco);
                comando.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                comando.Parameters.AddWithValue("@id_do_bairro", endereco.Id_do_bairro);
                comando.Parameters.AddWithValue("@status_endereco", endereco.Status_endereco);
                comando.ExecuteNonQuery();
                long ultimoID = comando.LastInsertedId;
                comando.Dispose();
                return ultimoID;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para modificar itens de endereços já registrados
        public void Atualiza(Endereco endereco, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Endereco set cep = @cep, id_endereco = @id_endereco, logradouro = @logradouro, id_do_bairro = @id_do_bairro, status_endereco = @status_endereco where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@cep", endereco.Cep);
                comando.Parameters.AddWithValue("@id_endereco", endereco.Id_endereco);
                comando.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                comando.Parameters.AddWithValue("@id_do_bairro", endereco.Id_do_bairro);
                comando.Parameters.AddWithValue("@status_endereco", endereco.Status_endereco);
                comando.ExecuteNonQuery();
                
                conexao.Close();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para excluir itens de endereços registrados
        public void Excluir(string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string sql = "Delete From Endereco where " + condicao + "";
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
                comando.Dispose();
                conexao.Close();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        
        public List<Endereco> BuscarEnderecoCompleto(string condicao)
        {
            List<Endereco> listaDeEndereco = new List<Endereco>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            string SQL = "Select * From view_Endereco where " + condicao + " LIMIT 1000 ";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Endereco Endereco = new Endereco()
                    {
                        Cep = (string)leitor["cep"],
                        Id_endereco = (int)leitor["id_endereco"],
                        Logradouro = (string)leitor["logradouro"],
                        Status_endereco = (string)leitor["status_endereco"],
                        Id_do_bairro = (int)leitor["id_do_bairro"],

                        Bairro = new Bairro()
                        {
                            Id_bairro = (int)leitor["id_bairro"],
                            Nome_bairro = (string)leitor["nome_bairro"],
                            Status_bairro = (string)leitor["status_bairro"],
                            Id_do_municipio = (int)leitor["id_do_municipio"],

                            Municipio = new Municipio()
                            {
                                Id_municipio = (int)leitor["id_municipio"],
                                Nome_municipio = (string)leitor["nome_municipio"],
                                Status_municipio = (string)leitor["status_municipio"],
                                Id_do_estado = (int)leitor["id_do_estado"],

                                Estado = new Estado()
                                {

                                    Id_estado = (int)leitor["id_estado"],
                                    Nome_estado = (string)leitor["nome_estado"],
                                    Status_estado = (string)leitor["status_estado"],
                                    Id_do_pais = (int)leitor["id_do_pais"],

                                    Pais = new Pais()
                                    {
                                        Id_pais = (int)leitor["id_pais"],
                                        Nome_pais = (string)leitor["nome_pais"],
                                        Status_pais = (string)leitor["status_pais"]
                                    }
                                }
                            }
                        }
                    };


                    listaDeEndereco.Add(Endereco);

                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDeEndereco;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}