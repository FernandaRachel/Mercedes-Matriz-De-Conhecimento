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

        tblWorkzoneXAtividades GetWorzoneXActivityById(int id);

        tblWorkzoneXAtividades CreateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity);

        tblWorkzoneXAtividades UpdateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity);

        tblWorkzoneXAtividades DeleteWorzoneXActivity(int id);

        bool checkIfWorzoneXActivityAlreadyExits(tblWorkzoneXAtividades WorzoneXActivity);


    }
}
