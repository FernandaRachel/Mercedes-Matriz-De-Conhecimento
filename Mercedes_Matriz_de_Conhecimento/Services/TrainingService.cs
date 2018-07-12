using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class TrainingService : ITrainingService
    {

        public DbConnection _db = new DbConnection();



        public tblTreinamento GetTrainingById(int id)
        {
            tblTreinamento training;

            var query = from f in _db.tblTreinamento
                        where f.IdTreinamento == id
                        orderby f.Nome
                        select f;

            training = query.FirstOrDefault();

            return training;
        }

        public IEnumerable<tblTreinamento> GetTrainings()
        {
            IEnumerable<tblTreinamento> training;



            var query = from f in _db.tblTreinamento
                        orderby f.Nome
                        select f;

            training = query.AsEnumerable();

            return training;
        }


        public tblTreinamento CreateTraining(tblTreinamento Training)
        {
            _db.tblTreinamento.Add(Training);

            _db.SaveChanges();


            return Training;
        }

        public tblTreinamento DeleteTraining(int id)
        {
            tblTreinamento Training;

            var query = from f in _db.tblTreinamento
                        where f.IdTreinamento == id
                        orderby f.Nome
                        select f;

            Training = query.FirstOrDefault();

            _db.tblTreinamento.Remove(Training);
            _db.SaveChanges();

            return Training;
        }

        
        public tblTreinamento UpdateTraining(tblTreinamento Training)
        {
            var trainingToUpdate = _db.tblTreinamento.Find(Training.IdTreinamento);
            trainingToUpdate.Nome = Training.Nome;
            trainingToUpdate.Sigla = Training.Sigla;
            trainingToUpdate.Descricao = Training.Descricao;
            trainingToUpdate.IndicaGrupoDeTreinamentos = Training.IndicaGrupoDeTreinamentos;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfTrainingAlreadyExits(tblTreinamento Training)
        {
            var query = from f in _db.tblTreinamento
                        where f.Nome == Training.Nome
                        orderby f.Nome
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdTreinamento != Training.IdTreinamento)
                return true;

            return false;
        }
    }
}