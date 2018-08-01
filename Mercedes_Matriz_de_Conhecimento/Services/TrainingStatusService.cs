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
    public class TrainingStatusService : ITrainingStatusService
    {

        public DbConnection _db = new DbConnection();


        public tblTreinamentoStatus GetTrainingStatusById(int id)
        {
            tblTreinamentoStatus TrainingStatus;

            var query = from f in _db.tblTreinamentoStatus
                        where f.idStatusTreinamento == id
                        orderby f.Denominacao ascending
                        select f;

            TrainingStatus = query.FirstOrDefault();

            return TrainingStatus;
        }

        public IEnumerable<tblTreinamentoStatus> GetTrainingStatuss()
        {
            IEnumerable<tblTreinamentoStatus> TrainingStatus;



            var query = from f in _db.tblTreinamentoStatus
                        orderby f.Denominacao ascending
                        select f;

            TrainingStatus = query.AsEnumerable();

            return TrainingStatus;
        }


        public tblTreinamentoStatus CreateTrainingStatus(tblTreinamentoStatus TrainingStatus)
        {
            _db.tblTreinamentoStatus.Add(TrainingStatus);

            _db.SaveChanges();


            return TrainingStatus;
        }

        public tblTreinamentoStatus DeleteTrainingStatus(int id)
        {
            tblTreinamentoStatus TrainingStatus;

            var query = from f in _db.tblTreinamentoStatus
                        where f.idStatusTreinamento == id
                        orderby f.Denominacao ascending
                        select f;

            TrainingStatus = query.FirstOrDefault();

            _db.tblTreinamentoStatus.Remove(TrainingStatus);
            _db.SaveChanges();

            return TrainingStatus;
        }

        
        public tblTreinamentoStatus UpdateTrainingStatus(tblTreinamentoStatus TrainingStatus)
        {
            var TrainingStatusToUpdate = _db.tblTreinamentoStatus.Find(TrainingStatus.idStatusTreinamento);
            TrainingStatusToUpdate.Denominacao = TrainingStatus.Denominacao;
            TrainingStatusToUpdate.Cor = TrainingStatus.Cor;

            _db.Entry(TrainingStatusToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return TrainingStatusToUpdate;
        }


        public bool checkIfTrainingStatusAlreadyExits(tblTreinamentoStatus TrainingStatus)
        {
            var query = from f in _db.tblTreinamentoStatus
                        where f.Denominacao == TrainingStatus.Denominacao
                        orderby f.Denominacao ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idStatusTreinamento != TrainingStatus.idStatusTreinamento)
                return true;

            return false;
        }

        public IEnumerable<tblTreinamentoStatus> GetTrainingStatussWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblTreinamentoStatus> TrainingStatus;



            var query = from f in _db.tblTreinamentoStatus
                        orderby f.Denominacao ascending
                        select f;

            TrainingStatus = query.ToPagedList(pageNumber,quantity);

            return TrainingStatus;
        }
    }
}