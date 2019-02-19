using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EventViewer.DAO;
using EventViewer.Modelo;
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;

using System.Collections.ObjectModel;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for CADASTRO_DE_PARTICIPANTES.xaml
    /// </summary>
    public partial class CADASTRO_DE_PARTICIPANTES : Window
    {
        DAOPessoa daoPessoa;
        DAOPais daoPais;
        DAOEstado daoEstado;
        DAOMunicipio daoMunicipio;
        DAOBairro daoBairro;
        DAOEndereco daoEndereco;

        Pessoa pessoa;



        ObservableCollection<Pessoa> listaDePessoa;
        List<Endereco> listaDeEndereco;
        List<Estado> listaDeEstado;
        public CADASTRO_DE_PARTICIPANTES()
        {
            InitializeComponent();
            textBoxNome.Focus();
            FabricaConexao.AbrirConexao();
            daoPessoa = new DAOPessoa(FabricaConexao.Conexao);
            daoEndereco = new DAOEndereco(FabricaConexao.Conexao);
            daoPais = new DAOPais(FabricaConexao.Conexao);
            daoEstado = new DAOEstado(FabricaConexao.Conexao);
            daoMunicipio = new DAOMunicipio(FabricaConexao.Conexao);
            daoBairro = new DAOBairro(FabricaConexao.Conexao);

            listaDePessoa = new ObservableCollection<Pessoa>();
            dataGridParticipantes.ItemsSource = listaDePessoa;
            this.pessoa = new Pessoa();

        }
        #region

        private void
            imageBotaoExcluirNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {

            imageBotaoExcluirNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_aperte_excluir.fw.png", UriKind.Relative));
            Excluir();
        }
        private void Excluir()
        {
            if (this.pessoa.Id_pessoa != 0)
            {
                if (MessageBox.Show("Deseja excluir o participante '" + this.pessoa.Nome_pessoa + "' ?", "Confirmar exlusão", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {


                    try
                    {

                        daoPessoa.Excluir("id_pessoa = '" + this.pessoa.Id_pessoa + "'");
                        listaDePessoa.Remove(this.pessoa);
                        LimparCampos();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.Message);
                    }

                }
            }
        }
        private void imageBotaoExcluirNormal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageBotaoExcluirNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_normal_excluir.fw.png", UriKind.Relative));
        }

        private void imageBotaoSalvarNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageBotaoSalvarNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_aperte_salvar.fw.png", UriKind.Relative));

            Gravar();
        }

        private void imageBotaoSalvarNormal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageBotaoSalvarNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_normal_salvar.fw.png", UriKind.Relative));
        }

        private void imageBotaoNovoNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageBotaoNovoNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_aperte_novo.fw.png", UriKind.Relative));
            LimparCampos();
        }

        private void imageBotaoNovoNormal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageBotaoNovoNormal.Source = new BitmapImage(new Uri("/Imagens/botao_verde_borda_normal_novo.fw.png", UriKind.Relative));
        }

        #endregion

        private void LimparCampos()
        {
            textBoxId.Clear();
            textBoxRg.Clear();
            textBoxCpf.Clear();
            textBoxNome.Clear();
            textBoxTelefoneResidencial.Clear();
            textBoxTelefoneMovel.Clear();
            textBoxEmail.Clear();
            textBoxCep.Clear();
            ResetarPais();
            ResetarEstado();
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
            textBoxComplemento.Clear();
            textBoxNumeroResidencial.Clear();
            textBoxNome.Focus();
            imageAvatar.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/AvatarFinal.fw.png", UriKind.Relative));
            this.pessoa = new Pessoa();
        }

        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxNome.Focus();
                throw new Exception("Informe o nome do participante");

            }
            return true;
        }

        private bool ValidarEndereco()
        {
            if (!String.IsNullOrEmpty(textBoxCep.Text) || !String.IsNullOrEmpty(comboBoxPais.Text) || !String.IsNullOrEmpty(comboBoxEstado.Text) || !String.IsNullOrEmpty(comboBoxCidade.Text) || !String.IsNullOrEmpty(comboBoxBairro.Text) || !String.IsNullOrEmpty(comboBoxLogradouro.Text) || !String.IsNullOrEmpty(textBoxNumeroResidencial.Text) || !String.IsNullOrEmpty(textBoxComplemento.Text))
            {
                if (String.IsNullOrEmpty(textBoxCep.Text))
                {
                    textBoxCep.Focus();
                    throw new Exception("Endereço inválido. Informe o CEP.");
                }
                else if (String.IsNullOrEmpty(comboBoxPais.Text))
                {
                    comboBoxPais.Focus();
                    throw new Exception("Endereço inválido. Informe o país.");
                }
                else if (String.IsNullOrEmpty(comboBoxEstado.Text))
                {
                    comboBoxEstado.Focus();
                    throw new Exception("Endereço inválido. Informe o estado.");
                }
                else if (String.IsNullOrEmpty(comboBoxCidade.Text))
                {
                    comboBoxCidade.Focus();
                    throw new Exception("Endereço inválido. Informe a cidade.");
                }
                else if (String.IsNullOrEmpty(comboBoxBairro.Text))
                {
                    comboBoxBairro.Focus();
                    throw new Exception("Endereço inválido. Informe o bairro.");
                }
                else if (String.IsNullOrEmpty(comboBoxLogradouro.Text))
                {
                    comboBoxLogradouro.Focus();
                    throw new Exception("Endereço inválido. Informe o logradouro.");
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private void Gravar()
        {
            try
            {

                if (ValidarCampos())
                {

                    if (ValidarEndereco())
                    {
                        #region
                        this.pessoa.Endereco.Cep = textBoxCep.Text;
                        this.pessoa.Endereco.Logradouro = comboBoxLogradouro.Text.ToUpper();
                        this.pessoa.Endereco.Status_endereco = "1";
                        this.pessoa.Complemento_residencia = textBoxComplemento.Text.ToUpper();
                        this.pessoa.Numero_residencia = textBoxNumeroResidencial.Text;
                        this.pessoa.Endereco.Bairro.Nome_bairro = comboBoxBairro.Text.ToUpper();
                        this.pessoa.Endereco.Bairro.Status_bairro = "1";


                        this.pessoa.Endereco.Bairro.Municipio.Nome_municipio = comboBoxCidade.Text.ToUpper();
                        this.pessoa.Endereco.Bairro.Municipio.Status_municipio = "1";


                        this.pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado = comboBoxEstado.Text.ToUpper();
                        this.pessoa.Endereco.Bairro.Municipio.Estado.Status_estado = "1";



                        this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais = comboBoxPais.Text.ToUpper();

                        this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Status_pais = "1";
                        if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais == 0)
                        {


                            this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais = (int)daoPais.Inserir(this.pessoa.Endereco.Bairro.Municipio.Estado.Pais);
                        }
                        else
                        {
                            this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais;
                            daoPais.Atualiza(this.pessoa.Endereco.Bairro.Municipio.Estado.Pais, "id_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'");

                        }
                        if (this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado == 0)
                        {
                            this.pessoa.Endereco.Bairro.Municipio.Id_do_estado = (int)daoEstado.Inserir(this.pessoa.Endereco.Bairro.Municipio.Estado);
                        }
                        else
                        {
                            this.pessoa.Endereco.Bairro.Municipio.Id_do_estado = this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado;
                            daoEstado.Atualiza(this.pessoa.Endereco.Bairro.Municipio.Estado, "id_estado = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado + "'");
                        }

                        if (this.pessoa.Endereco.Bairro.Municipio.Id_municipio == 0)
                        {
                            this.pessoa.Endereco.Bairro.Id_do_municipio = (int)daoMunicipio.Inserir(this.pessoa.Endereco.Bairro.Municipio);
                        }
                        else
                        {
                            this.pessoa.Endereco.Bairro.Id_do_municipio = this.pessoa.Endereco.Bairro.Municipio.Id_municipio;
                            daoMunicipio.Atualiza(this.pessoa.Endereco.Bairro.Municipio, "id_municipio = '" + this.pessoa.Endereco.Bairro.Municipio.Id_municipio + "'");
                        }
                        if (this.pessoa.Endereco.Bairro.Id_bairro == 0)
                        {

                            this.pessoa.Endereco.Id_do_bairro = (int)daoBairro.Inserir(this.pessoa.Endereco.Bairro);

                        }
                        else
                        {
                            this.pessoa.Endereco.Id_do_bairro = this.pessoa.Endereco.Bairro.Id_bairro;
                            daoBairro.Atualiza(this.pessoa.Endereco.Bairro, "id_bairro = '" + this.pessoa.Endereco.Bairro.Id_bairro + "'");

                        }

                        if (this.pessoa.Endereco.Id_endereco == 0)
                        {
                            this.pessoa.Id_do_endereco = (int)daoEndereco.Inserir(this.pessoa.Endereco);
                        }
                        else
                        {
                            this.pessoa.Id_do_endereco = this.pessoa.Endereco.Id_endereco;
                            daoEndereco.Atualiza(this.pessoa.Endereco, "id_endereco = '" + this.pessoa.Endereco.Id_endereco + "'");
                        }
                        #endregion
                    }
                    pessoa.Identidade = textBoxRg.Text;
                    pessoa.Cpf = textBoxCpf.Text;
                    pessoa.Nome_pessoa = textBoxNome.Text;
                    pessoa.Telefone_residencial = textBoxTelefoneResidencial.Text;
                    pessoa.Telefone_movel = textBoxTelefoneMovel.Text;
                    pessoa.Email = textBoxEmail.Text;
                    pessoa.Status_pessoa = comboBoxStatusParticipante.SelectedIndex.ToString();

                    if (pessoa.Id_pessoa == 0)
                    {

                        this.pessoa.Id_pessoa = (int)daoPessoa.Inserir(this.pessoa);
                        
                        listaDePessoa.Add(this.pessoa);
                    }
                    else
                    {
                        daoPessoa.Atualiza(this.pessoa, "id_pessoa = '" + this.pessoa.Id_pessoa + "'");
                        listaDePessoa.Remove(this.pessoa);
                        listaDePessoa.Add(this.pessoa);
                    }
                    LimparCampos();

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

        private void textBoxCep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                listaDeEndereco = daoEndereco.BuscarEnderecoCompleto("cep = '" + textBoxCep.Text.Trim() + "'");
                if (listaDeEndereco.Count > 0)
                {

                    this.pessoa.Endereco = listaDeEndereco[0];
                    PopularEndereco();

                }
                comboBoxPais.Focus();
            }
        }

        private void PopularEndereco()
        {
            if (this.pessoa.Endereco.Id_endereco != 0)
            {

                textBoxCep.Text = this.pessoa.Endereco.Cep;
                comboBoxPais.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                comboBoxEstado.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado;
                comboBoxCidade.Text = this.pessoa.Endereco.Bairro.Municipio.Nome_municipio;
                comboBoxBairro.Text = this.pessoa.Endereco.Bairro.Nome_bairro;
                comboBoxLogradouro.Text = this.pessoa.Endereco.Logradouro;

            }
        }
        private void PopularDadosPessoa()
        {
            textBoxNome.Text = pessoa.Nome_pessoa;
            textBoxNumeroResidencial.Text = pessoa.Numero_residencia;
            textBoxRg.Text = pessoa.Identidade;
            textBoxTelefoneMovel.Text = pessoa.Telefone_movel;
            textBoxTelefoneResidencial.Text = pessoa.Telefone_residencial;
            textBoxEmail.Text = pessoa.Email;
            textBoxCpf.Text = pessoa.Cpf;
            textBoxComplemento.Text = pessoa.Complemento_residencia;
            textBoxNumeroResidencial.Text = pessoa.Numero_residencia;
            comboBoxStatusParticipante.SelectedIndex = Convert.ToInt32(pessoa.Status_pessoa);
            if (this.pessoa.Caminho_foto != null)
            {
                imageAvatar.Source = LoadImage(this.pessoa.Caminho_foto);
            }
            PopularEndereco();
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listaDePessoa = daoPessoa.BuscarRegistro("id_pessoa = '" + textBoxId.Text.Trim() + "'");
                if (listaDePessoa.Count > 0)
                {
                    this.pessoa = listaDePessoa[0];
                    PopularDadosPessoa();


                }
            }
        }


        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string condicao;
                    if (checkBoxPesquisarParteNome.IsChecked == true)
                    {
                        condicao = "nome_pessoa like '%" + textBoxBuscar.Text.Trim() + "%'";

                    }
                    else
                    {
                        condicao = "nome_pessoa like '" + textBoxBuscar.Text.Trim() + "%'";

                    }

                    if (radioButtonCadastroAtivo.IsChecked == true)
                    {
                        condicao += " AND status_pessoa = '1'";
                    }
                    else if (radioButtonCadastroInativo.IsChecked == true)
                    {
                        condicao += " AND status_pessoa = '0'";
                    }
                    listaDePessoa = daoPessoa.BuscarRegistro(condicao);

                    dataGridParticipantes.ItemsSource = listaDePessoa;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridParticipantes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridParticipantes.SelectedIndex > -1 && dataGridParticipantes.SelectedIndex < listaDePessoa.Count)
            {
                this.pessoa = (Pessoa)dataGridParticipantes.SelectedItem;

                PopularDadosPessoa();
            }
        }

        private void comboBoxPais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {
                    if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais == 0)
                    {
                        comboBoxPais.ItemsSource = daoPais.BuscarRegistro("nome_pais like '" + comboBoxPais.Text + "%'");


                        if (comboBoxPais.Items.Count > 0)
                        {

                            comboBoxPais.Text = "Selecione o resultado da pesquisa -->";

                        }
                    }
                    else
                    {
                        comboBoxEstado.Focus();
                    }
                    if (!String.IsNullOrEmpty(comboBoxPais.Text.Trim()) && comboBoxPais.Items.Count == 0)
                    {
                        comboBoxEstado.Focus();
                    }
                }

                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }

        }

        private void comboBoxPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais == null)
            //{
            //    this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = new Pais();
            //}
            if (this.pessoa == null)
            {
                this.pessoa = new Pessoa();
            }
            if (comboBoxPais.SelectedItem != null)
            {
                ResetarEstado();
                ResetarCidade();
                ResetarBairro();
                ResetarLogradouro();
                this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = (Pais)comboBoxPais.SelectedItem;
                this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais;


            }
        }

        private void comboBoxEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado == 0)
                {

                    if (listaDeEstado == null)
                    {
                        listaDeEstado = new List<Estado>();
                    }
                    string condicao = "nome_estado like '" + comboBoxEstado.Text.Trim() + "%'";

                    if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                    {
                        condicao += " and id_do_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                    }
                    comboBoxEstado.ItemsSource = daoEstado.BuscarRegistro(condicao);

                    if (comboBoxEstado.Items.Count > 0)
                    {
                        comboBoxEstado.Text = "Selecione um resultado ->";
                    }

                }
                else
                {
                    comboBoxCidade.Focus();
                }
                if (!String.IsNullOrEmpty(comboBoxEstado.Text.Trim()) && comboBoxEstado.Items.Count == 0)
                {
                    comboBoxCidade.Focus();
                }
            }
        }

        private void comboBoxEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.pessoa == null)
            {
                this.pessoa = new Pessoa();
            }
            if (comboBoxEstado.SelectedItem != null)
            {
                ResetarCidade();
                ResetarBairro();
                ResetarLogradouro();
                this.pessoa.Endereco.Bairro.Municipio.Estado = (Estado)comboBoxEstado.SelectedItem;
                this.pessoa.Endereco.Bairro.Municipio.Id_do_estado = ((Estado)comboBoxEstado.SelectedItem).Id_estado;
                this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = ((Estado)comboBoxEstado.SelectedItem).Pais;

                this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais = ((Estado)comboBoxEstado.SelectedItem).Id_do_pais;
                try
                {
                    this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = ((Estado)comboBoxEstado.SelectedItem).Pais.Nome_pais;

                }
                catch (Exception erro)
                {



                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.pessoa.Endereco.Bairro.Municipio.Id_municipio == 0)
                    {

                        string condicao = "nome_municipio like '" + comboBoxCidade.Text.Trim() + "%'";
                        //if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                        //{
                        //    condicao += " and id_do_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                        //}
                        if (this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado != 0)
                        {
                            condicao += (" AND id_do_estado = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado + "'");

                        }
                        comboBoxCidade.ItemsSource = daoMunicipio.BuscarRegistro(condicao);
                        if (comboBoxCidade.Items.Count > 0)
                        {
                            comboBoxCidade.Text = "Selecione um resultado ->";
                        }
                    }
                    else
                    {
                        comboBoxBairro.Focus();
                    }
                    if (!String.IsNullOrEmpty(comboBoxCidade.Text.Trim()) && comboBoxCidade.Items.Count == 0)
                    {
                        comboBoxBairro.Focus();
                    }
                }
                catch (Exception erro)
                {


                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxCidade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCidade.SelectedItem != null)
            {
                try
                {
                    ResetarBairro();
                    ResetarLogradouro();

                    this.pessoa.Endereco.Bairro.Municipio = (Municipio)comboBoxCidade.SelectedItem;
                    this.pessoa.Endereco.Bairro.Municipio.Estado = daoEstado.BuscarRegistro("id_estado = '" + this.pessoa.Endereco.Bairro.Municipio.Id_do_estado + "'")[0];
                    comboBoxEstado.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxBairro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.pessoa.Endereco.Bairro.Id_bairro == 0)
                    {

                        string condicao = "nome_bairro like '" + comboBoxBairro.Text.Trim() + "%'";
                        //if (this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                        //{
                        //    condicao += " and id_do_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                        //}
                        //if (this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado != 0)
                        //{
                        //    condicao += (" AND id_do_estado = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado + "'");

                        //}
                        if (this.pessoa.Endereco.Bairro.Municipio.Id_municipio != 0)
                        {
                            condicao += " AND id_do_municipio = '" + this.pessoa.Endereco.Bairro.Municipio.Id_municipio + "'";

                        }
                        comboBoxBairro.ItemsSource = daoBairro.BuscarRegistro(condicao);
                        if (comboBoxBairro.Items.Count > 0)
                        {
                            comboBoxBairro.Text = "Selecione um resultado ->";
                        }
                    }
                    else
                    {
                        comboBoxLogradouro.Focus();
                    }
                    if (!String.IsNullOrEmpty(comboBoxBairro.Text.Trim()) && comboBoxBairro.Items.Count == 0)
                    {
                        comboBoxLogradouro.Focus();
                    }
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxBairro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxBairro.SelectedItem != null)
            {
                try
                {
                    ResetarLogradouro();
                    this.pessoa.Endereco.Bairro = (Bairro)comboBoxBairro.SelectedItem;
                    this.pessoa.Endereco.Bairro.Municipio = daoMunicipio.BuscarRegistro("id_municipio = '" + this.pessoa.Endereco.Bairro.Id_do_municipio + "'")[0];
                    comboBoxCidade.Text = this.pessoa.Endereco.Bairro.Municipio.Nome_municipio;
                    this.pessoa.Endereco.Bairro.Municipio.Estado = daoEstado.BuscarRegistro("id_estado = '" + this.pessoa.Endereco.Bairro.Municipio.Id_do_estado + "'")[0];
                    comboBoxEstado.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    this.pessoa.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.pessoa.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxLogradouro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.pessoa.Endereco.Id_endereco == 0)
                    {

                        string condicao = "logradouro like '" + comboBoxLogradouro.Text.Trim() + "%'";
                        if (this.pessoa.Endereco.Bairro.Id_bairro != 0)
                        {
                            condicao += " AND id_do_bairro ='" + this.pessoa.Endereco.Bairro.Id_bairro + "'";
                        }

                        comboBoxLogradouro.ItemsSource = daoEndereco.BuscarEnderecoCompleto("logradouro like '" + comboBoxLogradouro.Text.Trim() + "%'");

                        if (comboBoxLogradouro.Items.Count > 0)
                        {
                            comboBoxLogradouro.Text = "Selecione um resultado >-";
                        }
                    }
                    else
                    {
                        textBoxComplemento.Focus();
                    }
                    if (!String.IsNullOrEmpty(comboBoxLogradouro.Text.Trim()) && comboBoxLogradouro.Items.Count == 0)
                    {
                        textBoxComplemento.Focus();
                    }
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxLogradouro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxLogradouro.SelectedItem != null)
            {
                try
                {
                    this.pessoa.Endereco = (Endereco)comboBoxLogradouro.SelectedItem;
                    textBoxCep.Text = this.pessoa.Endereco.Cep;
                    comboBoxBairro.Text = this.pessoa.Endereco.Bairro.Nome_bairro;
                    comboBoxCidade.Text = this.pessoa.Endereco.Bairro.Municipio.Nome_municipio;
                    comboBoxEstado.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    comboBoxPais.Text = this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void comboBoxPais_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void labelPais_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void labelPais_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ResetarPais();
            ResetarEstado();
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();

        }
        private void ResetarPais()
        {
            comboBoxPais.ItemsSource = null;
            comboBoxPais.Text = "";
            this.pessoa.Endereco.Bairro.Municipio.Estado.Pais.Id_pais = 0;
        }
        private void labelEstado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarEstado();
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
        }
        private void ResetarEstado()
        {
            comboBoxEstado.ItemsSource = null;
            comboBoxEstado.Text = "";
            this.pessoa.Endereco.Bairro.Municipio.Estado.Id_estado = 0;
        }
        private void labelCidade_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
        }
        private void ResetarCidade()
        {
            comboBoxCidade.ItemsSource = null;
            comboBoxCidade.Text = "";
            this.pessoa.Endereco.Bairro.Municipio.Id_municipio = 0;
        }

        private void labelBairro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarBairro();
            ResetarLogradouro();
        }
        private void ResetarBairro()
        {
            comboBoxBairro.ItemsSource = null;
            comboBoxBairro.Text = "";
            this.pessoa.Endereco.Bairro.Id_bairro = 0;
        }
        private void labelLogradouro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarLogradouro();
        }
        private void ResetarLogradouro()
        {
            comboBoxLogradouro.ItemsSource = null;
            comboBoxLogradouro.Text = "";
            this.pessoa.Endereco.Id_endereco = 0;
        }

        private void labelCep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void ResetarCep()
        {
            textBoxCep.Text = "";
            this.pessoa.Endereco.Id_endereco = 0;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            SalvarImagem((String[])e.Data.GetData(DataFormats.FileDrop));

        }

        private void SalvarImagem(string[] arquivo)
        {
            try
            {
                String caminho = arquivo[0];
                // cria uma imagem na memória a partir do caminho do arquivo arrastado
                imageAvatar.Source = new BitmapImage(new Uri(caminho, UriKind.Absolute));

                string FileName = caminho;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                this.pessoa.Caminho_foto = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();

                // imageLogoEvento.Source = new BitmapImage(new Uri(caminho, UriKind.Relative));
                // atribui esta imagem ao pictureBox
                //pbClassificacao.Image = imagem;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void dataGridParticipantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imageAvatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                SalvarImagem(new String[] { dlg.FileName });

            }

        }

        private void textBoxTelefoneResidencial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxRg.Focus();
            }
        }

        private void textBoxNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxTelefoneResidencial.Focus();
            }
        }

        private void textBoxRg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxCpf.Focus();
            }
        }

        private void textBoxRg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                textBoxTelefoneMovel.Focus();
            }
        }

        private void textBoxTelefoneMovel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxEmail.Focus();
            }
        }

        private void textBoxComplemento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxNumeroResidencial.Focus();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxCep.Focus();
            }
        }

        private void imageBairro_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}

