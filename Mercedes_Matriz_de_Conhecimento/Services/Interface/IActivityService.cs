using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityService
    {

        IEnumerable<tblAtividades> GetActivities();

        tblAtividades GetActivityById(int id);

        tblAtividades CreateActivity(tblAtividades Activity);

        tblAtividades UpdateActivity(tblAtividades Activity);

        tblAtividades DeleteActivity(int id);

        bool checkIfActivityAlreadyExits(tblAtividades Activity);

        IEnumerable<tblAtividades> GetActivitiesWithPagination(int pageNumber, int quantity);
    }
}
