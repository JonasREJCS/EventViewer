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
    /// Classe gerada para a tabela : Pessoa.
    /// </summary>
    class DAOPessoa
    {
        private MySqlConnection conexao;

        private MySqlCommand comando;
        // método para conectar com o bcdd (Banco de dados)
        public DAOPessoa(MySqlConnection conexao)
        {
            this.conexao = conexao;
            comando = new MySqlCommand();
            comando.Connection = this.conexao;
        }
        //método para buscar dados já registrados, da tabela Pessoa no bcdd
        public ObservableCollection<Pessoa> BuscarRegistro(string whereClause)
        {

            ObservableCollection<Pessoa> listaDePessoa = new ObservableCollection<Pessoa>();
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();
            //string SQL = "Select * From Pessoa where " + whereClause + " LIMIT 1000 ";

            string SQL = "select * from view_Participante_Completo where " + whereClause + " LIMIT 500";
            comando.CommandText = SQL;

            MySqlDataReader leitor = comando.ExecuteReader();
            try
            {
                while (leitor.Read())
                {
                    Pessoa Pessoa = new Pessoa();
                    if ((leitor["caminho_foto"] is DBNull) == false)
                    {
                        Pessoa.Caminho_foto = (byte[])leitor["caminho_foto"];
                    }

                    Pessoa.Status_pessoa = leitor["status_pessoa"].ToString();
                    Pessoa.Cpf = leitor["cpf"].ToString();
                    Pessoa.Telefone_residencial = leitor["telefone_residencial"].ToString();
                    if ((leitor["id_do_usuario"] is DBNull) == false)
                    {
                        Pessoa.Id_do_usuario = (int)leitor["id_do_usuario"];
                    }
                    Pessoa.Telefone_movel = leitor["telefone_movel"].ToString();

                    Pessoa.Nome_pessoa = leitor["nome_pessoa"].ToString();

                    Pessoa.Id_pessoa = (int)leitor["id_pessoa"];

                    Pessoa.Identidade = leitor["identidade"].ToString();

                    Pessoa.Email = leitor["email"].ToString();

                    if ((leitor["id_do_endereco"] is DBNull) == false)
                    {
                        Pessoa.Id_do_endereco = (int)leitor["id_do_endereco"];

                        Pessoa.Numero_residencia = leitor["numero_residencia"].ToString();


                        Pessoa.Complemento_residencia = leitor["complemento_residencia"].ToString();
                        Pessoa.Endereco.Cep = leitor["cep"].ToString();
                        Pessoa.Endereco = new Endereco();

                        if ((leitor["id_endereco"] is DBNull) == false)
                        {
                            Pessoa.Endereco.Id_endereco = (int)leitor["id_endereco"];

                        }
                        Pessoa.Endereco.Logradouro = leitor["logradouro"].ToString();
                        Pessoa.Endereco.Status_endereco = leitor["status_endereco"].ToString();
                        if ((leitor["id_do_endereco"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Id_do_bairro = (int)leitor["id_do_endereco"];
                        }
                        if ((leitor["id_bairro"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Id_bairro = (int)leitor["id_bairro"];
                        }
                        Pessoa.Endereco.Bairro.Nome_bairro = leitor["nome_bairro"].ToString();
                        Pessoa.Endereco.Bairro.Status_bairro = leitor["status_bairro"].ToString();
                        if ((leitor["id_do_municipio"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Id_do_municipio = (int)leitor["id_do_municipio"];
                        }
                        if ((leitor["id_municipio"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Municipio.Id_municipio = (int)leitor["id_municipio"];
                        }
                        Pessoa.Endereco.Bairro.Municipio.Nome_municipio = leitor["nome_municipio"].ToString();
                        Pessoa.Endereco.Bairro.Municipio.Status_municipio = leitor["status_municipio"].ToString();
                        if ((leitor["id_do_estado"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Municipio.Id_do_estado = (int)leitor["id_do_estado"];
                        }
                        if ((leitor["id_estado"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Municipio.Estado.Id_estado = (int)leitor["id_estado"];
                        } Pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado = leitor["nome_estado"].ToString();
                        Pessoa.Endereco.Bairro.Municipio.Estado.Status_estado = leitor["status_estado"].ToString();
                        if ((leitor["id_do_pais"] is DBNull) == false)
                        {
                            Pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais = (int)leitor["id_do_pais"];
                        }
                        if ((leitor["id_pais"] is DBNull) == false)
                        {

                            Pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais = (int)leitor["id_pais"];

                        } Pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais = leitor["nome_pais"].ToString();
                        Pessoa.Endereco.Bairro.Municipio.Estado.Pais.Status_pais = leitor["status_pais"].ToString();
                    }



                    listaDePessoa.Add(Pessoa);
                }
                leitor.Close();
                comando.Dispose();
                leitor = null;

                return listaDePessoa;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        //método para salvar novos dados na tabela Pessoa, no bcdd
        public long Inserir(Pessoa pessoa)
        {
            try
            {
                long id;
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Insert into Pessoa (caminho_foto, status_pessoa, cpf, telefone_residencial, complemento_residencia, id_do_usuario, telefone_movel, nome_pessoa, id_do_endereco, numero_residencia, identidade, email) values (@caminho_foto, @status_pessoa, @cpf, @telefone_residencial, @complemento_residencia, @id_do_usuario, @telefone_movel, @nome_pessoa, @id_do_endereco, @numero_residencia, @identidade, @email)";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@caminho_foto", pessoa.Caminho_foto);
                comando.Parameters.AddWithValue("@status_pessoa", pessoa.Status_pessoa);
                comando.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                comando.Parameters.AddWithValue("@telefone_residencial", pessoa.Telefone_residencial);
                comando.Parameters.AddWithValue("@complemento_residencia", pessoa.Complemento_residencia);
                if (pessoa.Id_do_usuario != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_usuario", pessoa.Id_do_usuario);
                }
                else
                {

                    comando.Parameters.AddWithValue("@id_do_usuario", DBNull.Value);
                }

                comando.Parameters.AddWithValue("@telefone_movel", pessoa.Telefone_movel);
                comando.Parameters.AddWithValue("@nome_pessoa", pessoa.Nome_pessoa);
                if (pessoa.Id_do_endereco != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_endereco", pessoa.Id_do_endereco);
                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_endereco", DBNull.Value);
                }
                comando.Parameters.AddWithValue("@numero_residencia", pessoa.Numero_residencia);
                comando.Parameters.AddWithValue("@identidade", pessoa.Identidade);
                comando.Parameters.AddWithValue("@email", pessoa.Email);
                comando.ExecuteNonQuery();
                id = comando.LastInsertedId;
                comando.Dispose();
                return id;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para modificar dados registrados na tabela Pessoa no bcdd
        public void Atualiza(Pessoa pessoa, string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string SQL = "Update Pessoa set caminho_foto = @caminho_foto, status_pessoa = @status_pessoa, cpf = @cpf, telefone_residencial = @telefone_residencial, complemento_residencia = @complemento_residencia, id_do_usuario = @id_do_usuario, telefone_movel = @telefone_movel, nome_pessoa = @nome_pessoa, id_do_endereco = @id_do_endereco, id_pessoa = @id_pessoa, numero_residencia = @numero_residencia, identidade = @identidade, email = @email where " + condicao + "";
                comando.CommandText = SQL;
                comando.CommandType = CommandType.Text;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@caminho_foto", pessoa.Caminho_foto);
                comando.Parameters.AddWithValue("@status_pessoa", pessoa.Status_pessoa);
                comando.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                comando.Parameters.AddWithValue("@telefone_residencial", pessoa.Telefone_residencial);
                comando.Parameters.AddWithValue("@complemento_residencia", pessoa.Complemento_residencia);
                if (pessoa.Id_do_usuario != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_usuario", pessoa.Id_do_usuario);
                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_usuario", DBNull.Value);

                }
                comando.Parameters.AddWithValue("@telefone_movel", pessoa.Telefone_movel);
                comando.Parameters.AddWithValue("@nome_pessoa", pessoa.Nome_pessoa);
                if (pessoa.Endereco.Id_endereco != 0)
                {
                    comando.Parameters.AddWithValue("@id_do_endereco", pessoa.Endereco.Id_endereco);
                }
                else
                {
                    comando.Parameters.AddWithValue("@id_do_endereco", DBNull.Value);
                }

                comando.Parameters.AddWithValue("@id_pessoa", pessoa.Id_pessoa);
                comando.Parameters.AddWithValue("@numero_residencia", pessoa.Numero_residencia);
                comando.Parameters.AddWithValue("@identidade", pessoa.Identidade);
                comando.Parameters.AddWithValue("@email", pessoa.Email);
                comando.ExecuteNonQuery();
                conexao.Close();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        //método para apagar dados registrados na tabela Pessoa no bcdd
        public void Excluir(string condicao)
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                string sql = "Delete From Pessoa where " + condicao + "";
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