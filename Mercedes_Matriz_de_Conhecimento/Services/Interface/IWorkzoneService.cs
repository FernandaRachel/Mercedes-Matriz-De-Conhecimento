using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IWorkzoneService
    {

        IEnumerable<tblWorkzone> GetWorkzones();

        tblWorkzone GetWorkzoneById(int id);

        tblWorkzone CreateWorkzone(tblWorkzone Workzone);

        tblWorkzone UpdateWorkzone(tblWorkzone Workzone);

        tblWorkzone DeleteWorkzone(int id);

        long checkIfWorkzoneAlreadyExits(tblWorkzone Workzone);


    }
}
