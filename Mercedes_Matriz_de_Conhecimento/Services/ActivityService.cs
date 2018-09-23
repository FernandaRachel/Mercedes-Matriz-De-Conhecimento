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
    public class ActivityService : IActivityService
    {

        public DbConnection _db = new DbConnection();
        public ActivityGroupService _activityGroup = new ActivityGroupService();



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

        public IEnumerable<tblAtividades> GetActivityByName(string Nome)
        {
            IEnumerable<tblAtividades> employee;
            var query = _db.tblAtividades;
            employee = query.AsEnumerable();

            //Se o nome veio vazio traz todas Atividades
            if (Nome.Count() == 0)
                return employee;

            var query2 = _db.tblAtividades
                .Where(f => f.Nome.Contains(Nome));

            employee = query2.AsEnumerable();

            return employee;
        }

        public IEnumerable<tblAtividades> GetActivities()
        {
            IEnumerable<tblAtividades> activity;



            var query = from f in _db.tblAtividades
                        orderby f.Nome ascending
                        select f;

            activity = query.AsEnumerable();

            return activity.AsEnumerable();
        }

        public IEnumerable<tblAtividades> GetActivitiesDifferentFromFatherAndNotAGroup(int id)
        {
            IEnumerable<tblAtividades> activity;

            var query = from f in _db.tblAtividades
                        where f.idAtividade != id && f.IndicaGrupoDeAtividades == false
                        orderby f.Nome ascending
                        select f;

            activity = query.AsEnumerable();

            return activity.AsEnumerable();
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
                        orderby f.Nome ascending
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
            trainingToUpdate.IndicaGrupoDeAtividades = Activity.IndicaGrupoDeAtividades;

            if (!Activity.IndicaGrupoDeAtividades)
            {
                _activityGroup.DeleteActivityGroupByDaddy(Activity.idAtividade);
            }
            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityAlreadyExits(tblAtividades Activity)
        {
            var query = from f in _db.tblAtividades
                        where f.Nome == Activity.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idAtividade != Activity.idAtividade)
                return true;

            return false;
        }

        public IEnumerable<tblAtividades> GetActivitiesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblAtividades> activity;



            var query = from f in _db.tblAtividades
                        orderby f.Nome ascending
                        select f;

            activity = query.ToPagedList(pageNumber, quantity);

            return activity.AsEnumerable();
        }
    }
}