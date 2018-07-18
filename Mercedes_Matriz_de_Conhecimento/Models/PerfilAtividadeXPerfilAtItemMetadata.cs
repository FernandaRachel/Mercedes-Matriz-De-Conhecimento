using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class PerfilAtividadeXPerfilAtItemMetadata
    {
        [Required]
        [Display(Name = "Perfil Atividade")]
        public int idPerfilAtividade { get; set; }

        [Required]
        [Display(Name = "Perfil Atividade Item")]
        public int idPerfilAtivItem { get; set; }

        [Required]
        public Nullable<int> Ordem { get; set; }
    }
}