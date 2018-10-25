using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class EmployeeMetadata
    {
        public int idfuncionario { get; set; }

        [Required(ErrorMessage = "RE deve ser preenchida")]
        [StringLength(20, ErrorMessage = "RE deve ter no máximo 20 caracteres")]
        public string RE { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [StringLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [StringLength(400, ErrorMessage = "Justificativa deve conter no máximo 400 caracteres")]
        [Display(Name = "Justificadtiva")]
        public string JustificativaNaoAtivo { get; set; }

        [Required(ErrorMessage = "BU deve ser preenchida")]
        [Display(Name = "BU Atual")]
        public Nullable<int> idBu_Origem { get; set; }

        [StringLength(10, ErrorMessage = "Identificador Auxiliar deve ter no máximo 10 caracteres")]
        [Display(Name = "Identificador Auxiliar")]
        public string IdentificadorAuxiliar { get; set; }

        [StringLength(50, ErrorMessage = "ID Americas deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "ID Americas deve ser preenchido")]
        [Display(Name = "ID Americas")]
        public string idAmericas { get; set; }

    }
}