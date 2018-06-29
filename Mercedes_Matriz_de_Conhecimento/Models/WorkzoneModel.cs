using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class WorkzoneModel
    {
        public int idWorkzone { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(400)]
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        [MaxLength(20)]
        public string UsuarioDesativacao { get; set; }

        public bool FlagAtivo { get; set; }

        public string idCentroDeCusto { get; set; }

        public int PessoasNecessarias { get; set; }

        public DateTime DataAlteracao { get; set; }

        [MaxLength(20)]
        public string UsuarioAlteracao { get; set; }


        [MaxLength(400)]
        public string DadosUltimaAlteracao { get; set; }

        [MaxLength(400)]
        public string MotivoUltimaAlteracao { get; set; }
    }
}