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
	/// Classe gerada para a tabela : Tipo_de_convidado.(Desativada, criada p/ versões posteriores)
	/// </summary>
	class DAOTipo_de_convidado
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOTipo_de_convidado(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
		public List<Tipo_de_convidado> BuscarRegistro(string whereClause)
		{
			List<Tipo_de_convidado> listaDeTipo_de_convidado = new List<Tipo_de_convidado>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Tipo_de_convidado where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Tipo_de_convidado Tipo_de_convidado = new Tipo_de_convidado();
					Tipo_de_convidado.Id_tipo_de_convidado = (int)leitor["id_tipo_de_convidado"];
					Tipo_de_convidado.Descricao_tipo_convidado = leitor["descricao_tipo_convidado"].ToString();
                    Tipo_de_convidado.Nome_tipo_convidado = leitor["nome_tipo_convidado"].ToString();
                    Tipo_de_convidado.Status_tipo_convidado = leitor["status_tipo_convidado"].ToString();
					listaDeTipo_de_convidado.Add(Tipo_de_convidado);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeTipo_de_convidado;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
		public void Inserir(Tipo_de_convidado tipo_de_convidado)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Tipo_de_convidado (id_tipo_de_convidado, descricao_tipo_convidado, nome_tipo_convidado, status_tipo_convidado) values(@id_tipo_de_convidado, @descricao_tipo_convidado, @nome_tipo_convidado, @status_tipo_convidado)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_tipo_de_convidado", tipo_de_convidado.Id_tipo_de_convidado);
				comando.Parameters.AddWithValue("@descricao_tipo_convidado", tipo_de_convidado.Descricao_tipo_convidado);
				comando.Parameters.AddWithValue("@nome_tipo_convidado", tipo_de_convidado.Nome_tipo_convidado);
				comando.Parameters.AddWithValue("@status_tipo_convidado", tipo_de_convidado.Status_tipo_convidado);
				comando.ExecuteNonQuery();
				comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
		public void Atualiza(Tipo_de_convidado tipo_de_convidado, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Tipo_de_convidado set id_tipo_de_convidado = @id_tipo_de_convidado, descricao_tipo_convidado = @descricao_tipo_convidado, nome_tipo_convidado = @nome_tipo_convidado, status_tipo_convidado = @status_tipo_convidado where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_tipo_de_convidado", tipo_de_convidado.Id_tipo_de_convidado);
				comando.Parameters.AddWithValue("@descricao_tipo_convidado", tipo_de_convidado.Descricao_tipo_convidado);
				comando.Parameters.AddWithValue("@nome_tipo_convidado", tipo_de_convidado.Nome_tipo_convidado);
				comando.Parameters.AddWithValue("@status_tipo_convidado", tipo_de_convidado.Status_tipo_convidado);
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
			string sql = "Delete From Tipo_de_convidado where " + condicao + "";
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