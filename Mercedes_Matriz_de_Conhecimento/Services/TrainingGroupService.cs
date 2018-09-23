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
    public class TrainingGroupService : ITrainingGroupService
    {

        public DbConnection _db = new DbConnection();



        public tblGrupoTreinamentos GetTrainingGroupById(int id)
        {
            tblGrupoTreinamentos TrainingGroup;

            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoPai == id
                        orderby f.IdTreinamentoFilho ascending
                        select f;

            TrainingGroup = query.FirstOrDefault();

            return TrainingGroup;
        }

        public tblGrupoTreinamentos GetSonTrainingGroupById(int id)
        {
            tblGrupoTreinamentos TrainingGroup;

            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoFilho == id
                        select f;

            TrainingGroup = query.FirstOrDefault();

            return TrainingGroup;
        }

        public IEnumerable<tblGrupoTreinamentos> GetTrainingGroups()
        {
            IEnumerable<tblGrupoTreinamentos> TrainingGroup;

            var query = from f in _db.tblGrupoTreinamentos
                        orderby f.IdTreinamentoPai ascending
                        select f;

            TrainingGroup = query.AsEnumerable();

            return TrainingGroup;
        }


        public tblGrupoTreinamentos CreateTrainingGroup(tblGrupoTreinamentos TrainingGroup)
        {
            _db.tblGrupoTreinamentos.Add(TrainingGroup);

            _db.SaveChanges();


            return TrainingGroup;
        }

        public tblGrupoTreinamentos DeleteTrainingGroup(int idDaddy, int idSon)
        {
            tblGrupoTreinamentos TrainingGroup;

            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoPai == idDaddy && f.IdTreinamentoFilho == idSon
                        orderby f.IdTreinamentoPai ascending
                        select f;

            TrainingGroup = query.FirstOrDefault();

            _db.tblGrupoTreinamentos.Remove(TrainingGroup);
            _db.SaveChanges();

            return TrainingGroup;
        }

        public void DeleteTrainingGroupByDaddy(int idDaddy)
        {
            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoPai == idDaddy
                        orderby f.IdTreinamentoPai ascending
                        select f;

            if (query.Count() > 0)
            {
                _db.tblGrupoTreinamentos.RemoveRange(query);
                _db.SaveChanges();
            }
        }

        public tblGrupoTreinamentos UpdateTrainingGroup(tblGrupoTreinamentos TrainingGroup)
        {
            var TrainingGroupToUpdate = _db.tblGrupoTreinamentos.Find(TrainingGroup.IdTreinamentoPai);
            TrainingGroupToUpdate.IdTreinamentoPai = TrainingGroup.IdTreinamentoPai;
            TrainingGroupToUpdate.IdTreinamentoFilho = TrainingGroup.IdTreinamentoFilho;



            _db.Entry(TrainingGroupToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return TrainingGroupToUpdate;
        }

        public List<tblTreinamento> setUpTrainings(int idDaddy)
        {
            List<tblTreinamento> allSons = new List<tblTreinamento>();

            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoPai == idDaddy
                        select f;

            foreach (var son in query)
            {
                var query2 = from f in _db.tblTreinamento
                             where f.IdTreinamento == son.IdTreinamentoFilho
                             select f;
                allSons.Add(query2.FirstOrDefault());
            }

            return allSons;
        }


        public bool checkIfTrainingGroupAlreadyExits(tblGrupoTreinamentos TrainingGroup)
        {
            var query = from f in _db.tblGrupoTreinamentos
                        where f.IdTreinamentoPai == TrainingGroup.IdTreinamentoPai &&
                        f.IdTreinamentoFilho == TrainingGroup.IdTreinamentoFilho
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }

        public IEnumerable<tblGrupoTreinamentos> GetTrainingGroupsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblGrupoTreinamentos> TrainingGroup;

            var query = from f in _db.tblGrupoTreinamentos
                        orderby f.IdTreinamentoPai ascending
                        select f;

            TrainingGroup = query.ToPagedList(pageNumber, quantity);

            return TrainingGroup;
        }
    }
}