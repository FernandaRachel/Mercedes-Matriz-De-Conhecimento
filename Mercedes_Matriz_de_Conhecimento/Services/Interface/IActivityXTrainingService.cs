using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityXTrainingService
    {

        IEnumerable<tblAtividadeXTreinamentos> GetActivityXTraining();

        tblAtividadeXTreinamentos GetActivityXTrainingById(int id);

        tblAtividadeXTreinamentos CreateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining);

        tblAtividadeXTreinamentos UpdateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining);

        tblAtividadeXTreinamentos DeleteActivityXTraining(int idActivity, int Train);

        bool checkIfActivityXTrainingAlreadyExits(tblAtividadeXTreinamentos ActivityXTraining);

        IEnumerable<tblAtividades> GetActivityXTrainingWithPagination(int pageNumber, int quantity);

        IEnumerable<tblTreinamento> SetUpTrainingList(int idActivity);
    }
}
