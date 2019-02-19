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
    /// Interaction logic for CONSULTAR_GRUPO_PARTICIPANTE.xaml
    /// </summary>
    public partial class CONSULTAR_GRUPO_PARTICIPANTE : Window
    {
        DAOGrupo_de_participante daoGrupo_de_participante;
        Evento_agendado evento_agendado;
        DAOGrupos_do_evento_agendado daoGruposDoEvento;
        public CONSULTAR_GRUPO_PARTICIPANTE(Evento_agendado evento_agendado)
        {
            InitializeComponent();
            textBoxBuscar.Focus();
            daoGrupo_de_participante = new DAOGrupo_de_participante(FabricaConexao.Conexao);
            daoGruposDoEvento = new DAOGrupos_do_evento_agendado(FabricaConexao.Conexao);
            this.evento_agendado = evento_agendado;
            dataGridGruposDoEvento.ItemsSource = null;
            dataGridGruposDoEvento.ItemsSource = this.evento_agendado.ListaGruposDoEventoAgendado;
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    dataGridGrupos.ItemsSource = daoGrupo_de_participante.BuscarRegistro("nome_grupo_de_participante like '" + textBoxBuscar.Text.Trim() + "%'");
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridGrupos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AdicionarGrupoAOEvento();
        }
        private void AdicionarGrupoAOEvento()
        {
            if (dataGridGrupos.SelectedItem != null && !this.evento_agendado.ListaGruposDoEventoAgendado.Contains((Grupo_de_participante)dataGridGrupos.SelectedItem) && !dataGridGruposDoEvento.Items.Contains((Grupo_de_participante)dataGridGrupos.SelectedItem))
            {
                if (this.evento_agendado.Id_evento_agendado != 0)
                {
                    try
                    {
                        if (daoGruposDoEvento == null)
                        {
                            daoGruposDoEvento = new DAOGrupos_do_evento_agendado(FabricaConexao.Conexao);
                        }

                        Grupos_do_evento_agendado grupos_do_evento = new Grupos_do_evento_agendado()
                        {
                            Id_do_evento_agendado = this.evento_agendado.Id_evento_agendado,
                            Id_do_grupo = ((Grupo_de_participante)dataGridGrupos.SelectedItem).Id_grupo_de_participante
                        };

                        daoGruposDoEvento.Inserir(grupos_do_evento);

                        this.evento_agendado.ListaGruposDoEventoAgendado.Add((Grupo_de_participante)dataGridGrupos.SelectedItem);
                    }
                    catch (Exception erro)
                    {

                        MessageBox.Show(erro.Message);
                    }
                }
                else
                {

                    this.evento_agendado.ListaGruposDoEventoAgendado.Add((Grupo_de_participante)dataGridGrupos.SelectedItem);
                }
            }
           


        }

        private void RemoverGrupoDoEvento()
        {
            if (dataGridGruposDoEvento.SelectedItem != null)
            {

                if (this.evento_agendado.Id_evento_agendado == 0)
                {
                    this.evento_agendado.ListaGruposDoEventoAgendado.Remove((Grupo_de_participante)dataGridGruposDoEvento.SelectedItem);

                }
                else
                {
                    try
                    {
                        if (dataGridGruposDoEvento.Items.Count == 1)
                        {
                            throw new Exception("O evento deve ter pelo menos um grupo");
                        }
                        daoGruposDoEvento.Excluir("id_do_evento_agendado = '" + this.evento_agendado.Id_evento_agendado + "' AND Id_do_grupo = '" + ((Grupo_de_participante)dataGridGruposDoEvento.SelectedItem).Id_grupo_de_participante + "'");
                        this.evento_agendado.ListaGruposDoEventoAgendado.Remove((Grupo_de_participante)dataGridGruposDoEvento.SelectedItem);
                    }

                    catch (Exception erro)
                    {

                        MessageBox.Show(erro.Message);
                    }
                }
            }
        }

        private void imageAdicionar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdicionarGrupoAOEvento();
        }

        private void imageOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelecionarGrupos();
        }
        private void SelecionarGrupos()
        {

            AGENDAR_EVENTOS a = new AGENDAR_EVENTOS(this.evento_agendado);

            this.Close();
            a.ShowDialog();

        }

        private void imageRemover_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoverGrupoDoEvento();
        }

        private void dataGridGruposDoEvento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RemoverGrupoDoEvento();
        }

    }
}
