using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class TrainingsListModel
    {

        public TrainingsListModel()
        {
        }

        public int IdAtividade { get; set; }

        public IEnumerable<tblTreinamento> trainings { get; set; }

        public IEnumerable<tblTreinamento> trainingsAdded { get; set; }
    }
}