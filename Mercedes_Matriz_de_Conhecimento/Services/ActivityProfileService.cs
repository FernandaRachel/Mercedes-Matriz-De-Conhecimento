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



        public tblPerfis GetActivityProfileById(int id)
        {
            tblPerfis activityProfile;

            var query = from f in _db.tblPerfis
                        where f.IdPerfis == id
                        orderby f.Nome ascending
                        select f;

            activityProfile = query.FirstOrDefault();

            return activityProfile;
        }

        public IEnumerable<tblPerfis> GetActivityProfiles()
        {
            IEnumerable<tblPerfis> activityProfile;



            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }


        public tblPerfis CreateActivityProfile(tblPerfis ActivityProfile)
        {
            _db.tblPerfis.Add(ActivityProfile);

            _db.SaveChanges();


            return ActivityProfile;
        }

        public tblPerfis DeleteActivityProfile(int id)
        {
            tblPerfis ActivityProfile;

            var query = from f in _db.tblPerfis
                        where f.IdPerfis == id
                        orderby f.Nome ascending
                        select f;

            ActivityProfile = query.FirstOrDefault();

            _db.tblPerfis.Remove(ActivityProfile);
            _db.SaveChanges();

            return ActivityProfile;
        }

        
        public tblPerfis UpdateActivityProfile(tblPerfis ActivityProfile)
        {
            var trainingToUpdate = _db.tblPerfis.Find(ActivityProfile.IdPerfis);
            trainingToUpdate.Nome = ActivityProfile.Nome;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityProfileAlreadyExits(tblPerfis ActivityProfile)
        {
            var query = from f in _db.tblPerfis
                        where f.Nome == ActivityProfile.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfis != ActivityProfile.IdPerfis)
                return true;

            return false;
        }

        public IEnumerable<tblPerfis> GetActivityProfilesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfis> activityProfile;



            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        where f.Tipo == "A"
                        select f;

            activityProfile = query.ToPagedList(pageNumber, quantity);

            return activityProfile;
        }

        public IEnumerable<tblPerfis> GetActivityProfilesByType()
        {
            IEnumerable<tblPerfis> activityProfile;



            var query = from f in _db.tblPerfis
                        orderby f.Nome ascending
                        where f.Tipo == "A"
                        select f;

            activityProfile = query.AsEnumerable();

            return activityProfile;
        }
    }
}