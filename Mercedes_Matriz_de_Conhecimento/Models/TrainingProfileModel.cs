using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingProfileModel
    {
        public int idPerfilTreinamento { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(20)]
        public string UsuarioCricao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}