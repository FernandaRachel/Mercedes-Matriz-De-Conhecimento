using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingTypeMetada
    {
        public int idTipoTreinamento { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(200)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [MaxLength(5)]
        public string Sigla { get; set; }

        [MaxLength(20)]
        [Display(Name = "Data Criação")]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        [MaxLength(20)]
        public string UsuarioDesativacao { get; set; }

        public DateTime DataDesativacao { get; set; }

        [Display(Name = "Ativo")]
        public bool TipoAtivo { get; set; }

        public TrainingProfileMetada idPerfilTreinamento { get; set; }
    }
}