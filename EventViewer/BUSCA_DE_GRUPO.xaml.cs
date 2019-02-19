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
    /// Interaction logic for BUSCA_DE_GRUPO.xaml
    /// </summary>
    public partial class BUSCA_DE_GRUPO : Window
    {
        DAOGrupo_de_participante daoGrupo_de_participante;
        DAOPessoas_do_grupo daoPessoas_do_grupo;
        public BUSCA_DE_GRUPO()
        {
            daoGrupo_de_participante = new DAOGrupo_de_participante(FabricaConexao.Conexao);
            daoPessoas_do_grupo = new DAOPessoas_do_grupo(FabricaConexao.Conexao);
            InitializeComponent();
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
                        condicao = "nome_grupo_de_participante like '%" + textBoxBuscar.Text.Trim() + "%'";

                    }
                    else
                    {
                        condicao = "nome_grupo_de_participante like'" + textBoxBuscar.Text.Trim() + "%'";

                    }

                    if (radioButtonCadastroAtivo.IsChecked == true)
                    {
                        condicao += " AND status_grupo_de_participante = '1'";
                    }
                    else if (radioButtonCadastroInativo.IsChecked == true)
                    {
                        condicao += " AND status_grupo_de_participante = '0'";
                    }
                    dataGridGrupos.ItemsSource = daoGrupo_de_participante.BuscarRegistro(condicao);
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridGrupos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelecionarGrupo();
        }

        private void SelecionarGrupo()
        {
            if (dataGridGrupos.SelectedItem != null)
            {
                try
                {
                    Grupo_de_participante grupo = new Grupo_de_participante();

                    grupo = (Grupo_de_participante)dataGridGrupos.SelectedItem;

                    grupo.colecao_Participantes_Do_Grupo = daoPessoas_do_grupo.BuscarRegistro("id_do_grupo_de_participante = '" + grupo.Id_grupo_de_participante + "'");

                    GRUPO_DE_PARTICIPANTES grupo_de_participantes = new GRUPO_DE_PARTICIPANTES(grupo);
                    grupo_de_participantes.Show();
                    this.Close();
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }

            }
        }
        private void imageOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelecionarGrupo();
        }
    }
}
