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
    /// Interaction logic for AGENDAR_EVENTOS.xaml
    /// </summary>
    public partial class AGENDAR_EVENTOS : Window
    {
        DAOEvento_agendado daoEvento_agendado;
        DAOGrupos_do_evento_agendado daoGrupos_do_evento_agendado;
        DAOConvidados_do_evento daoConvidados_do_evento;
        Evento_agendado evento_agendado;

        public AGENDAR_EVENTOS(Usuario usuario)
        {
            InitializeComponent();
            CarregarObjetosPadrao();
            this.evento_agendado = new Evento_agendado();
            this.evento_agendado.Usuario = usuario;
            this.evento_agendado.Id_do_usuario_organizador = this.evento_agendado.Usuario.Id_usuario;
            if (this.evento_agendado.Status_evento_agendado == null)
            {
                this.evento_agendado.Status_evento_agendado = "1";
            }
            textBoxOrganizador.Text = this.evento_agendado.Usuario.Nome_usuario;
        }
        public AGENDAR_EVENTOS(Evento_agendado evento_agendado)
        {
            InitializeComponent();
            CarregarObjetosPadrao();
            this.evento_agendado = evento_agendado;
            PopularCampos();
        }
        // método para carregar os dados da DAO nos campos do formulário
        private void PopularCampos()
        {
            if (this.evento_agendado.Id_evento_agendado != 0)
            {

                textBoxId.Text = this.evento_agendado.Id_evento_agendado.ToString();
            }
            TextBoxDescricao.Text = this.evento_agendado.Descricao_evento_agendado;
            datePickerDataHorarioAgendamento.Value = this.evento_agendado.DataHora;
            timePickerHorarioTermino.Value = this.evento_agendado.Horario_termino;
            timePickerHorarioEncontro.Value = this.evento_agendado.Horario_encontro;
            integerUpDownCapacidade.Value = this.evento_agendado.Capacidade_participante;
            TextBoxConsultaLocal.Text = this.evento_agendado.Local_de_evento.Nome_do_local;
            TextBoxConsultaEvento.Text = this.evento_agendado.Evento.Nome_evento;
            comboBoxStatusEvento.SelectedIndex = Convert.ToInt32(this.evento_agendado.Status_evento_agendado);
            dataGridGruposDoEvento.ItemsSource = this.evento_agendado.ListaGruposDoEventoAgendado;
            dataGridAdicionaConvidado.ItemsSource = this.evento_agendado.ListConvidados;
            textBoxOrganizador.Text = this.evento_agendado.Usuario.Nome_usuario;
        }
        // método para para instanciar objetos no formulário
        private void CarregarObjetosPadrao()
        {
            daoEvento_agendado = new DAOEvento_agendado(FabricaConexao.Conexao);
            daoGrupos_do_evento_agendado = new DAOGrupos_do_evento_agendado(FabricaConexao.Conexao);
            daoConvidados_do_evento = new DAOConvidados_do_evento(FabricaConexao.Conexao);
        }
        // método para carregar os dados da DAO nos campos do formulário
        private bool ValidarCampos()
        {
            if (this.evento_agendado.Evento.Id_evento == 0)
            {
                imageConsultaEvento.Focus();
                throw new Exception("Selecione um evento.");
            }
            else if (this.evento_agendado.ListaGruposDoEventoAgendado.Count == 0)
            {
                imageConsultaGrupo.Focus();
                throw new Exception("Adicione pelo menos um grupo de participantes.");
            }
            else if (this.evento_agendado.Id_do_local_de_evento == 0)
            {
                imageConsultaLocais.Focus();
                throw new Exception("Infome o local do evento.");
            }
            else if (datePickerDataHorarioAgendamento.Value == null)
            {
                datePickerDataHorarioAgendamento.Focus();
                throw new Exception("Informe a data do agendamento.");
            }

            return true;
        }

        private void Gravar()
        {
            try
            {
                if (ValidarCampos())
                {
                    if (MessageBox.Show("Tem certeza que deseja agendar o evento?", "Confirmar agendamento", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SalvarNaMemoria();
                        if (this.evento_agendado.Id_evento_agendado == 0)
                        {
                            this.evento_agendado.Id_evento_agendado = (int)daoEvento_agendado.Inserir(this.evento_agendado);
                            foreach (Grupo_de_participante grupo in this.evento_agendado.ListaGruposDoEventoAgendado)
                            {
                                Grupos_do_evento_agendado grupos_do_evento_agendado = new Grupos_do_evento_agendado()
                                {
                                    Id_do_evento_agendado = this.evento_agendado.Id_evento_agendado,
                                    Id_do_grupo = grupo.Id_grupo_de_participante
                                };
                                daoGrupos_do_evento_agendado.Inserir(grupos_do_evento_agendado);
                            }

                            foreach (Convidado convidado in evento_agendado.ListConvidados)
                            {
                                Convidados_do_evento convidado_do_evento = new Convidados_do_evento()
                                {
                                    Id_do_convidado = convidado.Id_convidado,
                                    Id_do_evento_agendado = this.evento_agendado.Id_evento_agendado
                                };
                                daoConvidados_do_evento.Inserir(convidado_do_evento);
                            }
                        }
                        else
                        {

                            daoEvento_agendado.Atualiza(this.evento_agendado, "id_evento_agendado = '" + this.evento_agendado.Id_evento_agendado + "'");
                        }
                    }

                    Limpar();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }

        }

        private void SalvarNaMemoria()
        {
            this.evento_agendado.DataHora = datePickerDataHorarioAgendamento.Value;
            this.evento_agendado.Horario_termino = timePickerHorarioTermino.Value;
            this.evento_agendado.Horario_encontro = timePickerHorarioEncontro.Value;
            this.evento_agendado.Capacidade_participante = integerUpDownCapacidade.Value;
            this.evento_agendado.Descricao_evento_agendado = TextBoxDescricao.Text;
            this.evento_agendado.Status_evento_agendado = comboBoxStatusEvento.SelectedIndex.ToString();
            this.evento_agendado.Id_do_evento = this.evento_agendado.Evento.Id_evento;
        }
        private void imageBotaoEventViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {


            Gravar();

        }
        private void Limpar()
        {
            this.evento_agendado = new Evento_agendado();
            textBoxId.Clear();
            TextBoxDescricao.Clear();
            datePickerDataHorarioAgendamento.Value = null;
            timePickerHorarioTermino.Value = null;
            timePickerHorarioEncontro.Value = null;
            integerUpDownCapacidade.Value = null;
            dataGridAdicionaConvidado.ItemsSource = null;
            dataGridGruposDoEvento.ItemsSource = null;
            comboBoxStatusEvento.SelectedIndex = 1;
            TextBoxConsultaEvento.Clear();
            TextBoxConsultaLocal.Clear();
        }

        private void imageConsultaLocais_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SalvarNaMemoria();
            BUSCA_DE_LOCAL busca_de_local = new BUSCA_DE_LOCAL(this.evento_agendado);
            busca_de_local.radioButtonCadastroInativo.Visibility = System.Windows.Visibility.Hidden;
            busca_de_local.RadioButtonTodos.Visibility = System.Windows.Visibility.Hidden;
            this.Close();
            busca_de_local.ShowDialog();

        }

        private void imageBotaoEncerrarEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.evento_agendado != null)
            {
                if (this.evento_agendado.Id_evento_agendado != 0)
                {
                    if (MessageBox.Show("Deseja encerrar o evento agendado?", "Confirmar encerramento", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        try
                        {
                            this.evento_agendado.Status_evento_agendado = "0";
                            daoEvento_agendado.Atualiza(this.evento_agendado, "id_evento_agendado = '" + this.evento_agendado.Id_evento_agendado + "'");
                            Limpar();
                        }
                        catch (Exception erro)
                        {

                            MessageBox.Show(erro.Message);
                        }

                    }
                }
            }
        }

        private void imageConsultEvento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuscarEvento();
        }
        private void BuscarEvento()
        {
            BUSCAR_EVENTO b = new BUSCAR_EVENTO(this.evento_agendado);
            b.radioButtonCadastroInativo.Visibility = System.Windows.Visibility.Hidden;
            b.RadioButtonTodos.Visibility = System.Windows.Visibility.Hidden;
            this.Close();
            b.ShowDialog();
        }

        private void imageConsultaGrupo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SalvarNaMemoria();
            CONSULTAR_GRUPO_PARTICIPANTE consultarGrupo = new CONSULTAR_GRUPO_PARTICIPANTE(this.evento_agendado);
            this.Close();
            consultarGrupo.ShowDialog();
        }

        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Object obj = daoEvento_agendado.BuscarRegistro("id_evento = '" + textBoxId.Text + "'");
                if (((List<Evento_agendado>)obj).Count > 0)
                {
                    this.evento_agendado = ((List<Evento_agendado>)obj)[0];
                    PopularCampos();
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        private void imageConsultaConvidados_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SalvarNaMemoria();
            CONSULTAR_CONVIDADO consultarconvidado = new CONSULTAR_CONVIDADO(this.evento_agendado);
            this.Close();
            consultarconvidado.ShowDialog();
        }
    }
}
