using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingProfileMetada
    {

        public int IdPerfilTreinamento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(20)]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}