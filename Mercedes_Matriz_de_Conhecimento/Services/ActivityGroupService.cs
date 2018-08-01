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
    public class ActivityGroupService : IActivityGroupService
    {

        public DbConnection _db = new DbConnection();



        public tblGrupoAtividades GetActivityGroupById(int id)
        {
            tblGrupoAtividades ActivityGroup;

            var query = from f in _db.tblGrupoAtividades
                        where f.idAtividadePai == id
                        orderby f.idAtividadeFilho ascending
                        select f;

            ActivityGroup = query.FirstOrDefault();

            return ActivityGroup;
        }

        public IEnumerable<tblGrupoAtividades> GetActivityGroups()
        {
            IEnumerable<tblGrupoAtividades> ActivityGroup;

            var query = from f in _db.tblGrupoAtividades
                        orderby f.idAtividadePai ascending
                        select f;

            ActivityGroup = query.AsEnumerable();

            return ActivityGroup;
        }


        public tblGrupoAtividades CreateActivityGroup(tblGrupoAtividades ActivityGroup)
        {
            _db.tblGrupoAtividades.Add(ActivityGroup);

            _db.SaveChanges();


            return ActivityGroup;
        }

        public tblGrupoAtividades DeleteActivityGroup(int idDaddy, int idSon)
        {
            tblGrupoAtividades ActivityGroup;

            var query = from f in _db.tblGrupoAtividades
                        where f.idAtividadePai == idDaddy && f.idAtividadeFilho == idSon
                        orderby f.idAtividadePai ascending
                        select f;

            ActivityGroup = query.FirstOrDefault();

            _db.tblGrupoAtividades.Remove(ActivityGroup);
            _db.SaveChanges();

            return ActivityGroup;
        }


        public tblGrupoAtividades UpdateActivityGroup(tblGrupoAtividades ActivityGroup)
        {
            var ActivityGroupToUpdate = _db.tblGrupoAtividades.Find(ActivityGroup.idAtividadePai);
            ActivityGroupToUpdate.idAtividadePai = ActivityGroup.idAtividadePai;
            ActivityGroupToUpdate.idAtividadeFilho = ActivityGroup.idAtividadeFilho;



            _db.Entry(ActivityGroupToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return ActivityGroupToUpdate;
        }

        public List<tblAtividades> setUpActivitys(int idDaddy)
        {
            List<tblAtividades> allSons = new List<tblAtividades>();

            var query = from f in _db.tblGrupoAtividades
                        where f.idAtividadePai == idDaddy
                        select f;

            foreach (var son in query)
            {
                var query2 = from f in _db.tblAtividades
                             where f.idAtividade == son.idAtividadeFilho
                             select f;
                allSons.Add(query2.FirstOrDefault());
            }

            return allSons;
        }


        public bool checkIfActivityGroupAlreadyExits(tblGrupoAtividades ActivityGroup)
        {
            var query = from f in _db.tblGrupoAtividades
                        where f.idAtividadePai == ActivityGroup.idAtividadePai &&
                        f.idAtividadeFilho == ActivityGroup.idAtividadeFilho
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblGrupoAtividades> GetActivityGroupsItemsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblGrupoAtividades> ActivityGroup;

            var query = from f in _db.tblGrupoAtividades
                        orderby f.idAtividadePai ascending
                        select f;

            ActivityGroup = query.ToPagedList(pageNumber, quantity);

            return ActivityGroup;
        }
    }
}