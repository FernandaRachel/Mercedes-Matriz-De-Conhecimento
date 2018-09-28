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

        [Required(ErrorMessage = "A sigla é obrigatória"), StringLength(2, ErrorMessage = "A sigla deve conter no máximo 2 caracteres")]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
       
    }
}