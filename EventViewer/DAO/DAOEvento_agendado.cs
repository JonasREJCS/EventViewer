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
    /// Classe gerada para a tabela : Evento_agendado.
    /// </summary>
    class DAOEvento_agendado
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOEvento_agendado(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        public List<Evento_agendado> BuscarRegistro(string whereClause)
        {
            List<Evento_agendado> listaDeEvento_agendado = new List<Evento_agendado>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();

            string SQL = "Select * From view_evento_agendado where " + whereClause + " LIMIT 1000";
            comando.CommandText = SQL;
            MySqlDataReader leitor;
            try
            {
                using (leitor = comando.ExecuteReader())
                {
                    bool continuarLendo = leitor.Read();
                    while (continuarLendo)
                    {

                        Evento_agendado Evento_agendado = new Evento_agendado();
                        #region
                        Evento_agendado.Id_evento_agendado = (int)leitor["id_evento_agendado"];
                        Evento_agendado.Status_evento_agendado = (string)leitor["status_evento_agendado"];
                        Evento_agendado.DataHora = leitor.GetDateTime("data");
                        Evento_agendado.Descricao_evento_agendado = leitor["descricao_evento_agendado"].ToString();

                        Evento_agendado.Id_do_evento = (int)leitor["id_do_evento"];
                        Evento_agendado.Evento.Descricao_evento = leitor["descricao_evento"].ToString();
                        if ((leitor["logotipo_evento"] is DBNull) == false)
                        {
                            Evento_agendado.Evento.Logotipo_evento = (byte[])leitor["logotipo_evento"];

                        }
                        Evento_agendado.Evento.Nome_evento = leitor["nome_evento"].ToString();
                        Evento_agendado.Evento.Id_evento = (int)leitor["id_evento"];
                        Evento_agendado.Evento.Status_evento = (string)leitor["status_evento"];

                        if ((leitor["horario_termino"] is DBNull) == false)
                        {
                            Evento_agendado.Horario_termino = leitor.GetDateTime("horario_termino");
                        }
                        if ((leitor["horario_encontro"] is DBNull) == false)
                        {
                            Evento_agendado.Horario_encontro = leitor.GetDateTime("horario_encontro");
                        }
                        Evento_agendado.Id_do_usuario_organizador = (int)leitor["id_do_usuario_organizador"];
                        Evento_agendado.Usuario.Tipo_usuario = (string)leitor["tipo_usuario"];
                        Evento_agendado.Usuario.Nome_usuario = (string)leitor["nome_usuario"];
                        Evento_agendado.Usuario.Id_usuario = (int)leitor["id_usuario"];
                        Evento_agendado.Usuario.Usuario_login = (string)leitor["usuario_login"];

                        if ((leitor["capacidade_participante"] is DBNull) == false)
                        {
                            Evento_agendado.Capacidade_participante = (int)leitor["capacidade_participante"];
                        }
                        Evento_agendado.Id_do_local_de_evento = (int)leitor["id_do_local_de_evento"];
                        Evento_agendado.Local_de_evento.Endereco_virtual = leitor["endereco_virtual"].ToString();
                        if (leitor["id_do_endereco"] is DBNull)
                        {
                            Evento_agendado.Local_de_evento.Id_do_endereco = 0;
                        }
                        else
                        {
                            Evento_agendado.Local_de_evento.Id_do_endereco = (int)leitor["id_do_endereco"];
                            Evento_agendado.Local_de_evento.Endereco.Id_endereco = (int)leitor["id_endereco"];

                            Evento_agendado.Local_de_evento.Endereco.Cep = leitor["cep"].ToString();


                            Evento_agendado.Local_de_evento.Endereco.Logradouro = leitor["logradouro"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Status_endereco = leitor["status_endereco"].ToString();

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Id_bairro = (int)leitor["id_bairro"];


                            Evento_agendado.Local_de_evento.Endereco.Bairro.Nome_bairro = leitor["nome_bairro"].ToString();

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Status_bairro = leitor["status_bairro"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Bairro.Id_do_municipio = (int)leitor["id_do_municipio"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Id_municipio = (int)leitor["id_municipio"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Nome_municipio = leitor["nome_municipio"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Status_municipio = leitor["status_municipio"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Id_do_estado = (int)leitor["id_do_estado"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado = (int)leitor["id_estado"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado = leitor["nome_estado"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Status_estado = leitor["status_estado"].ToString();

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = (int)leitor["id_do_pais"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais = (int)leitor["id_pais"];

                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais = leitor["nome_pais"].ToString();
                            Evento_agendado.Local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Status_pais = leitor["status_pais"].ToString();

                        }

                        Evento_agendado.Local_de_evento.Complemento_do_local = leitor["complemento_do_local"].ToString();
                        if (leitor["id_local_de_evento"] is DBNull)
                        {
                            Evento_agendado.Local_de_evento.Id_local_de_evento = 0;
                        }
                        else
                        {
                            Evento_agendado.Local_de_evento.Id_local_de_evento = (int)leitor["id_local_de_evento"];
                        }

                        Evento_agendado.Local_de_evento.Numero_local = leitor["numero_local"].ToString();
                        Evento_agendado.Local_de_evento.Status_local_evento = leitor["status_local_evento"].ToString();
                        Evento_agendado.Local_de_evento.Nome_do_local = leitor["nome_do_local"].ToString();

                        #endregion
                        int id_grupo_anterior = 0;
                        int id_convidado_anterior = 0;
                        do
                        {
                            if (Evento_agendado.Id_evento_agendado != (int)leitor["id_evento_agendado"])
                            {
                                break;
                            }

                            if ((leitor["id_grupo_de_participante"] is DBNull) == false && (int)leitor["id_grupo_de_participante"] != id_grupo_anterior)
                            {
                                Grupo_de_participante Grupo_de_participante = new Grupo_de_participante();
                                Grupo_de_participante.Id_grupo_de_participante = (int)leitor["id_grupo_de_participante"];
                                id_grupo_anterior = Grupo_de_participante.Id_grupo_de_participante;
                                Grupo_de_participante.Descricao_grupo_de_participante = leitor["descricao_grupo_de_participante"].ToString();
                                Grupo_de_participante.Nome_grupo_de_participante = leitor["nome_grupo_de_participante"].ToString();
                                Grupo_de_participante.Status_grupo_de_participante = leitor["status_grupo_de_participante"].ToString();
                                Evento_agendado.ListaGruposDoEventoAgendado.Add(Grupo_de_participante);

                            }
                            if ((leitor["id_convidado"] is DBNull) == false && id_convidado_anterior != (int)leitor["id_convidado"])
                            {
                                Convidado Convidado = new Convidado();
                                Convidado.Id_convidado = (int)leitor["id_convidado"];
                                id_convidado_anterior = Convidado.Id_convidado;

                                if (!Evento_agendado.ListConvidados.Contains(Convidado))
                                {
                                    Convidado.Nome_convidado = (string)leitor["nome_convidado"].ToString();
                                    Convidado.Status_convidado = leitor["status_convidado"].ToString();
                                    Convidado.Descricao_convidado = leitor["descricao_convidado"].ToString();
                                    Evento_agendado.ListConvidados.Add(Convidado);
                                }


                            }
                        } while (continuarLendo = leitor.Read());

                        listaDeEvento_agendado.Add(Evento_agendado);
                    }
                    leitor.Close();
                    comando.Dispose();
                    leitor = null;
                    return listaDeEvento_agendado;
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public long Inserir(Evento_agendado evento_agendado)
        {
            long ultimoID = 0;
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Evento_agendado (id_evento_agendado, data_publicacao, status_evento_agendado, data, descricao_evento_agendado, id_do_evento, horario_termino, horario_encontro, id_do_usuario_organizador, capacidade_participante, id_do_local_de_evento) values(@id_evento_agendado, @data_publicacao, @status_evento_agendado, @data, @descricao_evento_agendado, @id_do_evento, @horario_termino, @horario_encontro, @id_do_usuario_organizador, @capacidade_participante, @id_do_local_de_evento)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_evento_agendado", evento_agendado.Id_evento_agendado);
                comando.Parameters.AddWithValue("@data_publicacao", evento_agendado.Data_publicacao);
                comando.Parameters.AddWithValue("@status_evento_agendado", evento_agendado.Status_evento_agendado);
                comando.Parameters.AddWithValue("@data", evento_agendado.DataHora);
                comando.Parameters.AddWithValue("@descricao_evento_agendado", evento_agendado.Descricao_evento_agendado);
                comando.Parameters.AddWithValue("@id_do_evento", evento_agendado.Id_do_evento);
                comando.Parameters.AddWithValue("@horario_termino", evento_agendado.Horario_termino);
                comando.Parameters.AddWithValue("@horario_encontro", evento_agendado.Horario_encontro);
                comando.Parameters.AddWithValue("@id_do_usuario_organizador", evento_agendado.Id_do_usuario_organizador);
                comando.Parameters.AddWithValue("@capacidade_participante", evento_agendado.Capacidade_participante);
                comando.Parameters.AddWithValue("@id_do_local_de_evento", evento_agendado.Id_do_local_de_evento);
                comando.ExecuteNonQuery();
                ultimoID = comando.LastInsertedId;
                comando.Dispose();
                return ultimoID;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public void Atualiza(Evento_agendado evento_agendado, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Evento_agendado set id_evento_agendado = @id_evento_agendado, data_publicacao = @data_publicacao, status_evento_agendado = @status_evento_agendado, data = @data, descricao_evento_agendado = @descricao_evento_agendado, id_do_evento = @id_do_evento, horario_termino = @horario_termino, horario_encontro = @horario_encontro, id_do_usuario_organizador = @id_do_usuario_organizador, capacidade_participante = @capacidade_participante, id_do_local_de_evento = @id_do_local_de_evento where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_evento_agendado", evento_agendado.Id_evento_agendado);
                comando.Parameters.AddWithValue("@data_publicacao", evento_agendado.Data_publicacao);
                comando.Parameters.AddWithValue("@status_evento_agendado", evento_agendado.Status_evento_agendado);
                comando.Parameters.AddWithValue("@data", evento_agendado.DataHora);
                comando.Parameters.AddWithValue("@descricao_evento_agendado", evento_agendado.Descricao_evento_agendado);
                comando.Parameters.AddWithValue("@id_do_evento", evento_agendado.Id_do_evento);
                comando.Parameters.AddWithValue("@horario_termino", evento_agendado.Horario_termino);
                comando.Parameters.AddWithValue("@horario_encontro", evento_agendado.Horario_encontro);
                comando.Parameters.AddWithValue("@id_do_usuario_organizador", evento_agendado.Id_do_usuario_organizador);
                comando.Parameters.AddWithValue("@capacidade_participante", evento_agendado.Capacidade_participante);
                comando.Parameters.AddWithValue("@id_do_local_de_evento", evento_agendado.Id_do_local_de_evento);
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
                string sql = "Delete From Evento_agendado where " + condicao + "";
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