using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingModel
    {
        public int idTreinamento { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(4)]
        public string Sigla { get; set; }

        [MaxLength(300)]
        public string Descricao { get; set; }

        [MaxLength(20)]
        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool indicaGrupoDeTreinamento { get; set; }

        public int idTipoTreinamento { get; set; }
    }
}