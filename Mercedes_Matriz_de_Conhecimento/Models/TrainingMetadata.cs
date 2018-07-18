using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingMetadata
    {

        public int IdTreinamento { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(4)]
        public string Sigla { get; set; }

        [MaxLength(300)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [MaxLength(20)]
        [Display(Name = "Usuário Criação")]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        [Display(Name = "Grupo de Treinamento")]
        public bool IndicaGrupoDeTreinamentos { get; set; }

        [Display(Name = "Tipo de Treinamento")]
        public int idTipoTreinamento { get; set; }
    }
}