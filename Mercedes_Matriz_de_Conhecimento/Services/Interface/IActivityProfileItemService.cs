using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityProfileItemService
    {

        IEnumerable<tblPerfilItens> GetActivityProfileItems();

        tblPerfilItens GetActivityProfileItemById(int id);

        IEnumerable<tblPerfilItens> GetActivityProfileItemByType(tblPerfilItens ActivityProfileItem);

        tblPerfilItens CreateActivityProfileItem(tblPerfilItens ActivityProfileItem);

        tblPerfilItens UpdateActivityProfileItem(tblPerfilItens ActivityProfileItem);

        tblPerfilItens DeleteActivityProfileItem(int id);

        bool checkIfActivityProfileItemAlreadyExits(tblPerfilItens ActivityProfileItem);

        IEnumerable<tblPerfilItens> GetActivityProfileItemsWithPagination(int pageNumber, int quantity);
    }
}
