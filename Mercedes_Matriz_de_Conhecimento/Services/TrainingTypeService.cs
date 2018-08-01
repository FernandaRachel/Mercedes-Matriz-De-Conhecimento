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
    public class TrainingTypeService : ITrainingTypeService
    {

        public DbConnection _db = new DbConnection();



        public tblTipoTreinamento GetTrainingTypeById(int id)
        {
            tblTipoTreinamento TrainingType;

            var query = from f in _db.tblTipoTreinamento
                        where f.IdTipoTreinamento == id
                        orderby f.Nome ascending
                        select f;

            TrainingType = query.FirstOrDefault();

            return TrainingType;
        }

        public IEnumerable<tblTipoTreinamento> GetTrainingTypes()
        {
            IEnumerable<tblTipoTreinamento> TrainingType;



            var query = from f in _db.tblTipoTreinamento
                        orderby f.Nome ascending
                        select f;

            TrainingType = query.AsEnumerable();

            return TrainingType;
        }


        public tblTipoTreinamento CreateTrainingType(tblTipoTreinamento TrainingType)
        {
            _db.tblTipoTreinamento.Add(TrainingType);

            _db.SaveChanges();


            return TrainingType;
        }

        public tblTipoTreinamento DeleteTrainingType(int id)
        {
            tblTipoTreinamento TrainingType;

            var query = from f in _db.tblTipoTreinamento
                        where f.IdTipoTreinamento == id
                        orderby f.Nome ascending
                        select f;

            TrainingType = query.FirstOrDefault();

            _db.tblTipoTreinamento.Remove(TrainingType);
            _db.SaveChanges();

            return TrainingType;
        }

        
        public tblTipoTreinamento UpdateTrainingType(tblTipoTreinamento TrainingType)
        {
            var TrainingTypeToUpdate = _db.tblTipoTreinamento.Find(TrainingType.IdTipoTreinamento);
            TrainingTypeToUpdate.Nome = TrainingType.Nome;
            TrainingTypeToUpdate.Sigla = TrainingType.Sigla;
            TrainingTypeToUpdate.Descricao = TrainingType.Descricao;
            TrainingTypeToUpdate.TipoAtivo = TrainingType.TipoAtivo;
            TrainingTypeToUpdate.UsuarioDesativacao = TrainingType.UsuarioDesativacao;
            TrainingTypeToUpdate.DataDesativacao = TrainingType.DataDesativacao;


            _db.Entry(TrainingTypeToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return TrainingTypeToUpdate;
        }


        public bool checkIfTrainingTypeAlreadyExits(tblTipoTreinamento TrainingType)
        {
            var query = from f in _db.tblTipoTreinamento
                        where f.Nome == TrainingType.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdTipoTreinamento != TrainingType.IdTipoTreinamento)
                return true;

            return false;
        }

        public IEnumerable<tblTipoTreinamento> GetTrainingTypesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblTipoTreinamento> TrainingType;



            var query = from f in _db.tblTipoTreinamento
                        orderby f.Nome ascending
                        select f;

            TrainingType = query.ToPagedList(pageNumber,quantity);

            return TrainingType;
        }
    }
}