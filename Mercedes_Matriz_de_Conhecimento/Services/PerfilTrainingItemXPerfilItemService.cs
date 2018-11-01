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
    public class PerfilTrainingItemXPerfilItemService : IPerfilTrainingItemXPerfilItemService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilTreinamentoxPerfilItem GetPerfilTrainingItemXPerfilItemById(int idPerfil, int idItem)
        {
            tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem;

            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        where f.IdPerfilTreinamento == idPerfil && f.IdPerfilItem == idItem
                        orderby f.IdPerfilTreinamento ascending
                        select f;

            PerfilTrainingItemXPerfilItem = query.FirstOrDefault();

            return PerfilTrainingItemXPerfilItem;
        }


        public IEnumerable<tblPerfilTreinamentoxPerfilItem> GetPerfilTrainingItemXPerfilItems()
        {
            IEnumerable<tblPerfilTreinamentoxPerfilItem> PerfilTrainingItemXPerfilItem;

            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        orderby f.IdPerfilTreinamento ascending
                        select f;

            PerfilTrainingItemXPerfilItem = query.AsEnumerable();

            return PerfilTrainingItemXPerfilItem;
        }


        public tblPerfilTreinamentoxPerfilItem CreatePerfilTrainingItemXPerfilItem(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem)
        {
            _db.tblPerfilTreinamentoxPerfilItem.Add(PerfilTrainingItemXPerfilItem);

            _db.SaveChanges();


            return PerfilTrainingItemXPerfilItem;
        }

        public tblPerfilTreinamentoxPerfilItem DeletePerfilTrainingItemXPerfilItem(int idProfile, int idItem)
        {
            tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem;

            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        where f.IdPerfilTreinamento == idProfile && f.IdPerfilItem == idItem
                        orderby f.IdPerfilTreinamento ascending
                        select f;

            PerfilTrainingItemXPerfilItem = query.FirstOrDefault();

            _db.tblPerfilTreinamentoxPerfilItem.Remove(PerfilTrainingItemXPerfilItem);
            _db.SaveChanges();

            return PerfilTrainingItemXPerfilItem;
        }


        public tblPerfilTreinamentoxPerfilItem UpdatePerfilTrainingItemXPerfilItem(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem)
        {
            var PerfilTrainingItemXPerfilItemToUpdate =
                from f in _db.tblPerfilTreinamentoxPerfilItem
                where f.IdPerfilTreinamento == PerfilTrainingItemXPerfilItem.IdPerfilTreinamento
                && f.IdPerfilItem == PerfilTrainingItemXPerfilItem.IdPerfilItem
                orderby f.IdPerfilTreinamento ascending
                select f;
            var AuxPAIXPI = PerfilTrainingItemXPerfilItemToUpdate.FirstOrDefault();

            AuxPAIXPI.IdPerfilItem = PerfilTrainingItemXPerfilItem.IdPerfilItem;
            AuxPAIXPI.IdPerfilTreinamento = PerfilTrainingItemXPerfilItem.IdPerfilTreinamento;
            AuxPAIXPI.Ordem = PerfilTrainingItemXPerfilItem.Ordem;


            _db.Entry(AuxPAIXPI).State = EntityState.Modified;
            _db.SaveChanges();


            return PerfilTrainingItemXPerfilItemToUpdate.FirstOrDefault();
        }


        public bool checkIfPerfilTrainingItemXPerfilItemAlreadyExits(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem)
        {
            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        where f.IdPerfilTreinamento == PerfilTrainingItemXPerfilItem.IdPerfilTreinamento
                        && f.IdPerfilTreinamento == PerfilTrainingItemXPerfilItem.IdPerfilTreinamento
                        orderby f.IdPerfilTreinamento ascending
                        select f;

            if (query.Count() == 1)
                return true;

            return false;
        }

        public bool checkIfOrderAlreadyExits(tblPerfilTreinamentoxPerfilItem PerfilTrainingItemXPerfilItem)
        {
            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        where f.Ordem == PerfilTrainingItemXPerfilItem.Ordem
                        && f.IdPerfilTreinamento == PerfilTrainingItemXPerfilItem.IdPerfilTreinamento
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblPerfis> GetPerfilTrainingItemXPerfilItemsWithPagination(int pageNumber, int quantity)
        {

            List<tblPerfis> Perfis = new List<tblPerfis>();

            var q = _db.tblPerfilTreinamentoxPerfilItem
                .Select(a => a.IdPerfilTreinamento).Distinct().AsQueryable();

            var q2 = _db.tblPerfis;

            //Seleciona apenas as workzones que possuem associações na 'tblWorkzoneXAtividades'
            foreach (var x in q)
            {
                var retorno = q2.Where(a => a.IdPerfis == x);
                Perfis.Add(retorno.FirstOrDefault());
            }

            // convert de List to Enumerable
            var returnedProfiles = Perfis.AsEnumerable();
            returnedProfiles = returnedProfiles.ToPagedList(pageNumber, quantity);

            return returnedProfiles;
            //////////////////////
        }

        public IEnumerable<tblPerfilItens> SetUpPerfilItensLista(int idPerfil)
        {
            List<tblPerfilItens> allPerfilItens = new List<tblPerfilItens>();

            var query = from f in _db.tblPerfilTreinamentoxPerfilItem
                        where f.IdPerfilTreinamento == idPerfil
                        select f;

            foreach (var training in query)
            {
                var query2 = from f in _db.tblPerfilItens
                             where f.IdPerfilItem == training.IdPerfilItem
                             select f;
                allPerfilItens.Add(query2.FirstOrDefault());
            }

            return allPerfilItens;
        }
    }
}