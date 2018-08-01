using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class TrainingProfileService : ITrainingProfileService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilTreinamento GetTrainingProfileById(int id)
        {
            tblPerfilTreinamento trainingProfile;

            var query = from f in _db.tblPerfilTreinamento
                        where f.IdPerfilTreinamento == id
                        orderby f.Nome ascending
                        select f;

            trainingProfile = query.FirstOrDefault();

            return trainingProfile;
        }
        public tblPerfilTreinamento GetFirstProfile()
        {
            return _db.tblPerfilTreinamento.FirstOrDefault();
        }

        public IEnumerable<tblPerfilTreinamento> GetTrainingProfiles()
        {
            IEnumerable<tblPerfilTreinamento> trainingProfile;



            var query = from f in _db.tblPerfilTreinamento
                        orderby f.Nome ascending
                        select f;

            trainingProfile = query.AsEnumerable();

            return trainingProfile;
        }


        public tblPerfilTreinamento CreateTrainingProfile(tblPerfilTreinamento TrainingProfile)
        {
            _db.tblPerfilTreinamento.Add(TrainingProfile);

            _db.SaveChanges();


            return TrainingProfile;
        }

        public tblPerfilTreinamento DeleteTrainingProfile(int id)
        {
            tblPerfilTreinamento TrainingProfile;

            var query = from f in _db.tblPerfilTreinamento
                        where f.IdPerfilTreinamento == id
                        orderby f.Nome ascending
                        select f;

            TrainingProfile = query.FirstOrDefault();

            _db.tblPerfilTreinamento.Remove(TrainingProfile);
            _db.SaveChanges();

            return TrainingProfile;
        }

        
        public tblPerfilTreinamento UpdateTrainingProfile(tblPerfilTreinamento TrainingProfile)
        {
            var trainingProfileToUpdate = _db.tblPerfilTreinamento.Find(TrainingProfile.IdPerfilTreinamento);
            trainingProfileToUpdate.Nome = TrainingProfile.Nome;
            trainingProfileToUpdate.tblTipoTreinamento= TrainingProfile.tblTipoTreinamento;
            trainingProfileToUpdate.tblPerfilTreinamentoxPerfilItem = TrainingProfile.tblPerfilTreinamentoxPerfilItem;
           


            _db.Entry(trainingProfileToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingProfileToUpdate;
        }


        public bool checkIfTrainingProfileAlreadyExits(tblPerfilTreinamento TrainingProfile)
        {
            var query = from f in _db.tblPerfilTreinamento
                        where f.Nome == TrainingProfile.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfilTreinamento != TrainingProfile.IdPerfilTreinamento)
                return true;

            return false;
        }
    }
}