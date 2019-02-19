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
    /// Classe gerada para a tabela : Local_de_evento.
    /// </summary>
    class DAOLocal_de_evento
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOLocal_de_evento(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        public List<Local_de_evento> BuscarRegistro(string whereClause)
        {
            List<Local_de_evento> listaDeLocal_de_evento = new List<Local_de_evento>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            //string SQL = "Select * From Local_de_evento where " + whereClause + " LIMIT 1000 ";
            string SQL = "select * from view_local_de_evento_completo where " + whereClause + " LIMIT 1000";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Local_de_evento Local_de_evento = new Local_de_evento();
                    Local_de_evento.Endereco_virtual = leitor["endereco_virtual"].ToString();
                    if (leitor["id_do_endereco"] is DBNull)
                    {
                        Local_de_evento.Id_do_endereco = 0;
                    }
                    else
                    {
                        Local_de_evento.Id_do_endereco = (int)leitor["id_do_endereco"];
                    }
                    Local_de_evento.Complemento_do_local = leitor["complemento_do_local"].ToString();
                    if (leitor["id_local_de_evento"] is DBNull)
                    {
                        Local_de_evento.Id_local_de_evento = 0;
                    }
                    else
                    {
                        Local_de_evento.Id_local_de_evento = (int)leitor["id_local_de_evento"];
                    }

                    Local_de_evento.Endereco.Cep = leitor["cep"].ToString();

                    if ((leitor["id_endereco"] is DBNull) == false)
                    {
                        Local_de_evento.Endereco.Id_endereco = (int)leitor["id_endereco"];

                    }

                    Local_de_evento.Endereco.Logradouro = leitor["logradouro"].ToString();
                    Local_de_evento.Endereco.Status_endereco = leitor["status_endereco"].ToString();

                    if ((leitor["id_do_endereco"] is DBNull) == false)
                    {

                        Local_de_evento.Endereco.Id_endereco = (int)leitor["id_do_endereco"];
                    }

                    if ((leitor["id_bairro"] is DBNull) == false)
                    {

                        Local_de_evento.Endereco.Bairro.Id_bairro = (int)leitor["id_bairro"];
                    }

                    Local_de_evento.Endereco.Bairro.Nome_bairro = leitor["nome_bairro"].ToString();

                    Local_de_evento.Endereco.Bairro.Status_bairro = leitor["status_bairro"].ToString();

                    if ((leitor["id_do_municipio"] is DBNull) == false)
                    {

                        Local_de_evento.Endereco.Bairro.Id_do_municipio = (int)leitor["id_do_municipio"];
                    }

                    if ((leitor["id_municipio"] is DBNull) == false)
                    {

                        Local_de_evento.Endereco.Bairro.Municipio.Id_municipio = (int)leitor["id_municipio"];
                    }

                    Local_de_evento.Endereco.Bairro.Municipio.Nome_municipio = leitor["nome_municipio"].ToString();
                    Local_de_evento.Endereco.Bairro.Municipio.Status_municipio = leitor["status_municipio"].ToString();

                    if ((leitor["id_do_estado"] is DBNull) == false)
                    {
                        Local_de_evento.Endereco.Bairro.Municipio.Id_do_estado = (int)leitor["id_do_estado"];
                    }

                    if ((leitor["id_estado"] is DBNull) == false)
                    {
                        Local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado = (int)leitor["id_estado"];
                    }

                    Local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado = leitor["nome_estado"].ToString();
                    Local_de_evento.Endereco.Bairro.Municipio.Estado.Status_estado = leitor["status_estado"].ToString();

                    if ((leitor["id_do_pais"] is DBNull) == false)
                    {
                        Local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = (int)leitor["id_do_pais"];
                    }

                    if ((leitor["id_pais"] is DBNull) == false)
                    {
                        Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais = (int)leitor["id_pais"];
                    }

                    Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais = leitor["nome_pais"].ToString();
                    Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Status_pais = leitor["status_pais"].ToString();

                    Local_de_evento.Numero_local = leitor["numero_local"].ToString();
                    Local_de_evento.Status_local_evento = leitor["status_local_evento"].ToString();
                    Local_de_evento.Nome_do_local = leitor["nome_do_local"].ToString();
                    listaDeLocal_de_evento.Add(Local_de_evento);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDeLocal_de_evento;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Inserir(Local_de_evento local_de_evento)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Local_de_evento (endereco_virtual, id_do_endereco, complemento_do_local, id_local_de_evento, numero_local, status_local_evento, nome_do_local) values(@endereco_virtual, @id_do_endereco, @complemento_do_local, @id_local_de_evento, @numero_local, @status_local_evento, @nome_do_local)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@endereco_virtual", local_de_evento.Endereco_virtual);
                if (local_de_evento.Id_do_endereco == 0)
                {

                    comando.Parameters.AddWithValue("@id_do_endereco", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_endereco", local_de_evento.Id_do_endereco);
                }
                comando.Parameters.AddWithValue("@complemento_do_local", local_de_evento.Complemento_do_local);
                comando.Parameters.AddWithValue("@id_local_de_evento", local_de_evento.Id_local_de_evento);
                comando.Parameters.AddWithValue("@numero_local", local_de_evento.Numero_local);
                comando.Parameters.AddWithValue("@status_local_evento", local_de_evento.Status_local_evento);
                comando.Parameters.AddWithValue("@nome_do_local", local_de_evento.Nome_do_local);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Atualiza(Local_de_evento local_de_evento, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Local_de_evento set endereco_virtual = @endereco_virtual, id_do_endereco = @id_do_endereco, complemento_do_local = @complemento_do_local, id_local_de_evento = @id_local_de_evento, numero_local = @numero_local, status_local_evento = @status_local_evento, nome_do_local = @nome_do_local where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@endereco_virtual", local_de_evento.Endereco_virtual);
                comando.Parameters.AddWithValue("@id_do_endereco", local_de_evento.Id_do_endereco);
                comando.Parameters.AddWithValue("@complemento_do_local", local_de_evento.Complemento_do_local);
                comando.Parameters.AddWithValue("@id_local_de_evento", local_de_evento.Id_local_de_evento);
                comando.Parameters.AddWithValue("@numero_local", local_de_evento.Numero_local);
                comando.Parameters.AddWithValue("@status_local_evento", local_de_evento.Status_local_evento);
                comando.Parameters.AddWithValue("@nome_do_local", local_de_evento.Nome_do_local);
                comando.ExecuteNonQuery();
                conexao.Close();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Excluir(string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string sql = "Delete From Local_de_evento where " + condicao + "";
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
    }
}