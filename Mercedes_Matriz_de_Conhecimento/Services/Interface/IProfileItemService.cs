using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IProfileItemService
    {

        IEnumerable<tblPerfilItens> GetProfileItems();

        IEnumerable<tblPerfilItens> GetProfileItemsByType(string type);

        tblPerfilItens GetProfileItemById(int id);

        tblPerfilItens CreateProfileItem(tblPerfilItens ProfileItem);

        tblPerfilItens UpdateProfileItem(tblPerfilItens ProfileItem);

        tblPerfilItens DeleteProfileItem(int id);

        bool checkIfProfileItemAlreadyExits(tblPerfilItens ProfileItem);

        IPagedList<tblPerfilItens> GetProfileItemsWithPagination(int pageNumber, int quantity);
    }
}
