using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class WorkzoneXAtividadesMetadata
    {
        [MaxLength(4,ErrorMessage = "Ordem deve conter no máximo 4 digitos")]
        [Required(ErrorMessage = "Ordem deve ser preenchida")]
        public int Ordem { get; set; }
    }
}