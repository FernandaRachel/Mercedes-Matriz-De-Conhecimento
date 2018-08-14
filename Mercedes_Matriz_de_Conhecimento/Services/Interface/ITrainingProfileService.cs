using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingProfileService
    {

        IEnumerable<tblPerfis> GetTrainingProfiles();

        IEnumerable<tblPerfis> GetTrainingProfilesByType(string type);

        tblPerfis GetTrainingProfileById(int id);

        tblPerfis CreateTrainingProfile(tblPerfis TrainingProfile);

        tblPerfis UpdateTrainingProfile(tblPerfis TrainingProfileProfile);

        tblPerfis DeleteTrainingProfile(int id);

        bool checkIfTrainingProfileAlreadyExits(tblPerfis TrainingProfile);

        IEnumerable<tblPerfis> GetTrainingProfilesWithPagination(int pageNumber, int quantity);

        IEnumerable<tblPerfis> GetTrainingProfilesByType();
    }
}
