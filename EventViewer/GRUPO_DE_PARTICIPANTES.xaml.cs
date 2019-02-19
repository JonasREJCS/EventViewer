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
    /// Interaction logic for GRUPO_DE_PARTICIPANTES.xaml
    /// </summary>
    public partial class GRUPO_DE_PARTICIPANTES : Window
    {
        DAOGrupo_de_participante daoGrupo_de_participante;
        Grupo_de_participante grupo_de_participante;
        DAOPessoas_do_grupo daoPessoas_do_grupo;
        DAOTipo_de_grupo daoTipo_de_grupo;
        DAOPessoa daoPessoa;


        public GRUPO_DE_PARTICIPANTES()
        {
            CarregarObjetosPadrao();
        }
        public GRUPO_DE_PARTICIPANTES(Grupo_de_participante grupo_de_participante)
        {
            CarregarObjetosPadrao();
            this.grupo_de_participante = grupo_de_participante;
            PopularCampos();
        }

        private void CarregarObjetosPadrao()
        {
            InitializeComponent();
            textBoxNome.Focus();
            daoGrupo_de_participante = new DAOGrupo_de_participante(FabricaConexao.Conexao);
            daoPessoas_do_grupo = new DAOPessoas_do_grupo(FabricaConexao.Conexao);
            daoPessoa = new DAOPessoa(FabricaConexao.Conexao);
            daoTipo_de_grupo = new DAOTipo_de_grupo(FabricaConexao.Conexao);
            if (this.grupo_de_participante == null)
            {
                this.grupo_de_participante = new Grupo_de_participante();
            }
            dataGridParticipantesGrupo.ItemsSource = this.grupo_de_participante.colecao_Participantes_Do_Grupo;
        }

        private void PopularCampos()
        {
            textBoxId.Text = this.grupo_de_participante.Id_grupo_de_participante.ToString();
            textBoxNome.Text = this.grupo_de_participante.Nome_grupo_de_participante;
            textBoxDescricao.Text = this.grupo_de_participante.Descricao_grupo_de_participante;
            dataGridParticipantesGrupo.ItemsSource = this.grupo_de_participante.colecao_Participantes_Do_Grupo;
            comboBoxStatus.SelectedIndex = Convert.ToInt32(this.grupo_de_participante.Status_grupo_de_participante);
        }

        private void imageCriarGrupo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Gravar();
        }

        private void Gravar()
        {
            if (ValidarCampos())
            {
                try
                {

                    this.grupo_de_participante.Nome_grupo_de_participante = textBoxNome.Text.Trim();
                    this.grupo_de_participante.Status_grupo_de_participante = comboBoxStatus.SelectedIndex.ToString();
                    this.grupo_de_participante.Descricao_grupo_de_participante = textBoxDescricao.Text;
                    if (this.grupo_de_participante.Id_grupo_de_participante == 0)
                    {
                        long idGrupo = daoGrupo_de_participante.Inserir(this.grupo_de_participante);

                        foreach (Pessoa pessoa in this.grupo_de_participante.colecao_Participantes_Do_Grupo)
                        {
                            Pessoas_do_grupo pessoa_do_grupo = new Pessoas_do_grupo()
                            {
                                Id_da_pessoa = pessoa.Id_pessoa,
                                Id_do_grupo_de_participante = (int)idGrupo
                            };
                            daoPessoas_do_grupo.Inserir(pessoa_do_grupo);

                        }
                    }
                    else
                    {
                        daoGrupo_de_participante.Atualiza(this.grupo_de_participante, "Id_grupo_de_participante = '" + this.grupo_de_participante.Id_grupo_de_participante + "'");
                    }
                    Limpar();
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }
        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(textBoxNome.Text))
            {
                MessageBox.Show("Informe o nome do grupo");
                textBoxNome.Focus();
                return false;
            }
            else if (this.grupo_de_participante.colecao_Participantes_Do_Grupo.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um participante ao grupo.");
                textBoxBuscar.Focus();
                return false;

            }
            return true;
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                try
                {

                    dataGridParticipantes.ItemsSource = daoPessoa.BuscarRegistro("nome_pessoa like '" + textBoxBuscar.Text.Trim() + "%' and status_pessoa = '1'");

                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void imageAdicionarParticipante_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageAdicionarParticipante.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/DireitaAperte.fw.png", UriKind.Relative));
            adicionarParticipanteAoGrupo();
        }

        private void dataGridParticipantes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            adicionarParticipanteAoGrupo();
        }
        private void adicionarParticipanteAoGrupo()
        {


            if (dataGridParticipantes.SelectedItem != null && !this.grupo_de_participante.colecao_Participantes_Do_Grupo.Contains((Pessoa)dataGridParticipantes.SelectedItem))
            {
                if (this.grupo_de_participante.Id_grupo_de_participante != 0)
                {
                    try
                    {
                        Pessoas_do_grupo pessoa_do_grupo = new Pessoas_do_grupo()
                        {

                            Id_da_pessoa = ((Pessoa)dataGridParticipantes.SelectedItem).Id_pessoa,
                            Id_do_grupo_de_participante = this.grupo_de_participante.Id_grupo_de_participante
                        };
                        daoPessoas_do_grupo.Inserir(pessoa_do_grupo);
                        this.grupo_de_participante.colecao_Participantes_Do_Grupo.Add((Pessoa)dataGridParticipantes.SelectedItem);
                    }
                    catch (Exception erro)
                    {

                        MessageBox.Show(erro.Message);
                    }
                }
                else
                {
                    this.grupo_de_participante.colecao_Participantes_Do_Grupo.Add((Pessoa)dataGridParticipantes.SelectedItem);
                }
                
            }
        }

        private void imageNovo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/BotãoAzulNormalNovo.fw.png", UriKind.Relative));
            Limpar();
        }
        private void Limpar()
        {
            textBoxId.Clear();
            textBoxNome.Clear();
            textBoxDescricao.Clear();
            comboBoxStatus.SelectedIndex = 1;
            this.grupo_de_participante = new Grupo_de_participante();
            textBoxNome.Focus();
            dataGridParticipantesGrupo.ItemsSource = null;
            dataGridParticipantesGrupo.ItemsSource = this.grupo_de_participante.colecao_Participantes_Do_Grupo;
        }

        private void imageNovo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageNovo.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/BotãoAzulAperteNovo.fw.png", UriKind.Relative));

        }

        private void imageRemoverParticipante_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageRemoverParticipante.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/EsquerdaAperte.fw.png", UriKind.Relative));
            if (dataGridParticipantesGrupo.SelectedIndex >= 0 && dataGridParticipantesGrupo.SelectedIndex < dataGridParticipantesGrupo.Items.Count)
            {
                RemoverParticipanteDoGrupo();
            }
        }
        private void RemoverParticipanteDoGrupo()
        {



            if (this.grupo_de_participante.Id_grupo_de_participante != 0 && this.grupo_de_participante.colecao_Participantes_Do_Grupo.Count > 1)
            {
                try
                {
                    daoPessoas_do_grupo.Excluir("id_do_grupo_de_participante = '" + this.grupo_de_participante.Id_grupo_de_participante + "' AND id_da_pessoa = '" + ((Pessoa)dataGridParticipantesGrupo.SelectedItem).Id_pessoa + "'");

                    this.grupo_de_participante.colecao_Participantes_Do_Grupo.Remove((Pessoa)dataGridParticipantesGrupo.SelectedItem);

                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
            else if (this.grupo_de_participante.Id_grupo_de_participante == 0)
            {
                
            this.grupo_de_participante.colecao_Participantes_Do_Grupo.Remove((Pessoa)dataGridParticipantesGrupo.SelectedItem);
            }
        }

        //private void Excluir()
        //{
        //    if (this.grupo_de_participante.Id_grupo_de_participante != 0)
        //    {
        //        try
        //        {
        //            Limpar();
        //        }
        //        catch (Exception erro)
        //        {
                    
        //            MessageBox.Show(erro.Message);
        //        }
        //    }
        //}
        private void imageRemoverParticipante_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageRemoverParticipante.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/EsquerdaNormal.fw.png", UriKind.Relative));
        }

        private void imageAdicionarParticipante_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imageAdicionarParticipante.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/DireitaNormal.fw.png", UriKind.Relative));
        }

        private void dataGridParticipantesGrupo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RemoverParticipanteDoGrupo();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AbrirBuscarGrupo();
        }
        private void AbrirBuscarGrupo()
        {
            BUSCA_DE_GRUPO busca_de_grupo = new BUSCA_DE_GRUPO();
            this.Close();
            busca_de_grupo.Show();
        }


        private void imageBuscar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AbrirBuscarGrupo();
        }

        private void textBoxNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxDescricao.Focus();
            }
        }
    }
}
