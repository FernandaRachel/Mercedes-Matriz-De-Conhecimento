using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IActivityGroupService
    {
        IEnumerable<tblGrupoAtividades> GetActivityGroups();

        tblGrupoAtividades GetActivityGroupById(int id);

        tblGrupoAtividades CreateActivityGroup(tblGrupoAtividades ActivityGroup);

        tblGrupoAtividades UpdateActivityGroup(tblGrupoAtividades ActivityGroup);

        tblGrupoAtividades DeleteActivityGroup(int idDaddy, int idSon);

        List<tblAtividades> setUpActivitys(int idDaddy);

        IEnumerable<tblGrupoAtividades> GetActivityGroupsItemsWithPagination(int pageNumber, int quantity);
    }
}
