using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingTypeMetada
    {
        public int IdTipoTreinamento { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [StringLength(50,ErrorMessage = "Nome deve conter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição deve ser preenchida")]
        [StringLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Sigla deve ser preenchida")]
        [StringLength(2, ErrorMessage="Sigla deve conter no máximo 2 caracteres")]
        public string Sigla { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioDesativacao { get; set; }

        public DateTime DataDesativacao { get; set; }

        [Display(Name = "Ativo")]
        public bool TipoAtivo { get; set; }
    }
}