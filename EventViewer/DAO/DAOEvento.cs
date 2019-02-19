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
	/// Classe gerada para a tabela : Evento.
	/// </summary>
	class DAOEvento
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOEvento(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar dados dos eventos já registrados no bcdd
		public List<Evento> BuscarRegistro(string whereClause)
		{
			List<Evento> listaDeEvento = new List<Evento>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();


			string SQL = "Select * From Evento where " + whereClause + " LIMIT 1000 ";
			
            
            
            comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Evento Evento = new Evento();
					Evento.Descricao_evento = (string)leitor["descricao_evento"];
                    if ((leitor["logotipo_evento"] is DBNull)== false)
                    {
                         Evento.Logotipo_evento = (byte[])leitor["logotipo_evento"];
                    }	
					Evento.Nome_evento = (string)leitor["nome_evento"];
					Evento.Id_evento = (int)leitor["id_evento"];
					Evento.Status_evento = (string)leitor["status_evento"];
					listaDeEvento.Add(Evento);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeEvento;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para salvar novos eventos no bcdd
        public void Inserir(Evento evento)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Evento (descricao_evento, logotipo_evento, nome_evento, id_evento, status_evento) values(@descricao_evento, @logotipo_evento, @nome_evento, @id_evento, @status_evento)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@descricao_evento", evento.Descricao_evento);
				comando.Parameters.AddWithValue("@logotipo_evento", evento.Logotipo_evento);
				comando.Parameters.AddWithValue("@nome_evento", evento.Nome_evento);
				comando.Parameters.AddWithValue("@id_evento", evento.Id_evento);
				comando.Parameters.AddWithValue("@status_evento", evento.Status_evento);
				comando.ExecuteNonQuery();
				comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para alterar dados dos eventos já registrados no bcdd
        public void Atualiza(Evento evento, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Evento set descricao_evento = @descricao_evento, logotipo_evento = @logotipo_evento, nome_evento = @nome_evento, id_evento = @id_evento, status_evento = @status_evento where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@descricao_evento", evento.Descricao_evento);
				comando.Parameters.AddWithValue("@logotipo_evento", evento.Logotipo_evento);
				comando.Parameters.AddWithValue("@nome_evento", evento.Nome_evento);
				comando.Parameters.AddWithValue("@id_evento", evento.Id_evento);
				comando.Parameters.AddWithValue("@status_evento", evento.Status_evento);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para apagar eventos registrados no bcdd
        public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Evento where " + condicao + "";
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