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
	/// Classe gerada para a tabela : Grupos_do_evento_agendado.
	/// </summary>
	class DAOGrupos_do_evento_agendado
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOGrupos_do_evento_agendado(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
		public List<Grupos_do_evento_agendado> BuscarRegistro(string whereClause)
		{
			List<Grupos_do_evento_agendado> listaDeGrupos_do_evento_agendado = new List<Grupos_do_evento_agendado>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Grupos_do_evento_agendado where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Grupos_do_evento_agendado Grupos_do_evento_agendado = new Grupos_do_evento_agendado();
					Grupos_do_evento_agendado.Id_do_grupo = (int)leitor["id_do_grupo"];
					Grupos_do_evento_agendado.Id_do_evento_agendado = (int)leitor["id_do_evento_agendado"];
					listaDeGrupos_do_evento_agendado.Add(Grupos_do_evento_agendado);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeGrupos_do_evento_agendado;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
		public void Inserir(Grupos_do_evento_agendado grupos_do_evento_agendado)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Grupos_do_evento_agendado (id_do_grupo, id_do_evento_agendado) values(@id_do_grupo, @id_do_evento_agendado)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_grupo", grupos_do_evento_agendado.Id_do_grupo);
				comando.Parameters.AddWithValue("@id_do_evento_agendado", grupos_do_evento_agendado.Id_do_evento_agendado);
				comando.ExecuteNonQuery();
				comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
		public void Atualiza(Grupos_do_evento_agendado grupos_do_evento_agendado, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Grupos_do_evento_agendado set id_do_grupo = @id_do_grupo, id_do_evento_agendado = @id_do_evento_agendado where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_grupo", grupos_do_evento_agendado.Id_do_grupo);
				comando.Parameters.AddWithValue("@id_do_evento_agendado", grupos_do_evento_agendado.Id_do_evento_agendado);
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
			string sql = "Delete From Grupos_do_evento_agendado where " + condicao + "";
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