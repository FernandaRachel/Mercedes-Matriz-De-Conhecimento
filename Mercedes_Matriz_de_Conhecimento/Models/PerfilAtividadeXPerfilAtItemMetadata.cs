using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class PerfilAtividadeXPerfilAtItemMetadata
    {
        [Required(ErrorMessage = "Perfil Atividadae é obrigatório")]
        [Display(Name = "Perfil Atividade")]
        public int idPerfilAtividade { get; set; }

        [Required(ErrorMessage ="Perfil Atividade Item é obrigatório")]
        [Display(Name = "Perfil Atividade Item")]
        public int idPerfilAtivItem { get; set; }

        [MaxLength(4, ErrorMessage = "Ordem deve conter no máximo 4 digitos")]
        [Required(ErrorMessage = "Ordem deve ser preenchida")]
        public Nullable<int> Ordem { get; set; }
    }
}