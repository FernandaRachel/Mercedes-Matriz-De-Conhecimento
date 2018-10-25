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
    public class ActivityProfileItemService : IActivityProfileItemService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilItens GetActivityProfileItemById(int id)
        {
            tblPerfilItens activityProfile;

            var query = from f in _db.tblPerfilItens
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            activityProfile = query.FirstOrDefault();

            return activityProfile;
        }

        public IEnumerable<tblPerfilItens> GetProfileItemActvByName(string Nome, int idActivityProfile)
        {
            List<tblPerfilItens> profileItemActv = new List<tblPerfilItens>();

            //Se o nome veio vazio traz todas Atividades
            if (Nome.Count() == 0)
                return GetActivityProfileItemsNotAssociated(idActivityProfile);

            var query = _db.tblPerfilItens.Where(f => f.Tipo == "A" && f.Sigla.Contains(Nome));

            foreach (var p2 in query)
            {
                var query2 = _db.tblPerfilAtividadeXPerfilAtItem
                    .Where(p => p.idPerfilAtividade == idActivityProfile && p.idPerfilAtivItem == p2.IdPerfilItem);

                if (query2.Count() == 0)
                    profileItemActv.Add(p2);
            }

            return profileItemActv;
        }

        public IEnumerable<tblPerfilItens> GetActivityProfileItems()
        {
            IEnumerable<tblPerfilItens> activityProfile;



            var query = from f in _db.tblPerfilItens
                        where f.Tipo == "A"
                        orderby f.Sigla ascending
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }

        public IEnumerable<tblPerfilItens> GetActivityProfileItemsNotAssociated(int idActivityProfile)
        {
            List<tblPerfilItens> activityProfile = new List<tblPerfilItens>();

            var query = from f in _db.tblPerfilItens
                        where f.Tipo == "A"
                        orderby f.Sigla ascending
                        select f;

            foreach (var p2 in query)
            {
                var query2 = _db.tblPerfilAtividadeXPerfilAtItem
                    .Where(p => p.idPerfilAtividade == idActivityProfile && p.idPerfilAtivItem == p2.IdPerfilItem);

                if (query2.Count() == 0)
                    activityProfile.Add(p2);
            }

            return activityProfile;
        }

        public tblPerfilItens CreateActivityProfileItem(tblPerfilItens ActivityProfileItem)
        {
            _db.tblPerfilItens.Add(ActivityProfileItem);

            _db.SaveChanges();


            return ActivityProfileItem;
        }

        public tblPerfilItens DeleteActivityProfileItem(int id)
        {
            tblPerfilItens ActivityProfileItem;

            var query = from f in _db.tblPerfilItens
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            ActivityProfileItem = query.FirstOrDefault();

            _db.tblPerfilItens.Remove(ActivityProfileItem);
            _db.SaveChanges();

            return ActivityProfileItem;
        }


        public tblPerfilItens UpdateActivityProfileItem(tblPerfilItens ActivityProfileItem)
        {
            var trainingToUpdate = _db.tblPerfilItens.Find(ActivityProfileItem.IdPerfilItem);
            trainingToUpdate.Sigla = ActivityProfileItem.Sigla;
            trainingToUpdate.Descricao = ActivityProfileItem.Descricao;
            trainingToUpdate.Tipo = "A";


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityProfileItemAlreadyExits(tblPerfilItens ActivityProfileItem)
        {
            var query = from f in _db.tblPerfilItens
                        where f.Sigla == ActivityProfileItem.Sigla
                        orderby f.Sigla ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfilItem != ActivityProfileItem.IdPerfilItem)
                return true;

            return false;
        }

        public IEnumerable<tblPerfilItens> GetActivityProfileItemsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfilItens> activityProfile;



            var query = from f in _db.tblPerfilItens
                        orderby f.Sigla ascending
                        where f.Tipo == "A"
                        select f;

            activityProfile = query.ToPagedList(pageNumber, quantity);

            return activityProfile;
        }

        public IEnumerable<tblPerfilItens> GetActivityProfileItemByType(tblPerfilItens ActivityProfileItem)
        {
            IEnumerable<tblPerfilItens> activityProfile;

            var query = from f in _db.tblPerfilItens
                        orderby f.Sigla ascending
                        where f.Tipo == "A"
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }
    }
}