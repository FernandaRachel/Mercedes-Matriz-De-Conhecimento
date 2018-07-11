using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class WorkzoneMetadata
    {
        public int idWorkzone { get; set; }

        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(400)]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }

        [Display(Name = "Data Criacao")]
        public DateTime DataCriacao { get; set; }

        [MaxLength(20)]
        [Display(Name = "Usuario Desativação")]
        public string UsuarioDesativacao { get; set; }

        [Display(Name = "Ativo")]
        public bool FlagAtivo { get; set; }

        [Display(Name = "Centro de Custo")]
        public string idCentroDeCusto { get; set; }

        [Display(Name = "Pessoas Necessárias")]
        public int PessoasNecessarias { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        [MaxLength(20)]
        [Display(Name = "Usuário Alteração")]
        public string UsuarioAlteracao { get; set; }


        [MaxLength(400)]
        [Display(Name = "Dados Ultima Alteração")]
        public string DadosUltimaAlteracao { get; set; }

        [MaxLength(400)]
        [Display(Name = "Motivo Última Alteração")]
        public string MotivoUltimaAlteracao { get; set; }
    }
}