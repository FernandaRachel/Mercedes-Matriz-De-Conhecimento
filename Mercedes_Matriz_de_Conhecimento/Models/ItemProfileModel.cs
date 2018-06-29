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

        [MaxLength(2)]
        public string Sigla { get; set; }

        [MaxLength(200)]
        public string Descricao{ get; set; }

    }
}