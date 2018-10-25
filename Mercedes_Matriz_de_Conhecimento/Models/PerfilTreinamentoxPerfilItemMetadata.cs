using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class PerfilTreinamentoxPerfilItemMetadata
    {
        [Required(ErrorMessage = "Perfil Treinamento deve ser preenchido")]
        [Display(Name = "Perfil Treinamento")]
        public int IdPerfilTreinamento { get; set; }

        [Required(ErrorMessage = "Perfil Treinamento Item deve ser preenchido")]
        [Display(Name = "Perfil Treinamento Item")]
        public int IdPerfilItem { get; set; }

        [Range(1,9999, ErrorMessage = "Ordem deve conter no máximo 4 digitos")]
        [Required(ErrorMessage = "Ordem deve ser preenchida")]
        public Nullable<int> Ordem { get; set; }
    }
}