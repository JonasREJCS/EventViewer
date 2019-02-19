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
using MySql.Data.MySqlClient;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for CONSULTAR_CONVIDADO.xaml
    /// </summary>
    public partial class CONSULTAR_CONVIDADO : Window
    {
        Evento_agendado evento_agendado;//criado papel em branco
        DAOConvidado DaoConvidado;//a variável, DaoEvento, vai acessar os métodos buscar, atualizar, inserir, excluir, etc.
        DAOConvidados_do_evento daoConvidados_do_evento;
        public CONSULTAR_CONVIDADO(Evento_agendado eventoagendado) //eventoagendado é o conteúdo que tem no papel original(matriz)
        {
            InitializeComponent();
            textBoxBuscar.Focus();
            this.evento_agendado = eventoagendado;//this é essa classe (CONSULTAR_CONVIDADO) e evento_agendado(papel branco) recebe o conteúdo(eventoagendado) do original
            DaoConvidado = new DAOConvidado(FabricaConexao.Conexao);
            daoConvidados_do_evento = new DAOConvidados_do_evento(FabricaConexao.Conexao);
            dataGridAdicionaConvidado.ItemsSource = this.evento_agendado.ListConvidados;
        }

        private void BuscarConvidado()
        {
            dataGridBuscaConvidado.ItemsSource = DaoConvidado.BuscarRegistro("nome_convidado like '" + textBoxBuscar.Text.Trim() + "%' and status_convidado ='1'");// aqui a DaoConvidado foi colocada (=) no ItemSource pq itemSource é uma propriedade e não um método

        }

        private void AdicionarConvidadoEvento()
        {
            if (dataGridBuscaConvidado.SelectedItem != null && !this.evento_agendado.ListConvidados.Contains((Convidado)dataGridBuscaConvidado.SelectedItem) && !dataGridAdicionaConvidado.Items.Contains((Convidado)dataGridBuscaConvidado.SelectedItem))
            {

                if (this.evento_agendado.Id_evento_agendado == 0)
                {
                    this.evento_agendado.ListConvidados.Add((Convidado)dataGridBuscaConvidado.SelectedItem);

                }
                else
                {
                    try
                    {
                        daoConvidados_do_evento.Inserir(new Convidados_do_evento() { Id_do_convidado = ((Convidado)dataGridBuscaConvidado.SelectedItem).Id_convidado, Id_do_evento_agendado = this.evento_agendado.Id_evento_agendado });
                        this.evento_agendado.ListConvidados.Add((Convidado)dataGridBuscaConvidado.SelectedItem);
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.Message);

                    }

                }
                //dataGridAdicionaConvidado.Items.Add(dataGridBuscaConvidado.SelectedItem);//aqui o dataGridBuscaConvidado ficou como parametro do Add pq Add é um método
            }

        }
        private void imageAdicionarConvidado_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdicionarConvidadoEvento();
        }

        private void RemoverConvidadoEvento()
        {
            if (this.evento_agendado.Id_evento_agendado == 0)
            {
                this.evento_agendado.ListConvidados.Remove((Convidado)dataGridAdicionaConvidado.SelectedItem);

            }
            else
            {
                try
                {
                    daoConvidados_do_evento.Excluir("id_do_evento_agendado = '" + this.evento_agendado.Id_evento_agendado + "' AND id_do_convidado = '" + ((Convidado)dataGridAdicionaConvidado.SelectedItem).Id_convidado + "'");
                    this.evento_agendado.ListConvidados.Remove((Convidado)dataGridAdicionaConvidado.SelectedItem);

                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuscarConvidado();
            }
        }

        private void imageRemoverConvidado_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoverConvidadoEvento();
        }

        private void imageBotaoOK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AGENDAR_EVENTOS agendareventos = new AGENDAR_EVENTOS(this.evento_agendado);
            this.Close();
            agendareventos.ShowDialog();
        }

        private void dataGridBuscaConvidado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            AdicionarConvidadoEvento();

        }

        private void dataGridAdicionaConvidado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridAdicionaConvidado.SelectedItem != null)
            {
                RemoverConvidadoEvento();
            }
        }
    }
}
