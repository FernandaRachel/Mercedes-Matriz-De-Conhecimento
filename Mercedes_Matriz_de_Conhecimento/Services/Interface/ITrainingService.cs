using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingService
    {

        IEnumerable<tblTreinamento> GetTrainings();

        tblTreinamento GetTrainingById(int id);

        tblTreinamento CreateTraining(tblTreinamento Training);

        tblTreinamento UpdateTraining(tblTreinamento Training);

        tblTreinamento DeleteTraining(int id);

        bool checkIfTrainingAlreadyExits(tblTreinamento Training);


    }
}
