using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActivityItemMetadata
    {
        public int IdPerfilItem { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}