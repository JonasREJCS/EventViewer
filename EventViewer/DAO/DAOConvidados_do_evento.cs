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
	/// Classe gerada para a tabela : Convidados_do_evento.
	/// </summary>
	class DAOConvidados_do_evento
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOConvidados_do_evento(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
		public List<Convidados_do_evento> BuscarRegistro(string whereClause)
		{
			List<Convidados_do_evento> listaDeConvidados_do_evento = new List<Convidados_do_evento>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Convidados_do_evento where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Convidados_do_evento Convidados_do_evento = new Convidados_do_evento();
					Convidados_do_evento.Id_do_convidado = (int)leitor["id_do_convidado"];
					Convidados_do_evento.Id_do_evento_agendado = (int)leitor["id_do_evento_agendado"];
					listaDeConvidados_do_evento.Add(Convidados_do_evento);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeConvidados_do_evento;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
		public void Inserir(Convidados_do_evento convidados_do_evento)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Convidados_do_evento (id_do_convidado, id_do_evento_agendado) values(@id_do_convidado, @id_do_evento_agendado)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_convidado", convidados_do_evento.Id_do_convidado);
				comando.Parameters.AddWithValue("@id_do_evento_agendado", convidados_do_evento.Id_do_evento_agendado);
				comando.ExecuteNonQuery();
				comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
		public void Atualiza(Convidados_do_evento convidados_do_evento, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Convidados_do_evento set id_do_convidado = @id_do_convidado, id_do_evento_agendado = @id_do_evento_agendado where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@id_do_convidado", convidados_do_evento.Id_do_convidado);
				comando.Parameters.AddWithValue("@id_do_evento_agendado", convidados_do_evento.Id_do_evento_agendado);
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
			string sql = "Delete From Convidados_do_evento where " + condicao + "";
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