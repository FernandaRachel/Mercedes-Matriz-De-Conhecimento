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

        [Required(ErrorMessage = "RE é obrigatório")]
        [StringLength(20)]
        public string RE { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [StringLength(400, ErrorMessage = "A Justificativa deve conter no máximo 400 caracteres")]
        [Display(Name = "Justificadtiva")]
        public string JustificativaNaoAtivo { get; set; }

        [Required(ErrorMessage = "BU é obrigatório")]
        [Display(Name = "BU Atual")]
        public Nullable<int> idBu_Origem { get; set; }

        [StringLength(10)]
        [Display(Name = "Identificador Auxiliar")]
        public string IdentificadorAuxiliar { get; set; }

        [Required(ErrorMessage = "ID Americas é obrigatório")]
        [Display(Name = "ID Americas")]
        public string idAmericas { get; set; }

    }
}