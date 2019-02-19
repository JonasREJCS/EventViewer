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
	/// Classe gerada para a tabela : Pais.
	/// </summary>
	class DAOPais
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOPais(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar Paises já registrados no bcdd
		public List<Pais> BuscarRegistro(string whereClause)
		{
			List<Pais> listaDePais = new List<Pais>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Pais where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Pais Pais = new Pais();
					Pais.Id_pais = (int)leitor["id_pais"];
					Pais.Status_pais = (string)leitor["status_pais"];
					Pais.Nome_pais = (string)leitor["nome_pais"];
					listaDePais.Add(Pais);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDePais;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para salvar novos Paises no bcdd
        public long Inserir(Pais pais)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Pais (id_pais, status_pais, nome_pais) values(@id_pais, @status_pais, @nome_pais)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_pais", pais.Id_pais);
				comando.Parameters.AddWithValue("@status_pais", pais.Status_pais);
				comando.Parameters.AddWithValue("@nome_pais", pais.Nome_pais);
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
        //método para modificar dados de Paises registrados no bcdd
        public void Atualiza(Pais pais, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Pais set id_pais = @id_pais, status_pais = @status_pais, nome_pais = @nome_pais where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_pais", pais.Id_pais);
				comando.Parameters.AddWithValue("@status_pais", pais.Status_pais);
				comando.Parameters.AddWithValue("@nome_pais", pais.Nome_pais);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para apagar Paises registrados no bcdd
        public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Pais where " + condicao + "";
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