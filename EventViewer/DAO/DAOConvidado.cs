using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EventViewer.Modelo;
using System.Collections.ObjectModel;

namespace EventViewer.DAO
{
    /// <summary>
    /// Classe gerada para a tabela : Convidado.
    /// </summary>
    class DAOConvidado
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOConvidado(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        //método para buscar convidados já registrados
        public ObservableCollection<Convidado> BuscarRegistro(string whereClause)
        {
            ObservableCollection<Convidado> listaDeConvidado = new ObservableCollection<Convidado>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            //string SQL = "Select * From Convidado where " + whereClause + " LIMIT 1000 ";
            string SQL = "Select * From view_Convidado where " + whereClause + " LIMIT 1000 ";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Convidado Convidado = new Convidado();
                    
                    Convidado.Nome_convidado = (string)leitor["nome_convidado"];
                    Convidado.Id_convidado = (int)leitor["id_convidado"];
                    Convidado.Status_convidado = leitor["status_convidado"].ToString();
                    Convidado.Descricao_convidado = leitor["descricao_convidado"].ToString();
                    listaDeConvidado.Add(Convidado);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDeConvidado;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para salvar novos convidados nos registros
        public long Inserir(Convidado convidado)
        {
            try
            {
                long id;
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Convidado (id_do_tipo_de_convidado, nome_convidado, id_convidado, status_convidado, descricao_convidado) values(@id_do_tipo_de_convidado, @nome_convidado, @id_convidado, @status_convidado, @descricao_convidado)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                if (convidado.Id_do_tipo_de_convidado != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_tipo_de_convidado", convidado.Id_do_tipo_de_convidado);

                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_tipo_de_convidado", DBNull.Value);

                }
                comando.Parameters.AddWithValue("@nome_convidado", convidado.Nome_convidado);
                comando.Parameters.AddWithValue("@id_convidado", convidado.Id_convidado);
                comando.Parameters.AddWithValue("@status_convidado", convidado.Status_convidado);
                comando.Parameters.AddWithValue("@descricao_convidado", convidado.Descricao_convidado);
                comando.ExecuteNonQuery();
                id = comando.LastInsertedId;
                comando.Dispose();
                return id;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para modificar os dados de um convidado já registrado
        public void Atualiza(Convidado convidado, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Convidado set id_do_tipo_de_convidado = @id_do_tipo_de_convidado, nome_convidado = @nome_convidado, id_convidado = @id_convidado, status_convidado = @status_convidado, descricao_convidado = @descricao_convidado where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                if (convidado.Id_do_tipo_de_convidado != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_tipo_de_convidado", convidado.Id_do_tipo_de_convidado);

                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_tipo_de_convidado", DBNull.Value);
                }
                comando.Parameters.AddWithValue("@nome_convidado", convidado.Nome_convidado);
                comando.Parameters.AddWithValue("@id_convidado", convidado.Id_convidado);
                comando.Parameters.AddWithValue("@status_convidado", convidado.Status_convidado);
                comando.Parameters.AddWithValue("@descricao_convidado", convidado.Descricao_convidado);
                comando.ExecuteNonQuery();
                conexao.Close();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para excluir um convidado dos registros
        public void Excluir(string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string sql = "Delete From Convidado where " + condicao + "";
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