using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityXTrainingService
    {

        IEnumerable<tblAtividadeXTreinamentos> GetActivityXWorkzone();

        tblAtividadeXTreinamentos GetActivityXTrainingById(int id);

        tblAtividadeXTreinamentos CreateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining);

        tblAtividadeXTreinamentos UpdateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining);

        tblAtividadeXTreinamentos DeleteActivityXTraining(int id);

        bool checkIfActivityXTrainingAlreadyExits(tblAtividadeXTreinamentos ActivityXTraining);
    }
}
