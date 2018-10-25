using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ItemProfileModel
    {
        
        public int idPerfilItem { get; set; }

        [Required(ErrorMessage = "Sigla é obrigatória")]
        [StringLength(2, ErrorMessage = "A sigla deve conter no máximo 2 carácteres")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "A sigla é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição deve conter no máximo 200 carácteres")]
        public string Descricao{ get; set; }

    }
}