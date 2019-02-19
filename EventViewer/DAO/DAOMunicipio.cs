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
	/// Classe gerada para a tabela : Municipio.
	/// </summary>
	class DAOMunicipio
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOMunicipio(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar Municipios já registrados no bcdd
		public List<Municipio> BuscarRegistro(string whereClause)
		{
			List<Municipio> listaDeMunicipio = new List<Municipio>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Municipio where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Municipio Municipio = new Municipio();
					Municipio.Id_municipio = (int)leitor["id_municipio"];
					Municipio.Status_municipio = (string)leitor["status_municipio"];
					Municipio.Nome_municipio = (string)leitor["nome_municipio"];
					Municipio.Id_do_estado = (int)leitor["id_do_estado"];
					listaDeMunicipio.Add(Municipio);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeMunicipio;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para salvar novos Municipios no bcdd
        public long Inserir(Municipio municipio)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Municipio (id_municipio, status_municipio, nome_municipio, id_do_estado) values(@id_municipio, @status_municipio, @nome_municipio, @id_do_estado)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_municipio", municipio.Id_municipio);
				comando.Parameters.AddWithValue("@status_municipio", municipio.Status_municipio);
				comando.Parameters.AddWithValue("@nome_municipio", municipio.Nome_municipio);
				comando.Parameters.AddWithValue("@id_do_estado", municipio.Id_do_estado);
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
        //método para modificar dados de Municipios já registrados no bcdd
        public void Atualiza(Municipio municipio, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Municipio set id_municipio = @id_municipio, status_municipio = @status_municipio, nome_municipio = @nome_municipio, id_do_estado = @id_do_estado where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_municipio", municipio.Id_municipio);
				comando.Parameters.AddWithValue("@status_municipio", municipio.Status_municipio);
				comando.Parameters.AddWithValue("@nome_municipio", municipio.Nome_municipio);
				comando.Parameters.AddWithValue("@id_do_estado", municipio.Id_do_estado);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para apagar Municipios registrados no bcdd
        public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Municipio where " + condicao + "";
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