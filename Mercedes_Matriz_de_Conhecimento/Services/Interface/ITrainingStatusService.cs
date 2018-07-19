using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingStatusService
    {

        IEnumerable<tblTreinamentoStatus> GetTrainingStatuss();

        tblTreinamentoStatus GetTrainingStatusById(int id);

        tblTreinamentoStatus CreateTrainingStatus(tblTreinamentoStatus TrainingStatusStatus);

        tblTreinamentoStatus UpdateTrainingStatus(tblTreinamentoStatus TrainingStatusStatus);

        tblTreinamentoStatus DeleteTrainingStatus(int id);

        bool checkIfTrainingStatusAlreadyExits(tblTreinamentoStatus TrainingStatusStatus);


    }
}
