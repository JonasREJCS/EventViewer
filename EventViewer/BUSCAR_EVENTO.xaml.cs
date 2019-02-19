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
    /// Interaction logic for BUSCAR_EVENTO.xaml
    /// </summary>
    public partial class BUSCAR_EVENTO : Window
    {
        DAOEvento daoEvento;
        List<Evento> listaEvento;
        Evento_agendado evento_agendado;
        public BUSCAR_EVENTO()
        {
            InitializeComponent();
            CarregarobjetosPadrao();
        }

        public BUSCAR_EVENTO(Evento_agendado evento_agendado)
        {
            InitializeComponent();
            CarregarobjetosPadrao();
            this.evento_agendado = evento_agendado;
        }
        private void CarregarobjetosPadrao()
        {
            daoEvento = new DAOEvento(FabricaConexao.Conexao);
            listaEvento = new List<Evento>();
        }

        private void Buscar()
        {

            String condicao = "";
            if (String.IsNullOrEmpty(textBoxBuscar.Text.Trim()))
            {
                condicao = "id_evento > 0 ";
            }
            else if (checkBoxPesquisarParteNome.IsChecked == true)
            {
                condicao = "nome_evento like '%" + textBoxBuscar.Text.Trim() + "%'";
            }
            else
            {
                condicao = "nome_evento like '" + textBoxBuscar.Text.Trim() + "%'";
            }
            if (radioButtonCadastroAtivo.IsChecked == true)
            {
                condicao = condicao + " AND status_evento = '1'";
            }
            if (radioButtonCadastroInativo.IsChecked == true)
            {
                condicao = condicao + " AND status_evento = '0'";
            }
            dataGridEventos.ItemsSource = daoEvento.BuscarRegistro(condicao);

        }
        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    Buscar();
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }

            }
        }

        private void dataGridEventos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelecionaEvento();
        }
        private void SelecionaEvento()
        {
            if (this.evento_agendado == null)
            {
                if (dataGridEventos.SelectedIndex > -1)
                {

                    CADASTRO_EVENTO formEvento = new CADASTRO_EVENTO((Evento)dataGridEventos.SelectedItem);
                    this.Close();
                    formEvento.ShowDialog();

                }
                else
                {
                    CADASTRO_EVENTO formEvento = new CADASTRO_EVENTO();
                    this.Close();
                    formEvento.ShowDialog();
                }
            }
            else if (dataGridEventos.SelectedIndex > -1)
            {
                this.evento_agendado.Evento = (Evento)dataGridEventos.SelectedItem;
                this.evento_agendado.Id_do_evento = ((Evento)dataGridEventos.SelectedItem).Id_evento;
                AGENDAR_EVENTOS a = new AGENDAR_EVENTOS(this.evento_agendado);
                this.Close();
                a.ShowDialog();
            }
            else
            {
                AGENDAR_EVENTOS a = new AGENDAR_EVENTOS(this.evento_agendado);
                this.Close();
                a.ShowDialog();
            }
            

        }

        private void imageOK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelecionaEvento();
        }
    }
}
