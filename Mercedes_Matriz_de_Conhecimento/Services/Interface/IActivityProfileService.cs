using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityProfileService
    {

        IEnumerable<tblPerfis> GetActivityProfiles();

        IEnumerable<tblPerfis> GetActivityProfilesByType();

        tblPerfis GetActivityProfileById(int id);

        tblPerfis CreateActivityProfile(tblPerfis ActivityProfile);

        tblPerfis UpdateActivityProfile(tblPerfis ActivityProfile);

        tblPerfis DeleteActivityProfile(int id);

        bool checkIfActivityProfileAlreadyExits(tblPerfis ActivityProfile);

        IEnumerable<tblPerfis> GetActivityProfilesWithPagination(int pageNumber, int quantity);
    }
}
