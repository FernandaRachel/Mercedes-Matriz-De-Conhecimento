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
    public class TrainingService : ITrainingService
    {

        public DbConnection _db = new DbConnection();
        public TrainingGroupService _trainingGroup = new TrainingGroupService();



        public tblTreinamento GetTrainingById(int id)
        {
            tblTreinamento training;

            var query = from f in _db.tblTreinamento
                        where f.IdTreinamento == id
                        orderby f.Nome ascending
                        select f;

            training = query.FirstOrDefault();

            return training;
        }


        public IEnumerable<tblTreinamento> GetTrainingByName(string Nome)
        {
            IEnumerable<tblTreinamento> employee;
            var query = _db.tblTreinamento;
            employee = query.AsEnumerable();

            if (Nome.Count() == 0)
                return employee;

            var query2 = _db.tblTreinamento
                .Where(f => f.Nome.Contains(Nome));

            employee = query2.AsEnumerable();

            return employee;
        }

        public IEnumerable<tblTreinamento> GetTrainings()
        {
            IEnumerable<tblTreinamento> training;



            var query = from f in _db.tblTreinamento
                        orderby f.Nome ascending
                        select f;

            training = query.AsEnumerable();

            return training;
        }

        public IEnumerable<tblTreinamento> GetTrainingDifferentFromFatherAndNotAGroup(int id)
        {
            IEnumerable<tblTreinamento> training;

            var query = from f in _db.tblTreinamento
                        where f.IndicaGrupoDeTreinamentos == false 
                        && f.IdTreinamento != id
                        orderby f.Nome ascending
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
                        orderby f.Nome ascending
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

            if (!Training.IndicaGrupoDeTreinamentos)
            {
                _trainingGroup.DeleteTrainingGroupByDaddy(Training.IdTreinamento);
            }

            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfTrainingAlreadyExits(tblTreinamento Training)
        {
            var query = from f in _db.tblTreinamento
                        where f.Nome == Training.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdTreinamento != Training.IdTreinamento)
                return true;

            return false;
        }

        public IPagedList<tblTreinamento> GetTrainingsWithPagination(int pageNumber, int quantity)
        {
            IPagedList<tblTreinamento> training;



            var query = from f in _db.tblTreinamento
                        orderby f.Nome ascending
                        select f;
            
            training = query.ToPagedList(pageNumber, quantity);

            return training;
        }
    }
}