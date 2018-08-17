using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ProfileItemListModel
    {

        public ProfileItemListModel()
        {

        }

        public int idProfile { get; set; }

        public string ProfileName { get; set; }

        public IEnumerable<tblPerfilItens> ProfileItem { get; set; }

        public IEnumerable<tblPerfilItens> ProfileItemAdded { get; set; }
    }
}