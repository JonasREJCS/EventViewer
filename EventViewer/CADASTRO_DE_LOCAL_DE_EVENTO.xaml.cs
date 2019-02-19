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
using EventViewer.Modelo;
using EventViewer.DAO;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for CADASTRO_DE_LOCAL_DE_EVENTO.xaml
    /// </summary>
    public partial class CADASTRO_DE_LOCAL_DE_EVENTO : Window
    {
        DAOLocal_de_evento daoLocal_de_evento;
        DAOPais daoPais;
        DAOEstado daoEstado;
        DAOMunicipio daoMunicipio;
        DAOBairro daoBairro;
        DAOEndereco daoEndereco;
        List<Endereco> listaDeEndereco;

        Local_de_evento local_de_evento;

        public CADASTRO_DE_LOCAL_DE_EVENTO()
        {
            InitializeComponent();
        }
        public CADASTRO_DE_LOCAL_DE_EVENTO(Local_de_evento local_de_evento)
        {
            InitializeComponent();
            this.local_de_evento = local_de_evento;
            PopularDados();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void imagePais_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void imageConsultarLocalNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageConsultarLocalNormal.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_local_evento_aperte.fw.png", UriKind.Relative));
            BUSCA_DE_LOCAL buscadelocal = new BUSCA_DE_LOCAL();
            this.Close();
            buscadelocal.ShowDialog();

        }

        private void imageSalvar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void imageSalvar_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void imageSalvar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_aperte_salvar.fw.png", UriKind.Relative));
            Gravar();
        }
        private bool ValidarEndereco()
        {
            if (!String.IsNullOrEmpty(textBoxCep.Text.Trim()) || !String.IsNullOrEmpty(comboBoxPais.Text.Trim()) || !String.IsNullOrEmpty(comboBoxEstado.Text.Trim()) || !String.IsNullOrEmpty(comboBoxCidade.Text.Trim()) || !String.IsNullOrEmpty(comboBoxBairro.Text.Trim()) || !String.IsNullOrEmpty(comboBoxLogradouro.Text.Trim()) || !String.IsNullOrEmpty(textBoxNumeroResidencial.Text.Trim()) || !String.IsNullOrEmpty(textBoxComplemento.Text.Trim()))
            {
                if (String.IsNullOrEmpty(textBoxCep.Text.Trim()))
                {
                    textBoxCep.Focus();
                    return false;
                }
                else if (String.IsNullOrEmpty(comboBoxPais.Text.Trim()))
                {
                    comboBoxPais.Focus();
                    return false;
                }
                else if (String.IsNullOrEmpty(comboBoxEstado.Text.Trim()))
                {
                    comboBoxEstado.Focus();
                    return false;
                }
                else if (String.IsNullOrEmpty(comboBoxCidade.Text.Trim()))
                {
                    comboBoxCidade.Focus();
                    return false;
                }
                else if (String.IsNullOrEmpty(comboBoxBairro.Text.Trim()))
                {
                    comboBoxBairro.Focus();
                    return false;
                }
                else if (String.IsNullOrEmpty(comboBoxLogradouro.Text.Trim()))
                {
                    comboBoxLogradouro.Focus();
                    return false;
                }
                else
                {
                    SalvarNaMemoriaEnderecoFisico();
                    if (ValidarCampos())
                    {
                        GravarEndereco();
                    }
                    return true;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(textBoxEnderecoVirtual.Text.Trim()))
                {
                    textBoxEnderecoVirtual.Focus();
                    return false;
                }
                return true;
            }
        }
        private void imageSalvar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_normal_salvar.fw.png", UriKind.Relative));
        }

        private void SalvarNaMemoriaEnderecoFisico()
        {

            this.local_de_evento.Endereco.Cep = textBoxCep.Text.Trim();
            this.local_de_evento.Endereco.Logradouro = comboBoxLogradouro.Text.Trim().ToUpper();
            this.local_de_evento.Endereco.Status_endereco = "1";
            this.local_de_evento.Complemento_do_local = textBoxComplemento.Text.Trim().ToUpper();
            this.local_de_evento.Numero_local = textBoxNumeroResidencial.Text.Trim();
            this.local_de_evento.Endereco.Bairro.Nome_bairro = comboBoxBairro.Text.Trim().ToUpper();
            this.local_de_evento.Endereco.Bairro.Status_bairro = "1";


            this.local_de_evento.Endereco.Bairro.Municipio.Nome_municipio = comboBoxCidade.Text.Trim().ToUpper();
            this.local_de_evento.Endereco.Bairro.Municipio.Status_municipio = "1";


            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado = comboBoxEstado.Text.Trim().ToUpper();
            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Status_estado = "1";



            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais = comboBoxPais.Text.Trim().ToUpper();

            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Status_pais = "1";
        }
        private void GravarEndereco()
        {
            if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais == 0)
            {


                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = (int)daoPais.Inserir(this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais);
            }
            else
            {
                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais;
                daoPais.Atualiza(this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais, "id_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'");

            }
            if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado == 0)
            {
                this.local_de_evento.Endereco.Bairro.Municipio.Id_do_estado = (int)daoEstado.Inserir(this.local_de_evento.Endereco.Bairro.Municipio.Estado);
            }
            else
            {
                this.local_de_evento.Endereco.Bairro.Municipio.Id_do_estado = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado;
                daoEstado.Atualiza(this.local_de_evento.Endereco.Bairro.Municipio.Estado, "id_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado + "'");
            }

            if (this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio == 0)
            {
                this.local_de_evento.Endereco.Bairro.Id_do_municipio = (int)daoMunicipio.Inserir(this.local_de_evento.Endereco.Bairro.Municipio);
            }
            else
            {
                this.local_de_evento.Endereco.Bairro.Id_do_municipio = this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio;
                daoMunicipio.Atualiza(this.local_de_evento.Endereco.Bairro.Municipio, "id_municipio = '" + this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio + "'");
            }
            if (this.local_de_evento.Endereco.Bairro.Id_bairro == 0)
            {

                this.local_de_evento.Endereco.Id_do_bairro = (int)daoBairro.Inserir(this.local_de_evento.Endereco.Bairro);

            }
            else
            {
                this.local_de_evento.Endereco.Id_do_bairro = this.local_de_evento.Endereco.Bairro.Id_bairro;
                daoBairro.Atualiza(this.local_de_evento.Endereco.Bairro, "id_bairro = '" + this.local_de_evento.Endereco.Bairro.Id_bairro + "'");

            }

            if (this.local_de_evento.Endereco.Id_endereco == 0)
            {
                this.local_de_evento.Id_do_endereco = (int)daoEndereco.Inserir(this.local_de_evento.Endereco);
            }
            else
            {
                this.local_de_evento.Id_do_endereco = this.local_de_evento.Endereco.Id_endereco;
                daoEndereco.Atualiza(this.local_de_evento.Endereco, "id_endereco = '" + this.local_de_evento.Endereco.Id_endereco + "'");
            }
        }
        private void Gravar()
        {
            try
            {
                if (ValidarEndereco())
                {
                    this.local_de_evento.Endereco_virtual = textBoxEnderecoVirtual.Text.Trim();
                }
                else
                {
                    throw new Exception("Endereço inválido. Complete o cadastro do endereço.");
                }
                if (ValidarCampos())
                {
                    local_de_evento.Nome_do_local = textBoxNome.Text;
                    local_de_evento.Numero_local = textBoxNumeroResidencial.Text;
                    local_de_evento.Status_local_evento = comboBoxStatusLocalEvento.SelectedIndex.ToString();

                    if (local_de_evento.Id_local_de_evento == 0)
                    {

                        daoLocal_de_evento.Inserir(this.local_de_evento);

                    }
                    else
                    {
                        daoLocal_de_evento.Atualiza(this.local_de_evento, "id_local_de_evento = '" + this.local_de_evento.Id_local_de_evento + "'");
                    }
                    LimparCampos();

                }


            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }



        }

        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxNome.Focus();
                throw new Exception("Informe o nome do local");
            }
            return true;
        }
        private void LimparCampos()
        {
            this.local_de_evento = new Local_de_evento();
            textBoxId.Clear();
            textBoxNome.Clear();
            textBoxNumeroResidencial.Clear();
            textBoxEnderecoVirtual.Clear();
            textBoxComplemento.Clear();
            textBoxCep.Clear();
            ResetarPais();
            ResetarEstado();
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
            textBoxNome.Focus();
            comboBoxStatusLocalEvento.SelectedIndex = 1;
        }
        private void textBoxCep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VerificarCEP();
            }
        }
        private void VerificarCEP()
        {

            listaDeEndereco = daoEndereco.BuscarEnderecoCompleto("cep = '" + textBoxCep.Text.Trim() + "'");
            if (listaDeEndereco.Count > 0)
            {

                this.local_de_evento.Endereco = listaDeEndereco[0];
                PopularEndereco();

            }
        }
        private void PopularEndereco()
        {
            if (this.local_de_evento.Endereco != null)
            {
                textBoxCep.Text = this.local_de_evento.Endereco.Cep;
                comboBoxPais.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                comboBoxEstado.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado;
                comboBoxCidade.Text = this.local_de_evento.Endereco.Bairro.Municipio.Nome_municipio;
                comboBoxBairro.Text = this.local_de_evento.Endereco.Bairro.Nome_bairro;
                comboBoxLogradouro.Text = this.local_de_evento.Endereco.Logradouro;
            }
        }
        private void PopularDados()
        {
            textBoxNome.Text = this.local_de_evento.Nome_do_local;
            textBoxId.Text = this.local_de_evento.Id_local_de_evento.ToString();
            textBoxNumeroResidencial.Text = this.local_de_evento.Numero_local;
            textBoxComplemento.Text = this.local_de_evento.Complemento_do_local;
            textBoxEnderecoVirtual.Text = this.local_de_evento.Endereco_virtual;
            comboBoxStatusLocalEvento.SelectedIndex = Convert.ToInt32(this.local_de_evento.Status_local_evento);
            PopularEndereco();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (this.local_de_evento == null)
            {
                this.local_de_evento = new Local_de_evento();
            }
            daoLocal_de_evento = new DAOLocal_de_evento(FabricaConexao.Conexao);
            daoPais = new DAOPais(FabricaConexao.Conexao);
            daoEstado = new DAOEstado(FabricaConexao.Conexao);
            daoMunicipio = new DAOMunicipio(FabricaConexao.Conexao);
            daoBairro = new DAOBairro(FabricaConexao.Conexao);
            daoEndereco = new DAOEndereco(FabricaConexao.Conexao);
            listaDeEndereco = new List<Endereco>();
        }

        private void comboBoxPais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {
                    if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais == 0)
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

        private void comboBoxEstado_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado == 0)
                {

                    comboBoxEstado.Text = "";

                    string condicao = "nome_estado like '" + comboBoxEstado.Text.Trim() + "%'";

                    if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                    {
                        condicao += " and id_do_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
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
        private void ResetarCep()
        {
            textBoxCep.Text = "";
            this.local_de_evento.Endereco.Id_endereco = 0;
        }
        private void ResetarPais()
        {
            comboBoxPais.ItemsSource = null;
            comboBoxPais.Text = "";
            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais = 0;
        }

        private void ResetarEstado()
        {
            comboBoxEstado.ItemsSource = null;
            comboBoxEstado.Text = "";
            this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado = 0;
        }
        private void ResetarBairro()
        {
            comboBoxBairro.ItemsSource = null;
            comboBoxBairro.Text = "";
            this.local_de_evento.Endereco.Bairro.Id_bairro = 0;
        }
        private void ResetarLogradouro()
        {
            ResetarCep();
            comboBoxLogradouro.ItemsSource = null;
            comboBoxLogradouro.Text = "";
            this.local_de_evento.Endereco.Id_endereco = 0;
        }
        private void ResetarCidade()
        {
            comboBoxCidade.ItemsSource = null;
            comboBoxCidade.Text = "";
            this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio = 0;
        }

        private void comboBoxCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio == 0)
                    {

                        string condicao = "nome_municipio like '" + comboBoxCidade.Text.Trim() + "%'";
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                        //{
                        //    condicao += " and id_do_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                        //}
                        if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado != 0)
                        {
                            condicao += (" AND id_do_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado + "'");

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

        private void comboBoxBairro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {

                    if (this.local_de_evento.Endereco.Bairro.Id_bairro == 0)
                    {

                        string condicao = "nome_bairro like '" + comboBoxBairro.Text.Trim() + "%'";
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                        //{
                        //    condicao += " and id_do_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                        //}
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado != 0)
                        //{
                        //    condicao += (" AND id_do_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado + "'");

                        //}
                        if (this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio != 0)
                        {
                            condicao += " AND id_do_municipio = '" + this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio + "'";

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

        private void comboBoxLogradouro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.local_de_evento.Endereco.Id_endereco == 0)
                    {
                        string condicao = "logradouro like '" + comboBoxLogradouro.Text.Trim() + "%'";
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais != 0)
                        //{
                        //    condicao += " and id_do_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais + "'";
                        //}
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado != 0)
                        //{
                        //    condicao += (" AND id_do_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_estado + "'");

                        //}
                        //if (this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio != 0)
                        //{
                        //    condicao += " AND id_do_municipio = '" + this.local_de_evento.Endereco.Bairro.Municipio.Id_municipio + "'";

                        //}
                        if (this.local_de_evento.Endereco.Bairro.Id_bairro != 0)
                        {
                            condicao += " AND id_do_bairro ='" + this.local_de_evento.Endereco.Bairro.Id_bairro + "'";
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

        private void comboBoxPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPais.SelectedItem != null)
            {
                ResetarEstado();
                ResetarCidade();
                ResetarBairro();
                ResetarLogradouro();
                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais = (Pais)comboBoxPais.SelectedItem;
                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Id_pais;


            }
        }

        private void comboBoxEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.local_de_evento == null)
            {
                this.local_de_evento = new Local_de_evento();
            }
            if (comboBoxEstado.SelectedItem != null)
            {
                ResetarCidade();
                ResetarBairro();
                ResetarLogradouro();
                this.local_de_evento.Endereco.Bairro.Municipio.Estado = (Estado)comboBoxEstado.SelectedItem;
                this.local_de_evento.Endereco.Bairro.Municipio.Id_do_estado = ((Estado)comboBoxEstado.SelectedItem).Id_estado;
                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais = ((Estado)comboBoxEstado.SelectedItem).Pais;

                this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais = ((Estado)comboBoxEstado.SelectedItem).Id_do_pais;
                try
                {
                    this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = ((Estado)comboBoxEstado.SelectedItem).Pais.Nome_pais;

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

                    this.local_de_evento.Endereco.Bairro.Municipio = (Municipio)comboBoxCidade.SelectedItem;
                    this.local_de_evento.Endereco.Bairro.Municipio.Estado = daoEstado.BuscarRegistro("id_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Id_do_estado + "'")[0];
                    comboBoxEstado.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
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
                    this.local_de_evento.Endereco.Bairro = (Bairro)comboBoxBairro.SelectedItem;
                    this.local_de_evento.Endereco.Bairro.Municipio = daoMunicipio.BuscarRegistro("id_municipio = '" + this.local_de_evento.Endereco.Bairro.Id_do_municipio + "'")[0];
                    comboBoxCidade.Text = this.local_de_evento.Endereco.Bairro.Municipio.Nome_municipio;
                    this.local_de_evento.Endereco.Bairro.Municipio.Estado = daoEstado.BuscarRegistro("id_estado = '" + this.local_de_evento.Endereco.Bairro.Municipio.Id_do_estado + "'")[0];
                    comboBoxEstado.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais = daoPais.BuscarRegistro("id_pais = '" + this.local_de_evento.Endereco.Bairro.Municipio.Estado.Id_do_pais + "'")[0];
                    comboBoxPais.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
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
                    this.local_de_evento.Endereco = (Endereco)comboBoxLogradouro.SelectedItem;
                    textBoxCep.Text = this.local_de_evento.Endereco.Cep;
                    comboBoxBairro.Text = this.local_de_evento.Endereco.Bairro.Nome_bairro;
                    comboBoxCidade.Text = this.local_de_evento.Endereco.Bairro.Municipio.Nome_municipio;
                    comboBoxEstado.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Nome_estado;
                    comboBoxPais.Text = this.local_de_evento.Endereco.Bairro.Municipio.Estado.Pais.Nome_pais;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void labelPais_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarPais();
            ResetarEstado();
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
        }

        private void labelEstado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarEstado();

            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
        }

        private void labelCidade_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarCidade();
            ResetarBairro();
            ResetarLogradouro();
        }

        private void labelLogradouro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarLogradouro();
        }

        private void labelBairro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarBairro();
        }

        private void imageNovo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_aperte_novo.fw.png", UriKind.Relative));
            LimparCampos();
        }

        private void imageNovo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_normal_novo.fw.png", UriKind.Relative));
        }

        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<Local_de_evento> lista = daoLocal_de_evento.BuscarRegistro("id_local_de_evento = '" + textBoxId.Text.Trim() + "'");
                if (lista.Count > 0)
                {
                    this.local_de_evento = lista[0];
                    PopularDados();
                }
            }
        }

        private void imageExcluir_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_normal_excluir.fw.png", UriKind.Relative));
        }

        private void imageExcluirNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imageExcluir_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_aperte_excluir.fw.png", UriKind.Relative));
            Excluir();
        }

        private void Excluir()
        {
            if (this.local_de_evento != null)
            {
                if (this.local_de_evento.Id_local_de_evento != 0)
                {

                    if (MessageBox.Show("Deseja excluir mesmo o local '" + this.local_de_evento.Nome_do_local + "'", "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            daoLocal_de_evento.Excluir("id_local_de_evento = '" + this.local_de_evento.Id_local_de_evento + "'");
                            LimparCampos();
                        }
                        catch (Exception erro)
                        {

                            MessageBox.Show(erro.Message);
                        }

                    }

                }
            }

        }
        private void labelCep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetarCep();
        }

        private void textBoxNome_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void imageConsultarLocalNormal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageConsultarLocalNormal.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_local_evento_normal.fw.png", UriKind.Relative));
        }

        private void textBoxNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxEnderecoVirtual.Focus();
            }
        }

        private void textBoxEnderecoVirtual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxCep.Focus();
            }
        }


    }
}
