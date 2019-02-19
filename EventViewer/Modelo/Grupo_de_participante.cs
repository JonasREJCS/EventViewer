using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EventViewer.Modelo
{
    /// <summary>
    /// Classe gerada para a tabela : Grupo_de_participante.
    /// </summary>
    public class Grupo_de_participante
    {
        public Grupo_de_participante()
        {
            //
            // TODO: Logica do construtor
            //
            colecao_Participantes_Do_Grupo = new ObservableCollection<Pessoa>();
            Tipo = new Tipo_de_grupo();
        }
        public int Id_grupo_de_participante { get; set; }
        public string Descricao_grupo_de_participante { get; set; }
        public string Nome_grupo_de_participante { get; set; }
        public string Status_grupo_de_participante { get; set; }
        public bool Status_grupo
        {
            get
            {
                if (Status_grupo_de_participante.Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (Status_grupo_de_participante.Equals("1"))
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
        }
        public int Id_do_tipo_de_grupo { get; set; }
        public Tipo_de_grupo Tipo { get; set; }
        public int QuantidadeDePessoa
        {
            get
            {
                DAO.DAOPessoas_do_grupo dao = new DAO.DAOPessoas_do_grupo(DAO.FabricaConexao.Conexao);
                return dao.BuscaQuantidadePessoa(Id_grupo_de_participante);

            }
        }
        public ObservableCollection<Pessoa> colecao_Participantes_Do_Grupo { get; set; }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Grupo_de_participante outroGrupo = (Grupo_de_participante)obj;
            if (!Id_grupo_de_participante.Equals(outroGrupo.Id_grupo_de_participante))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}