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

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for BUSCA_DE_LOCAL.xaml
    /// </summary>
    public partial class BUSCA_DE_LOCAL : Window
    {
        Evento_agendado evento_agendado;
        public BUSCA_DE_LOCAL()
        {
            InitializeComponent();

        }
        public BUSCA_DE_LOCAL(Evento_agendado evento_agendado)
        {
            InitializeComponent();
            this.evento_agendado = evento_agendado;
        }
        private void dataGridLocaisEventos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //Metodo para capturar se o mouse clickou
        private void imageOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageOk.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_aperte_Ok.fw.png", UriKind.Relative));
            SelecionarLocal();
        }
        //Metodo para capturar se o mouse soltou
        private void imageOk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageOk.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/botao_laranja_borda_normal_Ok.fw.png", UriKind.Relative));
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    DAOLocal_de_evento daoLocal = new DAOLocal_de_evento(FabricaConexao.Conexao);
                    String condicao;
                    if (checkBoxPesquisarParteNome.IsChecked == true)
                    {
                        condicao = "nome_do_local like '%" + textBoxBuscar.Text.Trim() + "%'";

                    }
                    else
                    {
                        condicao = "nome_do_local like '" + textBoxBuscar.Text.Trim() + "%'";

                    }

                    if (radioButtonCadastroAtivo.IsChecked == true)
                    {
                        condicao += " AND status_local_evento = '1'";
                    }
                    else if (radioButtonCadastroInativo.IsChecked == true)
                    {
                        condicao += " AND status_local_evento = '0'";
                    }
                    dataGridLocaisEventos.ItemsSource = daoLocal.BuscarRegistro(condicao);

                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridLocaisEventos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelecionarLocal();
        }
        private void SelecionarLocal()
        {
            if (dataGridLocaisEventos.SelectedIndex > -1 && this.evento_agendado == null)
            {
                CADASTRO_DE_LOCAL_DE_EVENTO cadastroLocal = new CADASTRO_DE_LOCAL_DE_EVENTO((Local_de_evento)dataGridLocaisEventos.SelectedItem);
                this.Close();
                cadastroLocal.ShowDialog();

            }
            else if (dataGridLocaisEventos.SelectedIndex > -1)
            {
                this.evento_agendado.Local_de_evento = ((Local_de_evento)dataGridLocaisEventos.SelectedItem);
                this.evento_agendado.Id_do_local_de_evento = this.evento_agendado.Local_de_evento.Id_local_de_evento;
            }
            AGENDAR_EVENTOS agendar_eventos = new AGENDAR_EVENTOS(this.evento_agendado);
            this.Close();
            agendar_eventos.ShowDialog();
        }
    }
}
