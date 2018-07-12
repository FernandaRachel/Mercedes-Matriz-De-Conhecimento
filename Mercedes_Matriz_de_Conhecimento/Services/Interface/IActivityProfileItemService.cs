using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityProfileItemService
    {

        IEnumerable<tblPerfilAtivItem> GetActivityProfileItems();

        tblPerfilAtivItem GetActivityProfileItemById(int id);

        tblPerfilAtivItem CreateActivityProfileItem(tblPerfilAtivItem ActivityProfileItem);

        tblPerfilAtivItem UpdateActivityProfileItem(tblPerfilAtivItem ActivityProfileItem);

        tblPerfilAtivItem DeleteActivityProfileItem(int id);

        bool checkIfActivityProfileItemAlreadyExits(tblPerfilAtivItem ActivityProfileItem);


    }
}
