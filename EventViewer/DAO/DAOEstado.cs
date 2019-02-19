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
	/// Classe gerada para a tabela : Estado.
	/// </summary>
	class DAOEstado
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOEstado(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar dados de Estados já registrados no bcdd
		public List<Estado> BuscarRegistro(string whereClause)
		{
			List<Estado> listaDeEstado = new List<Estado>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Estado where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Estado Estado = new Estado();
					Estado.Id_do_pais = (int)leitor["id_do_pais"];
					Estado.Status_estado = (string)leitor["status_estado"];
					Estado.Id_estado = (int)leitor["id_estado"];
					Estado.Nome_estado = (string)leitor["nome_estado"];
					listaDeEstado.Add(Estado);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeEstado;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para registrar novos Estados no bcdd
        public long Inserir(Estado estado)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Estado (id_do_pais, status_estado, id_estado, nome_estado) values(@id_do_pais, @status_estado, @id_estado, @nome_estado)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_pais", estado.Id_do_pais);
				comando.Parameters.AddWithValue("@status_estado", estado.Status_estado);
				comando.Parameters.AddWithValue("@id_estado", estado.Id_estado);
				comando.Parameters.AddWithValue("@nome_estado", estado.Nome_estado);
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
        //método para modificar dados de Estados registrados no bcdd
        public void Atualiza(Estado estado, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Estado set id_do_pais = @id_do_pais, status_estado = @status_estado, id_estado = @id_estado, nome_estado = @nome_estado where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_pais", estado.Id_do_pais);
				comando.Parameters.AddWithValue("@status_estado", estado.Status_estado);
				comando.Parameters.AddWithValue("@id_estado", estado.Id_estado);
				comando.Parameters.AddWithValue("@nome_estado", estado.Nome_estado);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para apagar dados de Estados registrados no bcdd
        public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Estado where " + condicao + "";
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