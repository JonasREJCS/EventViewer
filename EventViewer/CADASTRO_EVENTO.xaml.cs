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
using System.IO;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for CADASTRO_EVENTO.xaml
    /// </summary>
    public partial class CADASTRO_EVENTO : Window
    {
        Evento evento;
        DAOEvento daoEvento;
        public CADASTRO_EVENTO()
        {
            InitializeComponent();
            this.evento = new Evento();
            daoEvento = new DAOEvento(FabricaConexao.Conexao);
        }
        public CADASTRO_EVENTO(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
            daoEvento = new DAOEvento(FabricaConexao.Conexao);
            PopularCampos();
        }

        private void imageSalvarEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

            GravarEvento();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }   
        }

        private bool ValidarDados()
        {
            if (String.IsNullOrEmpty(textBoxNomeEvento.Text))
            {
                textBoxNomeEvento.Focus();
                throw new Exception("Informe o nome do evento");
            }
            
                return true;
       
        }

        private void PopularCampos()
        {
            textBoxNomeEvento.Text = this.evento.Nome_evento;
            textBoxIdEvento.Text = this.evento.Id_evento.ToString();
            textBoxDescricaoEvento.Text = this.evento.Descricao_evento;
            if (this.evento.Logotipo_evento != null)
            {
                imageLogoEvento.Source = LoadImage(this.evento.Logotipo_evento);
            }
            else
            {
                imageLogoEvento.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/Mortal_Kombat_2011.jpg",UriKind.Relative));
            }
            comboBoxStatusEvento.SelectedIndex = Convert.ToInt32(this.evento.Status_evento);
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

        public void GravarEvento()
        {
            if (ValidarDados())
            {
                try
                {
                    if (this.evento == null)
                    {
                        this.evento = new Evento();

                    }

                    SalvarNaMemoria();

                    if (this.evento.Id_evento == 0)
                    {
                        daoEvento.Inserir(this.evento);
                    }
                    else
                    {
                        daoEvento.Atualiza(this.evento, "id_evento = '" + this.evento.Id_evento + "'");
                    }
                    Limpar();
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message);
                }
            }
        }
        private void SalvarNaMemoria()
        {
            this.evento.Nome_evento = textBoxNomeEvento.Text;
            this.evento.Descricao_evento = textBoxDescricaoEvento.Text;
            this.evento.Status_evento = comboBoxStatusEvento.SelectedIndex.ToString();

        }

        public void DeletarEvento()
        {
            if (this.evento.Id_evento != 0)
            {

                if (MessageBox.Show("Tem certeza que deseja excluir o evento '" + this.evento.Id_evento + " - " + this.evento.Nome_evento + "' do sistema?", "CONFIRMAR EXCLUSÃO", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        daoEvento.Excluir("id_evento = '" + this.evento.Id_evento + "'");
                        Limpar();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.Message);
                    }

                }
            }

        }
        private void Limpar()
        {
            this.evento = new Evento(); ;
            this.textBoxIdEvento.Text = "";
            this.textBoxNomeEvento.Text = "";
            this.textBoxDescricaoEvento.Text = "";
            this.comboBoxStatusEvento.SelectedIndex = 1;
            imageLogoEvento.Source = new BitmapImage(new Uri("/EventViewer;component/Imagens/Mortal_Kombat_2011.jpg", UriKind.Relative));
            this.textBoxNomeEvento.Focus();
        }

        private void imageExcluirEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeletarEvento();
        }

        private void imageNovoEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemNovoApertado();
            Limpar();
            textBoxNomeEvento.Focus();
        }
        private void TrocarImagemNovoApertado()
        {
            imageNovoEvento.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_vermelho_borda_aperte_novo.fw.png", UriKind.Relative));
        }
        private void TrocarImagemNovoNormal()
        {
            imageNovoEvento.Source = new BitmapImage(new Uri("/EventViewer;component/IMAGENS/botao_vermelho_borda_normal_novo.fw.png", UriKind.Relative));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            SalvarImagem((String[])e.Data.GetData(DataFormats.FileDrop));
        }
        private void SalvarImagem( String[] arquivo)
        {


            try
            {
                String caminho = arquivo[0];
                // cria uma imagem na memória a partir do caminho do arquivo arrastado
                //imagem = Image.FromFile(caminho);
                imageLogoEvento.Source = new BitmapImage(new Uri(caminho, UriKind.Absolute));

                string FileName = caminho;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                this.evento.Logotipo_evento = br.ReadBytes((int)fs.Length);
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
        private void buttonProcurarEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {

            BUSCAR_EVENTO b = new BUSCAR_EVENTO();
            this.Close();
            b.ShowDialog();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GravarEvento();
        }

        private void buttonProcurarEvento_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void textBoxIdEvento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    List<Evento> lista = daoEvento.BuscarRegistro("id_evento ='" + textBoxIdEvento.Text.Trim() + "'");
                    if (lista.Count > 0)
                    {
                        this.evento = lista[0];
                        PopularCampos();
                    }

                }
                catch (Exception erro)
                {

                    MessageBox.Show(erro.Message);
                }
            }
        }

        private void imageNovoEvento_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TrocarImagemNovoNormal();
        }

        private void imageNovoEvento_KeyDown(object sender, KeyEventArgs e)
        {
            TrocarImagemNovoApertado();
        }

        private void imageNovoEvento_KeyUp(object sender, KeyEventArgs e)
        {
            TrocarImagemNovoNormal();
        }

        private void imageLogoEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AbrirPesquisaImagem();
        }
        private void AbrirPesquisaImagem()
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
                SalvarImagem(new String[]{dlg.FileName});
            }

        }

        private void textBoxNomeEvento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(textBoxNomeEvento.Text.Trim()))
            {
                textBoxDescricaoEvento.Focus();
            }
        }
    }
}
