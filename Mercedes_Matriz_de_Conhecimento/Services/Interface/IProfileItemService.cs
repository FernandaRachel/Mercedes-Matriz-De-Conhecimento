using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IProfileItemService
    {

        IEnumerable<tblPerfilItem> GetProfileItems();

        tblPerfilItem GetProfileItemById(int id);

        tblPerfilItem CreateProfileItem(tblPerfilItem ProfileItem);

        tblPerfilItem UpdateProfileItem(tblPerfilItem ProfileItem);

        tblPerfilItem DeleteProfileItem(int id);

        bool checkIfProfileItemAlreadyExits(tblPerfilItem ProfileItem);


    }
}
