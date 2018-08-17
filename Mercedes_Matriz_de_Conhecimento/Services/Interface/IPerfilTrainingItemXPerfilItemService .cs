using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IPerfilTrainingItemXPerfilItemService
    {
        IEnumerable<tblPerfilTreinamentoxPerfilItem> GetPerfilTrainingItemXPerfilItems();

        tblPerfilTreinamentoxPerfilItem GetPerfilTrainingItemXPerfilItemById(int idProfile, int idItem);

        tblPerfilTreinamentoxPerfilItem CreatePerfilTrainingItemXPerfilItem(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem);

        tblPerfilTreinamentoxPerfilItem UpdatePerfilTrainingItemXPerfilItem(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem);

        tblPerfilTreinamentoxPerfilItem DeletePerfilTrainingItemXPerfilItem(int idPAI, int idPA);

        bool checkIfPerfilTrainingItemXPerfilItemAlreadyExits(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem);

        bool checkIfOrderAlreadyExits(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem);

        IEnumerable<tblPerfis> GetPerfilTrainingItemXPerfilItemsWithPagination(int pageNumber, int quantity);
    }
}
