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
    public class WorzoneXActivityService : IWorzoneXActivityService
    {

        public DbConnection _db = new DbConnection();
        public ActivityGroupService _activityGroup = new ActivityGroupService();


        public tblWorkzoneXAtividades GetWorzoneXActivityById(int idWz)
        {
            tblWorkzoneXAtividades WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzone == idWz
                        orderby f.idWorkzoneAtividade ascending
                        select f;

            WorzoneXActivity = query.FirstOrDefault();

            return WorzoneXActivity;
        }

        public IEnumerable<tblWorkzoneXAtividades> GetWorzoneXActivityListByIdWz(int idWz)
        {
            IEnumerable<tblWorkzoneXAtividades> WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzone == idWz
                        orderby f.idWorkzoneAtividade ascending
                        select f;

            WorzoneXActivity = query.ToList();

            return WorzoneXActivity;
        }

        public IEnumerable<tblWorkzoneXAtividades> GetWorzoneXActivities()
        {
            IEnumerable<tblWorkzoneXAtividades> WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        orderby f.idWorkzoneAtividade ascending
                        select f;

            WorzoneXActivity = query.AsEnumerable();

            return WorzoneXActivity;
        }


        public tblWorkzoneXAtividades CreateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity)
        {
            _db.tblWorkzoneXAtividades.Add(WorzoneXActivity);

            _db.SaveChanges();


            return WorzoneXActivity;
        }

        public tblWorkzoneXAtividades DeleteWorzoneXActivity(int idWorkzone, int idActivity)
        {
            tblWorkzoneXAtividades WorkzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzone == idWorkzone && f.idAtividade == idActivity
                        select f;

            WorkzoneXActivity = query.FirstOrDefault();

            _db.tblWorkzoneXAtividades.Remove(WorkzoneXActivity);
            _db.SaveChanges();

            return WorkzoneXActivity;
        }


        public tblWorkzoneXAtividades UpdateWorzoneXActivity(tblWorkzoneXAtividades WorzoneXActivity)
        {
            var trainingToUpdate = _db.tblWorkzoneXAtividades.Find(WorzoneXActivity.idWorkzoneAtividade);
            trainingToUpdate.idWorkzone = WorzoneXActivity.idWorkzone;
            trainingToUpdate.idAtividade = WorzoneXActivity.idAtividade;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfWorzoneXActivityAlreadyExits(tblWorkzoneXAtividades WorzoneXActivity)
        {
            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idAtividade == WorzoneXActivity.idAtividade &&
                        f.idWorkzone == WorzoneXActivity.idWorkzone
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public bool checkIfOrderAlreadyExits(tblWorkzoneXAtividades WorzoneXActivity)
        {
            var query = from f in _db.tblWorkzoneXAtividades
                        where f.Ordem == WorzoneXActivity.Ordem 
                        && f.idWorkzone == WorzoneXActivity.idWorkzone
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblWorkzone> GetWorzoneXActivitiesPagination(int pageNumber, int quantity)
        {
            List<tblWorkzone> Workzone = new List<tblWorkzone>();

            var q = _db.tblWorkzoneXAtividades
                .Select(a => a.idWorkzone).Distinct().AsQueryable();

            var q2 = _db.tblWorkzone;

            //Seleciona apenas as workzones que possuem associações na 'tblWorkzoneXAtividades'
            foreach (var x in q)
            {
                var retorno = q2.Where(a => a.IdWorkzone == x);
                Workzone.Add(retorno.FirstOrDefault());
            }

            // convert de List to Enumerable
            var returnedWorkzone = Workzone.AsEnumerable();
            returnedWorkzone = returnedWorkzone.ToPagedList(pageNumber, quantity);

            return returnedWorkzone;
        }

        public IEnumerable<tblAtividades> SetUpActivitiesList(int idWorkzone)
        {
            List<tblAtividades> allActivies = new List<tblAtividades>();

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzone == idWorkzone
                        select f;

            foreach (var activies in query)
            {
                var query2 = from f in _db.tblAtividades
                             where f.idAtividade == activies.idAtividade
                             select f;
                allActivies.Add(query2.FirstOrDefault());
            }

            return allActivies;
        }

        public IEnumerable<tblAtividades> SetUpActivitiesListToMatrix(int idWorkzone)
        {
            List<tblAtividades> allActivies = new List<tblAtividades>();

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzone == idWorkzone
                        select f;

            foreach (var activies in query)
            {
                // Verifica se essa atividade é filha de outra
                var returned = _activityGroup.GetSonActivityGroupById((int)activies.idAtividade);

                //Se essa atividade n for filha de outra ele adiciona na lista para Matriz
                if (returned == null)
                {
                    var query2 = from f in _db.tblAtividades
                                 where f.idAtividade == activies.idAtividade
                                 select f;
                    allActivies.Add(query2.FirstOrDefault());
                }
            }

            return allActivies;
        }
    }
}