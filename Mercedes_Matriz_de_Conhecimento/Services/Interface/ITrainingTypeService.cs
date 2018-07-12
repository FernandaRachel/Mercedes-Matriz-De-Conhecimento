using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingTypeService
    {

        IEnumerable<tblTipoTreinamento> GetTrainingTypes();

        tblTipoTreinamento GetTrainingTypeById(int id);

        tblTipoTreinamento CreateTrainingType(tblTipoTreinamento TrainingTypeType);

        tblTipoTreinamento UpdateTrainingType(tblTipoTreinamento TrainingTypeType);

        tblTipoTreinamento DeleteTrainingType(int id);

        bool checkIfTrainingTypeAlreadyExits(tblTipoTreinamento TrainingTypeType);


    }
}
