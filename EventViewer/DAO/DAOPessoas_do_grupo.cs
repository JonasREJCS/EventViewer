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
    /// Classe gerada para a tabela : Pessoas_do_grupo.
    /// </summary>
    class DAOPessoas_do_grupo
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOPessoas_do_grupo(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        public ObservableCollection<Pessoa> BuscarRegistro(string whereClause)
        {
            ObservableCollection<Pessoa> listaDePessoas_do_grupo = new ObservableCollection<Pessoa>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            string SQL = "Select * From view_participantes_do_grupo where " + whereClause + " LIMIT 1000 ";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Pessoa Pessoa = new Pessoa();
                    if ((leitor["caminho_foto"] is DBNull) == false)
                    {
                        Pessoa.Caminho_foto = (byte[])leitor["caminho_foto"];
                    }
                    Pessoa.Status_pessoa = leitor["status_pessoa"].ToString();
                    Pessoa.Cpf = leitor["cpf"].ToString();
                    Pessoa.Telefone_residencial = leitor["telefone_residencial"].ToString();
                    Pessoa.Complemento_residencia = leitor["complemento_residencia"].ToString();

                    Pessoa.Telefone_movel = leitor["telefone_movel"].ToString();

                    Pessoa.Nome_pessoa = leitor["nome_pessoa"].ToString();


                    Pessoa.Id_pessoa = (int)leitor["id_pessoa"];

                    Pessoa.Identidade = leitor["identidade"].ToString();

                    Pessoa.Email = leitor["email"].ToString();

                    listaDePessoas_do_grupo.Add(Pessoa);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDePessoas_do_grupo;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Inserir(Pessoas_do_grupo pessoas_do_grupo)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Pessoas_do_grupo (contato_no_evento, id_do_grupo_de_participante, id_da_pessoa) values(@contato_no_evento, @id_do_grupo_de_participante, @id_da_pessoa)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@contato_no_evento", pessoas_do_grupo.Contato_no_evento);
                comando.Parameters.AddWithValue("@id_do_grupo_de_participante", pessoas_do_grupo.Id_do_grupo_de_participante);
                comando.Parameters.AddWithValue("@id_da_pessoa", pessoas_do_grupo.Id_da_pessoa);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Atualiza(Pessoas_do_grupo pessoas_do_grupo, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Pessoas_do_grupo set contato_no_evento = @contato_no_evento, id_do_grupo_de_participante = @id_do_grupo_de_participante, id_da_pessoa = @id_da_pessoa where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@contato_no_evento", pessoas_do_grupo.Contato_no_evento);
                comando.Parameters.AddWithValue("@id_do_grupo_de_participante", pessoas_do_grupo.Id_do_grupo_de_participante);
                comando.Parameters.AddWithValue("@id_da_pessoa", pessoas_do_grupo.Id_da_pessoa);
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
                string sql = "Delete From Pessoas_do_grupo where " + condicao + "";
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

        public int BuscaQuantidadePessoa(int id)
        {
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            string SQL = "SELECT count(id_do_grupo_de_participante) FROM view_participantes_do_grupo where id_do_grupo_de_participante = '" + id + "'";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                int quantidadePessoa = 0;
                if (leitor.Read())
                {
                    quantidadePessoa = Convert.ToInt32(leitor["count(id_do_grupo_de_participante)"]);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return quantidadePessoa;

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}