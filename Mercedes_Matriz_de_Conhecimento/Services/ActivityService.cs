using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class ActivityService : IActivityService
    {

        public DbConnection _db = new DbConnection();



        public tblAtividades GetActivityById(int id)
        {
            tblAtividades activity;

            var query = from f in _db.tblAtividades
                        where f.idAtividade == id
                        orderby f.Nome
                        select f;

            activity = query.FirstOrDefault();

            return activity;
        }

        public IEnumerable<tblAtividades> GetActivities()
        {
            IEnumerable<tblAtividades> activity;



            var query = from f in _db.tblAtividades
                        orderby f.Nome
                        select f;

            activity = query.AsEnumerable();

            return activity;
        }


        public tblAtividades CreateActivity(tblAtividades Activity)
        {
            _db.tblAtividades.Add(Activity);

            _db.SaveChanges();


            return Activity;
        }

        public tblAtividades DeleteActivity(int id)
        {
            tblAtividades Activity;

            var query = from f in _db.tblAtividades
                        where f.idAtividade == id
                        orderby f.Nome
                        select f;

            Activity = query.FirstOrDefault();

            _db.tblAtividades.Remove(Activity);
            _db.SaveChanges();

            return Activity;
        }

        
        public tblAtividades UpdateActivity(tblAtividades Activity)
        {
            var trainingToUpdate = _db.tblAtividades.Find(Activity.idAtividade);
            trainingToUpdate.Nome = Activity.Nome;
            trainingToUpdate.Sigla = Activity.Sigla;
            trainingToUpdate.Descricao = Activity.Descricao;
            trainingToUpdate.idPerfilAtividade = Activity.idPerfilAtividade;
            trainingToUpdate.idTipoEquipamentoGSA = Activity.idTipoEquipamentoGSA;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityAlreadyExits(tblAtividades Activity)
        {
            var query = from f in _db.tblAtividades
                        where f.Nome == Activity.Nome
                        orderby f.Nome
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idAtividade != Activity.idAtividade)
                return true;

            return false;
        }
    }
}