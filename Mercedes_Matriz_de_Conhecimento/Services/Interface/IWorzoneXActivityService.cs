using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IWorzoneXActivityService
    {

        IEnumerable<tblWorkzoneXAtividades> GetWorzoneXActivities();

        tblWorkzoneXAtividades GetWorzoneXActivityById(int idWz);

        tblWorkzoneXAtividades CreateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity);

        tblWorkzoneXAtividades UpdateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity);

        tblWorkzoneXAtividades DeleteWorzoneXActivity(int idWorkzone, int idActivity);

        bool checkIfWorzoneXActivityAlreadyExits(tblWorkzoneXAtividades WorzoneXActivity);

        bool checkIfOrderAlreadyExits(tblWorkzoneXAtividades WorzoneXActivity);

        IEnumerable<tblWorkzone> GetWorzoneXActivitiesPagination(int pageNumber, int quantity);

        IEnumerable<tblAtividades> SetUpActivitiesList(int idWorkzone);

    }
}
