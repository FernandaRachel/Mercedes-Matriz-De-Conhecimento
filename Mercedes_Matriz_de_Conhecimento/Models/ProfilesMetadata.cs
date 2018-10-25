using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ProfilesMetadata
    {

        public int IdPerfis { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [StringLength(50, ErrorMessage = "Nome deve conter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [StringLength(20, ErrorMessage = "Nome deve conter no máximo 50 caracteres")]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}