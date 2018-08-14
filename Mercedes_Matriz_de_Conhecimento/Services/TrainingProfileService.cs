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
    public class TrainingProfileService : ITrainingProfileService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfis GetTrainingProfileById(int id)
        {
            tblPerfis trainingProfile;

            var query = from f in _db.tblPerfis
                        where f.IdPerfis == id
                        orderby f.Nome ascending
                        select f;

            trainingProfile = query.FirstOrDefault();

            return trainingProfile;
        }
        public tblPerfis GetFirstProfile()
        {
            return _db.tblPerfis.FirstOrDefault();
        }

        public IEnumerable<tblPerfis> GetTrainingProfiles()
        {
            IEnumerable<tblPerfis> trainingProfile;



            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        select f;

            trainingProfile = query.AsEnumerable();

            return trainingProfile;
        }

        public IEnumerable<tblPerfis> GetTrainingProfilesByType()
        {
            IEnumerable<tblPerfis> activityProfile;

            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        where f.Tipo == "T"
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }

        public tblPerfis CreateTrainingProfile(tblPerfis TrainingProfile)
        {
            _db.tblPerfis.Add(TrainingProfile);

            _db.SaveChanges();


            return TrainingProfile;
        }

        public tblPerfis DeleteTrainingProfile(int id)
        {
            tblPerfis TrainingProfile;

            var query = from f in _db.tblPerfis
                        where f.IdPerfis == id
                        orderby f.Nome ascending
                        select f;

            TrainingProfile = query.FirstOrDefault();

            _db.tblPerfis.Remove(TrainingProfile);
            _db.SaveChanges();

            return TrainingProfile;
        }

        
        public tblPerfis UpdateTrainingProfile(tblPerfis TrainingProfile)
        {
            var trainingProfileToUpdate = _db.tblPerfis.Find(TrainingProfile.IdPerfis);
            trainingProfileToUpdate.Nome = TrainingProfile.Nome;
            trainingProfileToUpdate.tblPerfilAtividadeXPerfilAtItem= TrainingProfile.tblPerfilAtividadeXPerfilAtItem;
            trainingProfileToUpdate.tblPerfilAtividadeXPerfilAtItem= TrainingProfile.tblPerfilAtividadeXPerfilAtItem;
            trainingProfileToUpdate.tblPerfilTreinamentoxPerfilItem = TrainingProfile.tblPerfilTreinamentoxPerfilItem;
           


            _db.Entry(trainingProfileToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingProfileToUpdate;
        }


        public bool checkIfTrainingProfileAlreadyExits(tblPerfis TrainingProfile)
        {
            var query = from f in _db.tblPerfis
                        where f.Nome == TrainingProfile.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfis != TrainingProfile.IdPerfis)
                return true;

            return false;
        }

        public IEnumerable<tblPerfis> GetTrainingProfilesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfis> trainingProfile;



            var query = from f in _db.tblPerfis
                        where f.Tipo == "T"
                        orderby f.Nome ascending
                        select f;

            trainingProfile = query.ToPagedList(pageNumber,quantity);

            return trainingProfile;
        }

        public IEnumerable<tblPerfis> GetTrainingProfilesByType(string type)
        {
            IEnumerable<tblPerfis> trainingProfile;

            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        where f.Tipo == "T"
                        select f;

            trainingProfile = query.AsEnumerable();

            return trainingProfile;
        }
    }
}