using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class AvaliacaoAtividadesModel
    {

        public AvaliacaoAtividadesModel()
        {

        }


        public int idAtividade { get; set; }

        public int idFuncionario { get; set; }

        public string sigla { get; set; }

        public string cor { get; set; }
    }
}