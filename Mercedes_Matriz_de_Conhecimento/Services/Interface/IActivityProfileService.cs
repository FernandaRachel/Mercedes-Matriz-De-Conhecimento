using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityProfileService
    {

        IEnumerable<tblPerfilAtividade> GetActivityProfiles();

        tblPerfilAtividade GetActivityProfileById(int id);

        tblPerfilAtividade CreateActivityProfile(tblPerfilAtividade ActivityProfile);

        tblPerfilAtividade UpdateActivityProfile(tblPerfilAtividade ActivityProfile);

        tblPerfilAtividade DeleteActivityProfile(int id);

        bool checkIfActivityProfileAlreadyExits(tblPerfilAtividade ActivityProfile);

        IEnumerable<tblPerfilAtividade> GetActivityProfilesWithPagination(int pageNumber, int quantity);
    }
}
