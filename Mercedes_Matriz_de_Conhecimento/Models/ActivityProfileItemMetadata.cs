using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActivityProfileItemMetadata
    {
        public int idPerfilAtivItem { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Logar Transicao")]
        public bool LogarTransicao { get; set; }
    }
}