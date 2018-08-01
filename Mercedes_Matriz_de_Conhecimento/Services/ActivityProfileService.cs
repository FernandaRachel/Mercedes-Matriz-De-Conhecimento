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
    public class ActivityProfileService : IActivityProfileService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilAtividade GetActivityProfileById(int id)
        {
            tblPerfilAtividade activityProfile;

            var query = from f in _db.tblPerfilAtividade
                        where f.idPerfilAtividade == id
                        orderby f.nome ascending
                        select f;

            activityProfile = query.FirstOrDefault();

            return activityProfile;
        }

        public IEnumerable<tblPerfilAtividade> GetActivityProfiles()
        {
            IEnumerable<tblPerfilAtividade> activityProfile;



            var query = from f in _db.tblPerfilAtividade
                        orderby f.nome ascending
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }


        public tblPerfilAtividade CreateActivityProfile(tblPerfilAtividade ActivityProfile)
        {
            _db.tblPerfilAtividade.Add(ActivityProfile);

            _db.SaveChanges();


            return ActivityProfile;
        }

        public tblPerfilAtividade DeleteActivityProfile(int id)
        {
            tblPerfilAtividade ActivityProfile;

            var query = from f in _db.tblPerfilAtividade
                        where f.idPerfilAtividade == id
                        orderby f.nome ascending
                        select f;

            ActivityProfile = query.FirstOrDefault();

            _db.tblPerfilAtividade.Remove(ActivityProfile);
            _db.SaveChanges();

            return ActivityProfile;
        }

        
        public tblPerfilAtividade UpdateActivityProfile(tblPerfilAtividade ActivityProfile)
        {
            var trainingToUpdate = _db.tblPerfilAtividade.Find(ActivityProfile.idPerfilAtividade);
            trainingToUpdate.nome = ActivityProfile.nome;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityProfileAlreadyExits(tblPerfilAtividade ActivityProfile)
        {
            var query = from f in _db.tblPerfilAtividade
                        where f.nome == ActivityProfile.nome
                        orderby f.nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idPerfilAtividade != ActivityProfile.idPerfilAtividade)
                return true;

            return false;
        }

        public IEnumerable<tblPerfilAtividade> GetActivityProfilesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfilAtividade> activityProfile;



            var query = from f in _db.tblPerfilAtividade
                        orderby f.nome ascending
                        select f;

            activityProfile = query.ToPagedList(pageNumber, quantity);

            return activityProfile;
        }
    }
}