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
using System.Collections.ObjectModel;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for CADASTRO_DE_CONVIDADO.xaml
    /// </summary>
    public partial class CADASTRO_DE_CONVIDADO : Window
    {
        DAOConvidado daoConvidado;
        DAOTipo_de_convidado daoTipo_de_convidado;
        Convidado convidado;
        ObservableCollection<Convidado> listConvidado;

        public CADASTRO_DE_CONVIDADO()
        {
            InitializeComponent();
            CarregarObjetosPadrao();
        }
        private void CarregarObjetosPadrao()
        {
            daoConvidado = new DAOConvidado(FabricaConexao.Conexao);
            daoTipo_de_convidado = new DAOTipo_de_convidado(FabricaConexao.Conexao);
            if (this.convidado == null)
            {
                convidado = new Convidado();
            }
            listConvidado = new ObservableCollection<Convidado>();
            dataGridConvidado.ItemsSource = listConvidado;
        }

        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxNome.Focus();
                throw new Exception("Informe o nome do convidado");
            }
            return true;
        }

        private void Gravar()
        {
            try
            {
                if (ValidarCampos())
                {

                    this.convidado.Nome_convidado = textBoxNome.Text.Trim();
                    this.convidado.Status_convidado = comboBoxStatus.SelectedIndex.ToString();
                    this.convidado.Descricao_convidado = textBoxDescricaoConvidado.Text.Trim();

                    if (this.convidado.Id_convidado == 0)
                    {
                        convidado.Id_convidado = (int)daoConvidado.Inserir(convidado);
                        listConvidado.Add(convidado);
                    }
                    else
                    {
                        daoConvidado.Atualiza(convidado, "id_convidado = '" + this.convidado.Id_convidado + "'");
                        listConvidado.Remove(convidado);
                        listConvidado.Add(convidado);
                    }
                    Limpar();
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void imageSalvar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoSalvarApertado();
            Gravar();
        }
        private void Limpar()
        {
            this.convidado = new Convidado();
            textBoxId.Text = "";
            textBoxDescricaoConvidado.Text = "";
            textBoxNome.Text = "";
            comboBoxStatus.SelectedIndex = 1;
            textBoxNome.Focus();
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string condicao = "nome_convidado like '";
                    if (CheckBoxPesquisaQualquerParte.IsChecked == true)
                    {
                        condicao += "%" + textBoxBuscar.Text.Trim() + "%'";
                    }
                    else
                    {
                        condicao += textBoxBuscar.Text.Trim() + "%' ";
                    }
                    if (RadioButtonAtivos.IsChecked == true)
                    {
                        condicao += " AND status_convidado = '1'";
                    }
                    else if (RadioButtonInativos.IsChecked == true)
                    {
                        condicao += " AND status_convidado = '0'";
                    }

                    listConvidado = daoConvidado.BuscarRegistro(condicao);
                    dataGridConvidado.ItemsSource = listConvidado;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridConvidado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridConvidado.SelectedIndex > -1 && dataGridConvidado.SelectedIndex < dataGridConvidado.Items.Count)
            {
                this.convidado = (Convidado)dataGridConvidado.SelectedItem;
                PopularCampos();
            }
        }
        private void PopularCampos()
        {
            textBoxId.Text = this.convidado.Id_convidado.ToString();
            textBoxNome.Text = this.convidado.Nome_convidado;
            textBoxDescricaoConvidado.Text = this.convidado.Descricao_convidado;
            comboBoxStatus.SelectedIndex = Convert.ToInt32(this.convidado.Status_convidado);
        }

        private void imageBotaoNovo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoNovoApertado();
            Limpar();

        }
        private void TrocarImagemBotaoNovoApertado()
        {
            imageBotaoNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_aperte_novo.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoNovoSolto()
        {
            imageBotaoNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_normal_novo.fw.png", UriKind.Relative));
        }
        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listConvidado = daoConvidado.BuscarRegistro("id_convidado = '" + textBoxId.Text.Trim() + "'");
                if (listConvidado.Count > 0)
                {
                    this.convidado = listConvidado[0];
                    PopularCampos();
                }

            }
        }

        private void imageExcluir_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Excluir();
        }
        private void Excluir()
        {
            if (this.convidado.Id_convidado != 0)
            {
                if (MessageBox.Show("Deseja excluir o convidado '" + this.convidado.Nome_convidado + "' ?", "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        daoConvidado.Excluir("id_convidado = '" + this.convidado.Id_convidado + "'");
                        if (this.listConvidado.Contains(this.convidado))
                        {
                            this.listConvidado.Remove(this.convidado);
                            dataGridConvidado.ItemsSource = this.listConvidado;
                        }

                        Limpar();
                    }
                    catch (Exception erro)
                    {

                        MessageBox.Show(erro.Message);
                    }
                }
            }

        }
        private void imageBotaoNovo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoNovoSolto();
        }

        private void imageSalvar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoSalvarSolto();
        }
        private void TrocarImagemBotaoSalvarApertado()
        {
            imageSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_aperte_salvar.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoSalvarSolto()
        {
            imageSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_normal_salvar.fw.png", UriKind.Relative));
        }

        private void TrocarImagemBotaoExcluirApertado()
        {
            imageExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_aperte_excluir.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoExcluirSolto()
        {
            imageExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_rosa_borda_normal_excluir.fw.png", UriKind.Relative));
        }
    }
}
