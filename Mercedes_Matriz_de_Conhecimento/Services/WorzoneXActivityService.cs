using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class WorzoneXActivityService : IWorzoneXActivityService
    {

        public DbConnection _db = new DbConnection();



        public tblWorkzoneXAtividades GetWorzoneXActivityById(int id)
        {
            tblWorkzoneXAtividades WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzoneAtividade == id
                        orderby f.idWorkzoneAtividade
                        select f;

            WorzoneXActivity = query.FirstOrDefault();

            return WorzoneXActivity;
        }

        public IEnumerable<tblWorkzoneXAtividades> GetWorzoneXActivities()
        {
            IEnumerable<tblWorkzoneXAtividades> WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        orderby f.idWorkzoneAtividade
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

        public tblWorkzoneXAtividades DeleteWorzoneXActivity(int id)
        {
            tblWorkzoneXAtividades WorzoneXActivity;

            var query = from f in _db.tblWorkzoneXAtividades
                        where f.idWorkzoneAtividade == id
                        orderby f.idWorkzoneAtividade
                        select f;

            WorzoneXActivity = query.FirstOrDefault();

            _db.tblWorkzoneXAtividades.Remove(WorzoneXActivity);
            _db.SaveChanges();

            return WorzoneXActivity;
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
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }
    }
}