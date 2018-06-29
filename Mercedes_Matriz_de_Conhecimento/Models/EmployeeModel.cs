using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class EmployeeModel
    {
        public string idFuncionario { get; set; }

        [MaxLength(20)]
        public string RE { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [MaxLength(400)]
        public string JustificativaNaoAtivo { get; set; }

        public int idBU_Atual { get; set; }

        public int idBU_Origem { get; set; }

        public WorkzoneModel idWorkzone { get; set; }

        [MaxLength(100)]
        public string identificadorAuxiliar { get; set; }
    }
}