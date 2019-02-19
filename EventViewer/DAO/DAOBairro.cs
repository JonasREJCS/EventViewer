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
	/// Classe gerada para a tabela : Bairro.
	/// </summary>
	class DAOBairro
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOBairro(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar bairros já registrados
		public List<Bairro> BuscarRegistro(string whereClause)
		{
			List<Bairro> listaDeBairro = new List<Bairro>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Bairro where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Bairro Bairro = new Bairro();
					Bairro.Status_bairro = (string)leitor["status_bairro"];
					Bairro.Nome_bairro = (string)leitor["nome_bairro"];
					Bairro.Id_bairro = (int)leitor["id_bairro"];
					Bairro.Id_do_municipio = (int)leitor["id_do_municipio"];
					listaDeBairro.Add(Bairro);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeBairro;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para salvar um novo bairros nos registros
		public long Inserir(Bairro bairro)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Bairro (status_bairro, nome_bairro, id_bairro, id_do_municipio) values(@status_bairro, @nome_bairro, @id_bairro, @id_do_municipio)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@status_bairro", bairro.Status_bairro);
				comando.Parameters.AddWithValue("@nome_bairro", bairro.Nome_bairro);
				comando.Parameters.AddWithValue("@id_bairro", bairro.Id_bairro);
				comando.Parameters.AddWithValue("@id_do_municipio", bairro.Id_do_municipio);
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
        //método para modificar bairros já registrados
		public void Atualiza(Bairro bairro, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Bairro set status_bairro = @status_bairro, nome_bairro = @nome_bairro, id_bairro = @id_bairro, id_do_municipio = @id_do_municipio where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@status_bairro", bairro.Status_bairro);
				comando.Parameters.AddWithValue("@nome_bairro", bairro.Nome_bairro);
				comando.Parameters.AddWithValue("@id_bairro", bairro.Id_bairro);
				comando.Parameters.AddWithValue("@id_do_municipio", bairro.Id_do_municipio);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para excluir bairros registrados
		public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Bairro where " + condicao + "";
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