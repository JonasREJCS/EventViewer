using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventViewer.DAO;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Evento_agendado.
    /// </summary>
    public class Evento_agendado
    {
        public Evento_agendado()
        {
            //
            // TODO: Logica do construtor
            //
            ListaGruposDoEventoAgendado = new ObservableCollection<Grupo_de_participante>();
            ListConvidados = new ObservableCollection<Convidado>();
            Evento = new Evento();
            Local_de_evento = new Local_de_evento();
            Usuario = new Usuario();
        }
        public int Id_evento_agendado { get; set; }
        public DateTime? Data_publicacao { get; set; }
        public string Status_evento_agendado { get; set; }
        public DateTime? DataHora { get; set; }
        public string Descricao_evento_agendado { get; set; }
        public int Id_do_evento { get; set; }
        public Evento Evento { get; set; }
        public DateTime? Horario_termino { get; set; }
        public DateTime? Horario_encontro { get; set; }
        public int Id_do_usuario_organizador { get; set; }
        public int? Capacidade_participante { get; set; }
        public int QuantidadeParticipante
        {
            get
            {


                if (ListaGruposDoEventoAgendado.Count > 0)
                {
                    int qtdParticipantes = 0;
                    foreach (Grupo_de_participante grupo in ListaGruposDoEventoAgendado)
                    {
                        qtdParticipantes += grupo.QuantidadeDePessoa;
                    }
                    return qtdParticipantes;
                }
                else
                {
                    return 0;
                };
            }
            set { }
        }
        public int Id_do_local_de_evento { get; set; }
        public Local_de_evento Local_de_evento { get; set; }
        public ObservableCollection<Grupo_de_participante> ListaGruposDoEventoAgendado { get; set; }
        public ObservableCollection<Convidado> ListConvidados { get; set; }
        public Usuario Usuario { get; set; }
    }
}