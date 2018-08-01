using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PagedList;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class PerfilAtivItemXPerfilItemService : IPerfilAtivItemXPerfilItemService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilAtividadeXPerfilAtItem GetPerfilAtivItemXPerfilItemById(int idPAI, int idPA )
        {
            tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem;

            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        where f.idPerfilAtividade == idPA && f.idPerfilAtivItem == idPAI
                        orderby f.idPerfilAtividade ascending
                        select f;

            PerfilAtivItemXPerfilItem = query.FirstOrDefault();

            return PerfilAtivItemXPerfilItem;
        }

        public IEnumerable<tblPerfilAtividadeXPerfilAtItem> GetPerfilAtivItemXPerfilItems()
        {
            IEnumerable<tblPerfilAtividadeXPerfilAtItem> PerfilAtivItemXPerfilItem;

            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        orderby f.idPerfilAtividade ascending
                        select f;

            PerfilAtivItemXPerfilItem = query.AsEnumerable();

            return PerfilAtivItemXPerfilItem;
        }


        public tblPerfilAtividadeXPerfilAtItem CreatePerfilAtivItemXPerfilItem(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem)
        {
            _db.tblPerfilAtividadeXPerfilAtItem.Add(PerfilAtivItemXPerfilItem);

            _db.SaveChanges();


            return PerfilAtivItemXPerfilItem;
        }

        public tblPerfilAtividadeXPerfilAtItem DeletePerfilAtivItemXPerfilItem(int idPAI, int idPA)
        {
            tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem;

            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        where f.idPerfilAtividade == idPA && f.idPerfilAtivItem == idPAI
                        orderby f.idPerfilAtividade ascending
                        select f;

            PerfilAtivItemXPerfilItem = query.FirstOrDefault();

            _db.tblPerfilAtividadeXPerfilAtItem.Remove(PerfilAtivItemXPerfilItem);
            _db.SaveChanges();

            return PerfilAtivItemXPerfilItem;
        }


        public tblPerfilAtividadeXPerfilAtItem UpdatePerfilAtivItemXPerfilItem(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem)
        {
            var PerfilAtivItemXPerfilItemToUpdate =
                from f in _db.tblPerfilAtividadeXPerfilAtItem
                where f.idPerfilAtividade == PerfilAtivItemXPerfilItem.idPerfilAtividade
                && f.idPerfilAtivItem == PerfilAtivItemXPerfilItem.idPerfilAtivItem
                orderby f.idPerfilAtividade ascending
                select f;
            var AuxPAIXPI = PerfilAtivItemXPerfilItemToUpdate.FirstOrDefault();

            AuxPAIXPI.idPerfilAtivItem = PerfilAtivItemXPerfilItem.idPerfilAtivItem;
            AuxPAIXPI.idPerfilAtividade = PerfilAtivItemXPerfilItem.idPerfilAtividade;
            AuxPAIXPI.Ordem = PerfilAtivItemXPerfilItem.Ordem;


            _db.Entry(AuxPAIXPI).State = EntityState.Modified;
            _db.SaveChanges();


            return PerfilAtivItemXPerfilItemToUpdate.FirstOrDefault();
        }


        public bool checkIfPerfilAtivItemXPerfilItemAlreadyExits(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem)
        {
            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        where f.idPerfilAtividade == PerfilAtivItemXPerfilItem.idPerfilAtividade
                        && f.idPerfilAtivItem == PerfilAtivItemXPerfilItem.idPerfilAtivItem
                        orderby f.idPerfilAtividade ascending
                        select f;

            if (query.Count() == 1)
                return true;

            return false;
        }

        public bool checkIfOrderAlreadyExits(tblPerfilAtividadeXPerfilAtItem PerfilAtivItemXPerfilItem)
        {
            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        where f.Ordem == PerfilAtivItemXPerfilItem.Ordem
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblPerfilAtividadeXPerfilAtItem> GetPerfilAtivItemXPerfilItemsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfilAtividadeXPerfilAtItem> PerfilAtivItemXPerfilItem;

            var query = from f in _db.tblPerfilAtividadeXPerfilAtItem
                        orderby f.idPerfilAtividade ascending
                        select f;

            PerfilAtivItemXPerfilItem = query.ToPagedList(pageNumber, quantity);

            return PerfilAtivItemXPerfilItem;
        }
    }
}