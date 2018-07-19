using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActivityProfileMetada
    {
        public int idPerfilAtividade { get; set; }

        [Required]
        public string nome { get; set; }

        public string UsuarioCriacao { get; set; }

        public System.DateTime DataCriacao { get; set; }
    }
}