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
    /// Classe gerada para a tabela : Tipo_de_grupo.(Desativada, criada p/ versões posteriores)
	/// </summary>
	class DAOTipo_de_grupo
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOTipo_de_grupo(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
		public List<Tipo_de_grupo> BuscarRegistro(string whereClause)
		{
			List<Tipo_de_grupo> listaDeTipo_de_grupo = new List<Tipo_de_grupo>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Tipo_de_grupo where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Tipo_de_grupo Tipo_de_grupo = new Tipo_de_grupo();
					Tipo_de_grupo.Id_tipo_de_grupo = (int)leitor["id_tipo_de_grupo"];
					Tipo_de_grupo.Status_tipo_de_grupo = (string)leitor["status_tipo_de_grupo"];
					Tipo_de_grupo.Descricao_tipo_de_grupo = leitor["descricao_tipo_de_grupo"].ToString();
					Tipo_de_grupo.Nome_tipo_de_grupo = leitor["nome_tipo_de_grupo"].ToString();
					listaDeTipo_de_grupo.Add(Tipo_de_grupo);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeTipo_de_grupo;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
		public void Inserir(Tipo_de_grupo tipo_de_grupo)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Tipo_de_grupo (id_tipo_de_grupo, status_tipo_de_grupo, descricao_tipo_de_grupo, nome_tipo_de_grupo) values(@id_tipo_de_grupo, @status_tipo_de_grupo, @descricao_tipo_de_grupo, @nome_tipo_de_grupo)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_tipo_de_grupo", tipo_de_grupo.Id_tipo_de_grupo);
				comando.Parameters.AddWithValue("@status_tipo_de_grupo", tipo_de_grupo.Status_tipo_de_grupo);
				comando.Parameters.AddWithValue("@descricao_tipo_de_grupo", tipo_de_grupo.Descricao_tipo_de_grupo);
				comando.Parameters.AddWithValue("@nome_tipo_de_grupo", tipo_de_grupo.Nome_tipo_de_grupo);
				comando.ExecuteNonQuery();
				comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
		public void Atualiza(Tipo_de_grupo tipo_de_grupo, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Tipo_de_grupo set id_tipo_de_grupo = @id_tipo_de_grupo, status_tipo_de_grupo = @status_tipo_de_grupo, descricao_tipo_de_grupo = @descricao_tipo_de_grupo, nome_tipo_de_grupo = @nome_tipo_de_grupo where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_tipo_de_grupo", tipo_de_grupo.Id_tipo_de_grupo);
				comando.Parameters.AddWithValue("@status_tipo_de_grupo", tipo_de_grupo.Status_tipo_de_grupo);
				comando.Parameters.AddWithValue("@descricao_tipo_de_grupo", tipo_de_grupo.Descricao_tipo_de_grupo);
				comando.Parameters.AddWithValue("@nome_tipo_de_grupo", tipo_de_grupo.Nome_tipo_de_grupo);
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
			string sql = "Delete From Tipo_de_grupo where " + condicao + "";
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