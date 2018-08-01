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
    public class ActivityXTrainingService : IActivityXTrainingService
    {

        public DbConnection _db = new DbConnection();



        public tblAtividadeXTreinamentos GetActivityXTrainingById(int id)
        {
            tblAtividadeXTreinamentos ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtivTreinamento == id
                        orderby f.idAtivTreinamento ascending
                        select f;

            ActivityXTraining = query.FirstOrDefault();

            return ActivityXTraining;
        }

        public IEnumerable<tblAtividadeXTreinamentos> GetActivityXWorkzone()
        {
            IEnumerable<tblAtividadeXTreinamentos> ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        orderby f.idAtivTreinamento ascending
                        select f;

            ActivityXTraining = query.AsEnumerable();

            return ActivityXTraining;
        }


        public tblAtividadeXTreinamentos CreateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining)
        {
            _db.tblAtividadeXTreinamentos.Add(ActivityXTraining);

            _db.SaveChanges();


            return ActivityXTraining;
        }

        public tblAtividadeXTreinamentos DeleteActivityXTraining(int id)
        {
            tblAtividadeXTreinamentos ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtivTreinamento == id
                        orderby f.idAtivTreinamento ascending
                        select f;

            ActivityXTraining = query.FirstOrDefault();

            _db.tblAtividadeXTreinamentos.Remove(ActivityXTraining);
            _db.SaveChanges();

            return ActivityXTraining;
        }


        public tblAtividadeXTreinamentos UpdateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining)
        {
            var trainingToUpdate = _db.tblAtividadeXTreinamentos.Find(ActivityXTraining.idAtivTreinamento);
            trainingToUpdate.idTreinamento = ActivityXTraining.idTreinamento;
            trainingToUpdate.idAtividade = ActivityXTraining.idAtividade;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityXTrainingAlreadyExits(tblAtividadeXTreinamentos ActivityXTraining)
        {
            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtividade == ActivityXTraining.idAtividade &&
                        f.idTreinamento == ActivityXTraining.idTreinamento
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblAtividadeXTreinamentos> GetActivityXWorkzoneWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblAtividadeXTreinamentos> ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        orderby f.idAtivTreinamento ascending
                        select f;

            ActivityXTraining = query.ToPagedList(pageNumber, quantity);

            return ActivityXTraining;
        }
    }
}