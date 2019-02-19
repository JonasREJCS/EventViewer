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
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for PRINCIPAL.xaml
    /// </summary>
    public partial class PRINCIPAL : Window
    {
        DAOUsuario daoUsuario;
        Usuario usuarioLogado;
        DAOEvento_agendado daoEvento_agendado;
        List<Evento_agendado> listaEventos_agendados;
        int i;

        public PRINCIPAL()
        {
            InitializeComponent();
            CarregarobjetosPadrao();
            dateTimePickerPeriodoInicial.Value = DateTime.Today;
            dateTimePickerPeriodoFinal.Value = DateTime.Today.AddDays(7);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 8, 0);
            dispatcherTimer.Start();
           
            
        }
        void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (i == 0)
            {
                ChangeSource2(this.image1, new BitmapImage(new Uri("/EventViewer;component/Imagens/Apresentacao1TelaPrincipal.fw.png", UriKind.Relative)), new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 5));
                i++;
            }
            else if (i == 1)
            {
                ChangeSource2(this.image1, new BitmapImage(new Uri("/EventViewer;component/Imagens/Apresentacao1TelaPrincipal.fw.png", UriKind.Relative)), new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 5));
                i--;
            }


        }
        private void CarregarobjetosPadrao()
        {
            listaEventos_agendados = new List<Evento_agendado>();
            usuarioLogado = new Usuario();
        }
        private void labelEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_EVENTO formevento = new CADASTRO_EVENTO();
            formevento.ShowDialog();
        }

        private void labelParticipantes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_DE_PARTICIPANTES cadastrodeparticipantes = new CADASTRO_DE_PARTICIPANTES();
            cadastrodeparticipantes.ShowDialog();
        }

        private void labelLocaisDeEventos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_DE_LOCAL_DE_EVENTO cadastrodelocaldeevento = new CADASTRO_DE_LOCAL_DE_EVENTO();
            cadastrodelocaldeevento.ShowDialog();
        }

        private void labelGrupo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GRUPO_DE_PARTICIPANTES grupodeparticipantes = new GRUPO_DE_PARTICIPANTES();
            grupodeparticipantes.ShowDialog();
        }

        private bool Validar()
        {

            if (String.IsNullOrEmpty(textBoxUsuario.Text.Trim()))
            {
                textBoxUsuario.Focus();
                throw new Exception("Informe o usuário");
            }
            else if (String.IsNullOrEmpty(passwordBoxSenha.Password.Trim()))
            {
                passwordBoxSenha.Focus();
                throw new Exception("Informe a senha");
            }
            return true;
        }

        private void imageAutenticar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Autenticar();

        }
        private void exibeMenu()
        {
            gridUsuarioSenha.Visibility = System.Windows.Visibility.Hidden;
            imageAutenticar.Visibility = System.Windows.Visibility.Hidden;
            groupBoxConsultarEvento.Visibility = System.Windows.Visibility.Visible;
            gridMenu.Visibility = System.Windows.Visibility.Visible;
            this.image1.Visibility = System.Windows.Visibility.Hidden;
            if (this.usuarioLogado.Tipo_usuari == Tipo.Comum)
            {
                labelEvento.Visibility = System.Windows.Visibility.Hidden;
                labelConvidados.Visibility = System.Windows.Visibility.Hidden;
                labelLocaisDeEventos.Visibility = System.Windows.Visibility.Hidden;
                labelUsuarios.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                labelEvento.Visibility = System.Windows.Visibility.Visible;
                labelConvidados.Visibility = System.Windows.Visibility.Visible;
                labelLocaisDeEventos.Visibility = System.Windows.Visibility.Visible;
                labelUsuarios.Visibility = System.Windows.Visibility.Visible;
            
            }
        }

        private void LogOff()
        {
            this.Title = "Tela principal - EventViewer - Sistema de gerenciamento de participantes de evento";
                    
            gridUsuarioSenha.Visibility = System.Windows.Visibility.Visible;
            imageAutenticar.Visibility = System.Windows.Visibility.Visible;
            gridMenu.Visibility = System.Windows.Visibility.Hidden;
            groupBoxConsultarEvento.Visibility = System.Windows.Visibility.Hidden;
            this.usuarioLogado = null;
            image1.Visibility = System.Windows.Visibility.Visible;
            ChangeSource2(this.image1, new BitmapImage(new Uri("/EventViewer;component/Images/tulip.jpg", UriKind.Relative)), new TimeSpan(0, 0, 6), new TimeSpan(0, 0, 10));

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FabricaConexao.AbrirConexao();
        }

        private void labelUsuarios_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CadastroUsuario c = new CadastroUsuario(this.usuarioLogado.Id_usuario);
            if (this.usuarioLogado.Tipo_usuari == Tipo.Comum)
            {
                c.comboBoxTiposUsuario.SelectedIndex = 0;
                c.comboBoxTiposUsuario.IsEditable = false;
            }
            c.ShowDialog();
        }

        private void textBoxSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Autenticar();
            }
        }
        private void Autenticar()
        {
            try
            {
                if (Validar())
                {

                    if (daoUsuario == null)
                    {
                        daoUsuario = new DAOUsuario(FabricaConexao.Conexao);
                    }

                    ObservableCollection<Usuario> lista = daoUsuario.BuscarRegistro("usuario_login = '" + textBoxUsuario.Text.Trim() + "' AND senha = '" + passwordBoxSenha.Password.Trim() + "' AND status_usuario = '1'");
                    if (lista.Count > 0)
                    {
                        this.usuarioLogado = lista[0];
                        textBoxUsuario.Clear();
                        passwordBoxSenha.Clear();
                        exibeMenu();
                        this.Title = "Tela principal - EventViewer - Sistema de gerenciamento de participantes de evento - Usuário autenticado: " + this.usuarioLogado.Usuario_login;
                    
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha inválida.");
                    }
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void labelAgendarEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AGENDAR_EVENTOS a = new AGENDAR_EVENTOS(this.usuarioLogado);
            a.ShowDialog();
        }

        /*private void button1_Click(object sender, RoutedEventArgs e)
        {
            CarregarEventosAgendados();
        }*/

        private bool ValidarPesquisaEventos()
        {
            if (dateTimePickerPeriodoFinal.Value < dateTimePickerPeriodoInicial.Value)
            {
                dateTimePickerPeriodoInicial.Focus();
                throw new Exception("A data final não pode ser maior que a inicial.");
            }
            else
            {
                return true;
            }
        }
        private void CarregarEventosAgendados()
        {
            try
            {
                if (ValidarPesquisaEventos())
                {
                    stackPanel1.Children.Clear();
                    string condicao = "data > '" + Convert.ToDateTime(dateTimePickerPeriodoInicial.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' AND data < '" + Convert.ToDateTime(dateTimePickerPeriodoFinal.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    if (checkBoxEventosOrganizadosPeloUsuario.IsChecked == true)
                    {
                        condicao += " AND id_do_usuario_organizador = '" + this.usuarioLogado.Id_usuario + "'";
                    }
                    if (checkBoxListarEventosEncerrados.IsChecked == false)
                    {
                        condicao += " AND status_evento_agendado = '1'";
                    }
                    if (this.daoEvento_agendado == null)
                    {

                        daoEvento_agendado = new DAOEvento_agendado(FabricaConexao.Conexao);
                    }
                    condicao += " ORDER BY data";
                    listaEventos_agendados = daoEvento_agendado.BuscarRegistro(condicao);

                    foreach (Evento_agendado evento_agendado in listaEventos_agendados)
                    {
                        UserControlPainelEventosAgendados userControlPainelEventoAgendado = new UserControlPainelEventosAgendados();
                        userControlPainelEventoAgendado.evento_agendado = evento_agendado;
                        if (evento_agendado.Evento.Logotipo_evento != null)
                        {
                            userControlPainelEventoAgendado.imageLogotipoEvento.Source = LoadImage(evento_agendado.Evento.Logotipo_evento);
                        }
                        userControlPainelEventoAgendado.textBoxNomeEvento.Text = evento_agendado.Evento.Nome_evento;
                        userControlPainelEventoAgendado.textBoxData.Text = Convert.ToDateTime(evento_agendado.DataHora.ToString()).ToString("dd-MM-yyyy");
                        userControlPainelEventoAgendado.textBoxHorarioInicio.Text = Convert.ToDateTime(evento_agendado.DataHora.ToString()).ToString("HH:mm:ss"); 
                        userControlPainelEventoAgendado.textBoxLocal.Text = evento_agendado.Local_de_evento.Nome_do_local;

                        if (evento_agendado.Capacidade_participante <= 0 || evento_agendado.Capacidade_participante == null)
                        {
                            evento_agendado.Capacidade_participante = evento_agendado.QuantidadeParticipante;
                        }
                        int porcentagemCapacidade = 0;
                        if (evento_agendado.Capacidade_participante != 0)
                        {
                            porcentagemCapacidade = (evento_agendado.QuantidadeParticipante * 100) / (int)evento_agendado.Capacidade_participante;
                        }
                        else
                        {
                            porcentagemCapacidade = 0;
                        }
                       

                        userControlPainelEventoAgendado.progressBarCapacidade.Value = porcentagemCapacidade;
                        userControlPainelEventoAgendado.textBoxQuantidadePessoas.Text = evento_agendado.QuantidadeParticipante.ToString() + " de " + evento_agendado.Capacidade_participante.ToString();
                        if (evento_agendado.Status_evento_agendado == "0")
                        {
                            userControlPainelEventoAgendado.textBoxStatus.Text = "ENCERRADO";
                        }
                        else
                        {
                            userControlPainelEventoAgendado.textBoxStatus.Text = "EM ABERTO";
                        }
                        stackPanel1.Children.Add(userControlPainelEventoAgendado);
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new System.IO.MemoryStream(imageData))
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

        private void labelConvidados_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_DE_CONVIDADO cadastro_convidado = new CADASTRO_DE_CONVIDADO();
            cadastro_convidado.ShowDialog();
        }

        private void imageLogoff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogOff();
        }
        private void Limpar()
        {
            this.stackPanel1.Children.Clear();

            dateTimePickerPeriodoInicial.Value = DateTime.Today;
            dateTimePickerPeriodoFinal.Value = DateTime.Today.AddDays(7);
        }

        private void passwordBoxSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Autenticar();
            }
        }

        public void ChangeSource2(
    Image image, ImageSource source, TimeSpan fadeOutTime, TimeSpan fadeInTime)
        {
            var fadeInAnimation = new DoubleAnimation(1d, fadeInTime);

            if (image.Source != null)
            {
                var fadeOutAnimation = new DoubleAnimation(0d, fadeOutTime);

                fadeOutAnimation.Completed += (o, e) =>
                {
                    image.Source = source;
                    image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
                };

                image.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
            }
            else
            {
                image.Opacity = 0d;
                image.Source = source;
                image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
            }
        }

        /*private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            CarregarEventosAgendados();
        }*/

        private void imageLogoff_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            LogOff();
            Limpar();
        }

        private void labelSair_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogOff();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FabricaConexao.FecharConexao();
        }

        private void labelBuscarEventosAgendados_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CarregarEventosAgendados();
        }

         /*private void imageBuscarEventoAgendado_MouseDown(object sender, MouseButtonEventArgs e)
         {
             CarregarEventosAgendados();
         }*/

    }
}
