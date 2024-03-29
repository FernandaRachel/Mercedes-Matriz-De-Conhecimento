﻿using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PagedList;
using Microsoft.Ajax.Utilities;

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

        public IEnumerable<tblAtividadeXTreinamentos> GetActivityXTrainingListByIdActv(int id)
        {
            IEnumerable<tblAtividadeXTreinamentos> ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtivTreinamento == id
                        orderby f.idAtivTreinamento ascending
                        select f;

            ActivityXTraining = query.ToList();

            return ActivityXTraining;
        }

        public IEnumerable<tblAtividadeXTreinamentos> GetActivityXTraining()
        {
            IEnumerable<tblAtividadeXTreinamentos> ActivityXTraining;

            var query = _db.tblAtividadeXTreinamentos
                .DistinctBy(a => a.idAtividade)
                .OrderBy(a => a.idAtividade);

            query.Distinct();

            ActivityXTraining = query.AsEnumerable();

            return ActivityXTraining;
        }


        public tblAtividadeXTreinamentos CreateActivityXTraining(tblAtividadeXTreinamentos ActivityXTraining)
        {
            _db.tblAtividadeXTreinamentos.Add(ActivityXTraining);

            _db.SaveChanges();


            return ActivityXTraining;
        }

        public tblAtividadeXTreinamentos DeleteActivityXTraining(int idActivity, int idTrain)
        {
            tblAtividadeXTreinamentos ActivityXTraining;

            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtividade == idActivity && f.idTreinamento == idTrain
                        select f;

            ActivityXTraining = query.FirstOrDefault();
            try
            {

                _db.tblAtividadeXTreinamentos.Remove(ActivityXTraining);
                _db.SaveChanges();
                return ActivityXTraining;
            }
            catch
            {
                return ActivityXTraining;
            }

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

        public IEnumerable<tblAtividades> GetActivityXTrainingWithPagination(int pageNumber, int quantity)
        {
            List<tblAtividades> Activity = new List<tblAtividades>();

            var q = _db.tblAtividadeXTreinamentos
                .Select(a => a.idAtividade).Distinct().AsQueryable();

            var q2 = _db.tblAtividades;


            // TESTA ISSO AQUI DEPOIS - VE SE APARECE UM DE CADA ID 
            foreach (var x in q)
            {
                var retorno = q2.Where(a => a.idAtividade == x);
                Activity.Add(retorno.FirstOrDefault());
            }

            //var q = _db.tblAtividadeXTreinamentos
            //    .DistinctBy(a => a.idAtividade).AsQueryable();
            var returnedActivity = Activity.AsEnumerable();
            returnedActivity = returnedActivity.ToPagedList(pageNumber, quantity);

            return returnedActivity;
        }

        public IEnumerable<tblTreinamento> SetUpTrainingList(int idActivity)
        {
            List<tblTreinamento> allTrainings = new List<tblTreinamento>();

            var query = from f in _db.tblAtividadeXTreinamentos
                        where f.idAtividade == idActivity
                        select f;

            foreach (var trainings in query)
            {
                var query2 = from f in _db.tblTreinamento
                             where f.IdTreinamento == trainings.idTreinamento
                             select f;
                allTrainings.Add(query2.FirstOrDefault());
            }

            return allTrainings;
        }
    }
}