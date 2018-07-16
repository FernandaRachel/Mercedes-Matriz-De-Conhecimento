using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class WorkzoneMetadata
    {
        public int IdWorkzone { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string UsuarioCriacao { get; set; }

        [Display(Name = "Data Criacao")]
        public System.DateTime DataCriacao { get; set; }

        [Display(Name = "Usuario Desativação")]
        public string UsuarioDesativacao { get; set; }

        public string DataDesativacao { get; set; }

        [Display(Name = "Centro de Custo")]
        public Nullable<int> IdCentroDeCusto { get; set; }

        [Display(Name = "Pessoas Necessárias")]
        public int PessoasNecessarias { get; set; }

        [Display(Name = "Data de Alteração")]
        public Nullable<System.DateTime> DataAlteracao { get; set; }

        [MaxLength(20)]
        [Display(Name = "Usuário Alteração")]
        public string UsuarioAlteracao { get; set; }

        [MaxLength(400)]
        [Display(Name = "Dados Ultima Alteração")]
        public string DadosUltimaAlteracao { get; set; }

        [Display(Name = "Motivo Última Alteração")]
        public string MotivoUltimaAlteracao { get; set; }

        [Display(Name = "Ativo")]
        public bool FlagAtivo { get; set; }

    }
}