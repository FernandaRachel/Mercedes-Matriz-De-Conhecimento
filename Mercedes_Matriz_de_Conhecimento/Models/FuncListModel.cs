using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class FuncListModel
    {

        public FuncListModel()
        {

        }

        public int idWorkzone { get; set; }

        public IEnumerable<tblFuncionarios> funcionarios { get; set; }

        public IEnumerable<tblFuncionarios> funcionariosAdded { get; set; }
    }
}