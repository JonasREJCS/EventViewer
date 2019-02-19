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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventViewer.Modelo;

namespace EventViewer
{
    /// <summary>
    /// Interaction logic for UserControlPainelEventosAgendados.xaml
    /// </summary>
    public partial class UserControlPainelEventosAgendados : UserControl
    {
        public Evento_agendado evento_agendado { get; set; }
        public UserControlPainelEventosAgendados()
        {
            InitializeComponent();
        }

        private void imageLogotipoEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_EVENTO cadastro_evento = new CADASTRO_EVENTO(evento_agendado.Evento);
            cadastro_evento.ShowDialog();
        }

        private void labelLocalEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_DE_LOCAL_DE_EVENTO cadastro_local_de_evento = new CADASTRO_DE_LOCAL_DE_EVENTO(evento_agendado.Local_de_evento);
            cadastro_local_de_evento.ShowDialog();
        }

        private void labelData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AGENDAR_EVENTOS agendar_eventos = new AGENDAR_EVENTOS(evento_agendado);
            agendar_eventos.ShowDialog();
        }

        private void labelHorario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AGENDAR_EVENTOS agendar_eventos = new AGENDAR_EVENTOS(evento_agendado);
            agendar_eventos.ShowDialog();
        }

        private void labelCapacidade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AGENDAR_EVENTOS agendar_eventos = new AGENDAR_EVENTOS(evento_agendado);
            agendar_eventos.ShowDialog();
        }

        private void imageCantoLogoEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_EVENTO cadastro_evento = new CADASTRO_EVENTO(evento_agendado.Evento);
            cadastro_evento.ShowDialog();
        }

        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CADASTRO_EVENTO cadastro_evento = new CADASTRO_EVENTO(evento_agendado.Evento);
            cadastro_evento.ShowDialog();
        }

    }
}
