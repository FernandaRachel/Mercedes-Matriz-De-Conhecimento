using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface ITrainingGroupService
    {
        IEnumerable<tblGrupoTreinamentos> GetTrainingGroups();

        tblGrupoTreinamentos GetTrainingGroupById(int id);

        tblGrupoTreinamentos CreateTrainingGroup(tblGrupoTreinamentos TrainingGroup);

        tblGrupoTreinamentos UpdateTrainingGroup(tblGrupoTreinamentos TrainingGroup);

        tblGrupoTreinamentos DeleteTrainingGroup(int idDaddy, int idSon);

        List<tblTreinamento> setUpTrainings(int idDaddy);

        IEnumerable<tblGrupoTreinamentos> GetTrainingGroupsWithPagination(int pageNumber, int quantity);
    }
}
