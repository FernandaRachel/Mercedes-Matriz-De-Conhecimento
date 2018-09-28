using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class AvaliacaoAtividadesModel
    {

        public AvaliacaoAtividadesModel()
        {

        }


        public int idAtividade { get; set; }

        public int idFuncionario { get; set; }

        [Required(ErrorMessage = "A sigla é obrigatória")]
        [StringLength(2, ErrorMessage = "A sigla deve conter no máximo 2 caracteres")]
        public string sigla { get; set; }

        public string cor { get; set; }
    }
}