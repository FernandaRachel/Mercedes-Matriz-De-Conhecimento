using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActiviesListModel
    {

        public ActiviesListModel()
        {
        }

        public int idWorkzone { get; set; }

        public IEnumerable<tblAtividades> activies { get; set; }

        public IEnumerable<tblAtividades> activiesAdded { get; set; }
    }
}