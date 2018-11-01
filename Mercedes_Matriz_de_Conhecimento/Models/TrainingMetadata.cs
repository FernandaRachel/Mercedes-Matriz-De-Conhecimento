using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingMetadata
    {

        public int IdTreinamento { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sigla deve ser preenchida")]
        [StringLength(5, ErrorMessage = "A sigla deve conter no máximo 5 caracteres")]
        public string Sigla { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Descrição deve ter no máximo 300 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(20, ErrorMessage = "Usuario Criacao deve ter no máximo 20 caracteres")]
        [Display(Name = "Usuário Criação")]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        [Display(Name = "Grupo de Treinamento")]
        public bool IndicaGrupoDeTreinamentos { get; set; }

        [Required(ErrorMessage = "Tipo de Treinamento deve ser preenchido")]
        [Display(Name = "Tipo de Treinamento")]
        public int idTipoTreinamento { get; set; }
    }
}