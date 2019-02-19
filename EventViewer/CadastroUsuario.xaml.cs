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
    /// Interaction logic for CadastroUsuario.xaml
    /// </summary>
    public partial class CadastroUsuario : Window
    {
        Usuario usuario;
        DAOUsuario daoUsuario;
        ObservableCollection<Usuario> listaDeUsuario;
        int idUsuarioLogado;
        public CadastroUsuario(int idUsuarioLogado)
        {
            InitializeComponent();
            textBoxNome.Focus();
            CarregarObjetosPadrao();
            this.idUsuarioLogado = idUsuarioLogado;
        }

        private void CarregarObjetosPadrao()
        {
            this.usuario = new Usuario();
            daoUsuario = new DAOUsuario(FabricaConexao.Conexao);
            listaDeUsuario = new ObservableCollection<Usuario>();
            dataGridUsuario.ItemsSource = listaDeUsuario;
        }

        private void Limpar()
        {
            this.usuario = new Usuario();
            textBoxIdUsuario.Clear();
            textBoxNome.Clear();
            textBoxUsuario.Clear();
            passwordBoxSenha.Clear();
            passwordBoxRepetirSenha.Clear();
            comboBoxTiposUsuario.SelectedIndex = 0;
            comboBoxStatusUsuario.SelectedIndex = 1;
            textBoxNome.Focus();
        }

        private void ImageBotaoSalvar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoSalvarApertado();
            Gravar();
        }

        private bool ValidarCampos()
        {

            if (String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                textBoxNome.Focus();
                throw new Exception("Informe o nome do usuário.");

            }
            else if (String.IsNullOrEmpty(textBoxUsuario.Text.Trim()))
            {
                textBoxUsuario.Focus();
                throw new Exception("Informe o Login do usuário.");
            }
            else if (this.usuario.Senha == null)
            {

                if (String.IsNullOrEmpty(passwordBoxSenha.Password.Trim()))
                {
                    passwordBoxSenha.Focus();
                    throw new Exception("Informe a senha.");
                }
                else if (!passwordBoxSenha.Password.Trim().Equals(passwordBoxRepetirSenha.Password.Trim()))
                {
                    passwordBoxRepetirSenha.Focus();
                    throw new Exception("Senha não confere.");
                }

            }
            return true;

        }

        private void SalvarNaMemoria()
        {
            this.usuario.Nome_usuario = textBoxNome.Text.Trim();
            this.usuario.Usuario_login = textBoxUsuario.Text.Trim();
            this.usuario.Senha = passwordBoxSenha.Password.Trim();
            this.usuario.Tipo_usuario = comboBoxTiposUsuario.SelectedIndex.ToString();
            this.usuario.Status_usuario = comboBoxStatusUsuario.SelectedIndex.ToString();
        }

        private void Gravar()
        {
            try
            {
                if (ValidarCampos())
                {
                    SalvarNaMemoria();
                    if (this.usuario.Id_usuario == 0)
                    {
                        long id_usuario = daoUsuario.Inserir(this.usuario);
                        this.usuario.Id_usuario = (int)id_usuario;
                        listaDeUsuario.Add(this.usuario);
                    }
                    else
                    {
                        daoUsuario.Atualiza(this.usuario, "id_usuario = '" + this.usuario.Id_usuario + "'");
                        listaDeUsuario.Remove(this.usuario);
                        listaDeUsuario.Add(this.usuario);
                    }
                    Limpar();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void PopularCampos()
        {
            textBoxIdUsuario.Text = this.usuario.Id_usuario.ToString();
            textBoxNome.Text = this.usuario.Nome_usuario;
            textBoxUsuario.Text = this.usuario.Usuario_login;
            comboBoxTiposUsuario.SelectedIndex = Convert.ToInt32(this.usuario.Tipo_usuario);
            comboBoxStatusUsuario.SelectedIndex = Convert.ToInt32(this.usuario.Status_usuario);
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string condicao = "nome_usuario like '";
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
                        condicao += " AND status_usuario = '1'";
                    }
                    else if (RadioButtonInativos.IsChecked == true)
                    {
                        condicao += " AND status_usuario = '0'";
                    }
                    condicao += " AND not usuario_login = 'ADMIN' AND NOT id_usuario = '" + this.idUsuarioLogado + "'";

                    listaDeUsuario = daoUsuario.BuscarRegistro(condicao);
                    dataGridUsuario.ItemsSource = listaDeUsuario;
                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void dataGridUsuario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridUsuario.SelectedIndex < dataGridUsuario.Items.Count && dataGridUsuario.SelectedIndex > -1)
            {
                this.usuario = (Usuario)dataGridUsuario.SelectedItem;
                PopularCampos();
            }
        }

        private void ImageBotaoNovo_KeyDown(object sender, KeyEventArgs e)
        {
            Limpar();
            textBoxNome.Focus();
        }

        private void Excluir()
        {
            try
            {
                if (this.usuario.Id_usuario != 0)
                {
                    if ((MessageBox.Show("Deseja excluir o usuário '" + this.usuario.Nome_usuario + "'?", "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                    {

                        daoUsuario.Excluir("id_usuario = '" + this.usuario.Id_usuario + "'");
                        listaDeUsuario.Remove(this.usuario);
                        Limpar();
                    }
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void ImageBotaoExcluir_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoExcluirApertado();
            

                Excluir();

            
        }

        private void ImageBotaoNovo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoNovoApertado();
            Limpar();
        }
        private void TrocarImagemBotaoNovoApertado()
        {
            ImageBotaoNovo.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_aperte_novo.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoNovoSolto()
        {
            ImageBotaoNovo.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_normal_novo.fw.png", UriKind.Relative));
        }

        private void ImageBotaoNovo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoNovoSolto();
        }

        private void ImageBotaoSalvar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoSalvarSolto();
        }

        private void TrocarImagemBotaoSalvarApertado()
        {
            ImageBotaoSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_aperte_salvar.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoSalvarSolto()
        {
            ImageBotaoSalvar.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_normal_salvar.fw.png", UriKind.Relative));
        }
        private void TrocarImagemBotaoExcluirApertado()
        {
            ImageBotaoExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_aperte_excluir.fw.png"
         , UriKind.Relative));
        }
        private void TrocarImagemBotaoExcluirSolto()
        {
            ImageBotaoExcluir.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_amarelo_borda_normal_excluir.fw.png"
         , UriKind.Relative));
        }

        private void ImageBotaoExcluir_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemBotaoExcluirSolto();
        }

        private void textBoxIdUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxNome.Text.Trim()))
            {
                comboBoxTiposUsuario.Focus();
            }
        }

        private void comboBoxTiposUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxUsuario.Focus();
            }
        }

        private void textBoxUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxUsuario.Text.Trim()))
            {
                passwordBoxSenha.Focus();
            }
        }

        private void passwordBoxSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(passwordBoxSenha.Password.Trim()))
            {
                passwordBoxRepetirSenha.Focus();
            }
        }

        private void passwordBoxRepetirSenha_KeyDown(object sender, KeyEventArgs e)
        {

        }

        
    }

}
