using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IPerfilAtivItemXPerfilItemService
    {
        IEnumerable<tblPerfilAtividadeXPerfilAtItem> GetPerfilAtivItemXPerfilItems();

        tblPerfilAtividadeXPerfilAtItem GetPerfilAtivItemXPerfilItemById(int idAI, int idPI);

        tblPerfilAtividadeXPerfilAtItem CreatePerfilAtivItemXPerfilItem(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem);

        tblPerfilAtividadeXPerfilAtItem UpdatePerfilAtivItemXPerfilItem(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem);

        tblPerfilAtividadeXPerfilAtItem DeletePerfilAtivItemXPerfilItem(int idPAI, int idPA);

        bool checkIfPerfilAtivItemXPerfilItemAlreadyExits(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem);

        bool checkIfOrderAlreadyExits(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem);

        IEnumerable<tblPerfilAtividadeXPerfilAtItem> GetPerfilAtivItemXPerfilItemsWithPagination(int pageNumber, int quantity);
    }
}
