using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingProfileService
    {

        IEnumerable<tblPerfilTreinamento> GetTrainingProfiles();

        tblPerfilTreinamento GetTrainingProfileById(int id);

        tblPerfilTreinamento CreateTrainingProfile(tblPerfilTreinamento TrainingProfile);

        tblPerfilTreinamento UpdateTrainingProfile(tblPerfilTreinamento TrainingProfileProfile);

        tblPerfilTreinamento DeleteTrainingProfile(int id);

        bool checkIfTrainingProfileAlreadyExits(tblPerfilTreinamento TrainingProfile);

        IEnumerable<tblPerfilTreinamento> GetTrainingProfilesWithPagination(int pageNumber, int quantity);

    }
}
