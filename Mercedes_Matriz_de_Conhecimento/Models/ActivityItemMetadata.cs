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

        [Required(ErrorMessage = "A sigla deve ser preenchida")]
        [StringLength(2, ErrorMessage = "A sigla deve conter no máximo 2 carácteres")]
        public string Sigla { get; set; }

        [StringLength(200, ErrorMessage = "Descrição deve conter no máximo 200 carácteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}