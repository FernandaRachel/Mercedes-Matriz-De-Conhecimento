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

        [Required]
        [MaxLength(20)]
        public string RE { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [MaxLength(400)]
        [Display(Name = "Justificadtiva")]
        public string JustificativaNaoAtivo { get; set; }

        [Display(Name = "BU Atual")]
        public Nullable<int> idBu_Origem { get; set; }

        [MaxLength(10)]
        [Display(Name = "Identificador Auxiliar")]
        public string IdentificadorAuxiliar { get; set; }

        [Display(Name = "ID Americas")]
        public int idAmericas { get; set; }

    }
}