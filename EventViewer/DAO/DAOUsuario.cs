using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EventViewer.Modelo;
using System.Collections.ObjectModel;

namespace EventViewer.DAO
{
	/// <summary>
	/// Classe gerada para a tabela : Usuario.
	/// </summary>
	class DAOUsuario
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOUsuario(MySqlConnection conexao)
		{
			this.conexao = conexao;
			comando = new MySqlCommand();
			comando.Connection = this.conexao;
		}
        //método para buscar dados  dos Usuários já registrados no bcdd
		public ObservableCollection<Usuario> BuscarRegistro(string whereClause)
		{
            ObservableCollection<Usuario> listaDeUsuario = new ObservableCollection<Usuario>();
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string SQL = "Select * From Usuario where " + whereClause + " LIMIT 1000 ";
			comando.CommandText = SQL;
			MySqlDataReader leitor = comando.ExecuteReader();
			try
			{
			while (leitor.Read())
			{
					Usuario Usuario = new Usuario();
					Usuario.Tipo_usuario = (string)leitor["tipo_usuario"];
					Usuario.Status_usuario = (string)leitor["status_usuario"];
					Usuario.Nome_usuario = (string)leitor["nome_usuario"];
					Usuario.Id_usuario = (int)leitor["id_usuario"];
					Usuario.Senha = (string)leitor["senha"];
					Usuario.Usuario_login = (string)leitor["usuario_login"];
					listaDeUsuario.Add(Usuario);
			}
			leitor.Close();
			comando.Dispose();
			leitor = null;
			return listaDeUsuario;
		}
		catch (Exception erro)
		{
			throw new Exception(erro.Message);
		}
	}
        //método para salvar novos Usuários no bcdd
		public long Inserir(Usuario usuario)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Insert into Usuario (tipo_usuario, status_usuario, nome_usuario, id_usuario, senha, usuario_login) values(@tipo_usuario, @status_usuario, @nome_usuario, @id_usuario, @senha, @usuario_login)";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@tipo_usuario", usuario.Tipo_usuario);
				comando.Parameters.AddWithValue("@status_usuario", usuario.Status_usuario);
				comando.Parameters.AddWithValue("@nome_usuario", usuario.Nome_usuario);
				comando.Parameters.AddWithValue("@id_usuario", usuario.Id_usuario);
				comando.Parameters.AddWithValue("@senha", usuario.Senha);
				comando.Parameters.AddWithValue("@usuario_login", usuario.Usuario_login);
				comando.ExecuteNonQuery();
                long ultimoId = comando.LastInsertedId;
				comando.Dispose();
                return ultimoId;
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para modificar dados dos Usuários registrados no bcdd
		public void Atualiza(Usuario usuario, string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
				conexao.Open();
			string SQL = "Update Usuario set tipo_usuario = @tipo_usuario, status_usuario = @status_usuario, nome_usuario = @nome_usuario, id_usuario = @id_usuario, senha = @senha, usuario_login = @usuario_login where " + condicao + "";
			comando.CommandText = SQL;
			comando.CommandType = CommandType.Text;
			comando.Parameters.Clear();
				comando.Parameters.AddWithValue("@tipo_usuario", usuario.Tipo_usuario);
				comando.Parameters.AddWithValue("@status_usuario", usuario.Status_usuario);
				comando.Parameters.AddWithValue("@nome_usuario", usuario.Nome_usuario);
				comando.Parameters.AddWithValue("@id_usuario", usuario.Id_usuario);
				comando.Parameters.AddWithValue("@senha", usuario.Senha);
				comando.Parameters.AddWithValue("@usuario_login", usuario.Usuario_login);
			comando.ExecuteNonQuery();
			conexao.Close();
			comando.Dispose();
			}
			catch (Exception erro)
			{
				throw new Exception(erro.Message);
			}
		}
        //método para apagar Usuários registrados no bcdd
        public void Excluir(string condicao)
		{
			try
			{
			if (conexao.State != System.Data.ConnectionState.Open)
					conexao.Open();
			string sql = "Delete From Usuario where " + condicao + "";
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