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



        public tblPerfilAtivItem GetActivityProfileItemById(int id)
        {
            tblPerfilAtivItem activityProfile;

            var query = from f in _db.tblPerfilAtivItem
                        where f.idPerfilAtivItem == id
                        orderby f.Sigla ascending
                        select f;

            activityProfile = query.FirstOrDefault();

            return activityProfile;
        }

        public IEnumerable<tblPerfilAtivItem> GetActivityProfileItems()
        {
            IEnumerable<tblPerfilAtivItem> activityProfile;



            var query = from f in _db.tblPerfilAtivItem
                        orderby f.Sigla ascending
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }


        public tblPerfilAtivItem CreateActivityProfileItem(tblPerfilAtivItem ActivityProfileItem)
        {
            _db.tblPerfilAtivItem.Add(ActivityProfileItem);

            _db.SaveChanges();


            return ActivityProfileItem;
        }

        public tblPerfilAtivItem DeleteActivityProfileItem(int id)
        {
            tblPerfilAtivItem ActivityProfileItem;

            var query = from f in _db.tblPerfilAtivItem
                        where f.idPerfilAtivItem == id
                        orderby f.Sigla ascending
                        select f;

            ActivityProfileItem = query.FirstOrDefault();

            _db.tblPerfilAtivItem.Remove(ActivityProfileItem);
            _db.SaveChanges();

            return ActivityProfileItem;
        }


        public tblPerfilAtivItem UpdateActivityProfileItem(tblPerfilAtivItem ActivityProfileItem)
        {
            var trainingToUpdate = _db.tblPerfilAtivItem.Find(ActivityProfileItem.idPerfilAtivItem);
            trainingToUpdate.Sigla = ActivityProfileItem.Sigla;
            trainingToUpdate.Descricao = ActivityProfileItem.Descricao;
            trainingToUpdate.LogarTransicao = ActivityProfileItem.LogarTransicao;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityProfileItemAlreadyExits(tblPerfilAtivItem ActivityProfileItem)
        {
            var query = from f in _db.tblPerfilAtivItem
                        where f.Sigla == ActivityProfileItem.Sigla
                        orderby f.Sigla ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idPerfilAtivItem != ActivityProfileItem.idPerfilAtivItem)
                return true;

            return false;
        }

        public IEnumerable<tblPerfilAtivItem> GetActivityProfileItemsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfilAtivItem> activityProfile;



            var query = from f in _db.tblPerfilAtivItem
                        orderby f.Sigla ascending
                        select f;

            activityProfile = query.ToPagedList(pageNumber, quantity);

            return activityProfile;
        }
    }
}