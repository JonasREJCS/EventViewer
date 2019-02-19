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
    /// Classe gerada para a tabela : Grupo_de_participante.
    /// </summary>
    class DAOGrupo_de_participante
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOGrupo_de_participante(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        //método para buscar dados dos grupos registrados no bcdd
        public List<Grupo_de_participante> BuscarRegistro(string whereClause)
        {
            List<Grupo_de_participante> listaDeGrupo_de_participante = new List<Grupo_de_participante>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            string SQL = "Select * From grupo_de_participante where " + whereClause + " LIMIT 1000 ";
            comando.CommandText = SQL;
            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Grupo_de_participante Grupo_de_participante = new Grupo_de_participante();
                    Grupo_de_participante.Id_grupo_de_participante = (int)leitor["id_grupo_de_participante"];
                    Grupo_de_participante.Descricao_grupo_de_participante = leitor["descricao_grupo_de_participante"].ToString();
                    Grupo_de_participante.Nome_grupo_de_participante = leitor["nome_grupo_de_participante"].ToString();
                    Grupo_de_participante.Status_grupo_de_participante = (string)leitor["status_grupo_de_participante"].ToString();
                    //if (!(leitor["id_do_tipo_de_grupo"] is DBNull))
                    //{
                    //    Grupo_de_participante.Id_do_tipo_de_grupo = (int)leitor["id_do_tipo_de_grupo"];
                    //    Tipo_de_grupo tipo_de_grupo = new Tipo_de_grupo()
                    //    {
                    //        Id_tipo_de_grupo = (int)leitor["id_tipo_de_grupo"],
                    //        Status_tipo_de_grupo = leitor["status_tipo_de_grupo"].ToString(),
                    //        Descricao_tipo_de_grupo = leitor["descricao_tipo_de_grupo"].ToString(),
                    //        Nome_tipo_de_grupo = leitor["nome_tipo_de_grupo"].ToString()
                    //    };
                    //    Grupo_de_participante.Tipo = tipo_de_grupo;
                    //}

                    listaDeGrupo_de_participante.Add(Grupo_de_participante);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;
                return listaDeGrupo_de_participante;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para salvar novos grupos no bcdd
        public long Inserir(Grupo_de_participante grupo_de_participante)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Grupo_de_participante (id_grupo_de_participante, descricao_grupo_de_participante, nome_grupo_de_participante, status_grupo_de_participante) values(@id_grupo_de_participante, @descricao_grupo_de_participante, @nome_grupo_de_participante, @status_grupo_de_participante)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_grupo_de_participante", grupo_de_participante.Id_grupo_de_participante);
                comando.Parameters.AddWithValue("@descricao_grupo_de_participante", grupo_de_participante.Descricao_grupo_de_participante);
                comando.Parameters.AddWithValue("@nome_grupo_de_participante", grupo_de_participante.Nome_grupo_de_participante);
                comando.Parameters.AddWithValue("@status_grupo_de_participante", grupo_de_participante.Status_grupo_de_participante);
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
        //método para modificar dados dos grupos já registrados no bcdd
        public void Atualiza(Grupo_de_participante grupo_de_participante, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Grupo_de_participante set id_grupo_de_participante = @id_grupo_de_participante, descricao_grupo_de_participante = @descricao_grupo_de_participante, nome_grupo_de_participante = @nome_grupo_de_participante, status_grupo_de_participante = @status_grupo_de_participante where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_grupo_de_participante", grupo_de_participante.Id_grupo_de_participante);
                comando.Parameters.AddWithValue("@descricao_grupo_de_participante", grupo_de_participante.Descricao_grupo_de_participante);
                comando.Parameters.AddWithValue("@nome_grupo_de_participante", grupo_de_participante.Nome_grupo_de_participante);
                comando.Parameters.AddWithValue("@status_grupo_de_participante", grupo_de_participante.Status_grupo_de_participante);
                comando.ExecuteNonQuery();
                conexao.Close();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para grupos registrados no bcdd
        public void Excluir(string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string sql = "Delete From Grupo_de_participante where " + condicao + "";
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