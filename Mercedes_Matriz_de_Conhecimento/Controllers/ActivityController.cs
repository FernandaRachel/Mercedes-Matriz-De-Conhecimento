using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.Entity;
using System.Net;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityController : Controller
    {


        private ActivityService _activity;
        private ActivityProfileService _activityProfile;
        private ActivityGroupService _activityGroup;


        public ActivityController()
        {
            _activity = new ActivityService();
            _activityProfile = new ActivityProfileService();
            _activityGroup = new ActivityGroupService();
        }

        // GET: activity
        public ActionResult Index()
        {
            IEnumerable<tblAtividades> activity;
            activity = _activity.GetActivities();

            return View(activity);

        }

        public ActionResult Create()
        {
            IEnumerable<tblPerfilAtividade> activityProfile;

            activityProfile = _activityProfile.GetActivityProfiles();

            ViewData["PerfildeAtividade"] = activityProfile;

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            // Declaração de variaveis
            tblAtividades activity;
            IEnumerable<tblPerfilAtividade> activityProfile;
            IEnumerable<tblAtividades> allActivies;
            IEnumerable<tblAtividades> activitiesGroup;

            //chamadas dos métodos(no service) e assignment
            activity = _activity.GetActivityById(id);
            activityProfile = _activityProfile.GetActivityProfiles();
            activitiesGroup = _activityGroup.setUpActivitys(id);
            allActivies = _activity.GetActivities();

            ViewData["PerfildeAtividade"] = activityProfile;
            ViewData["TodasAtividades"] = allActivies;
            ViewData["AtividadesFilhos"] = activitiesGroup;

            if (activity == null)
                return View("Index");

            return View("Edit", activity);
        }

        public ActionResult Push(int idDaddy, int idSon)
        {

            tblGrupoAtividades training = new tblGrupoAtividades();
            training.idAtividadePai = idDaddy;
            training.idAtividadeFilho = idSon;
            var exits = _activityGroup.checkIfActivityGroupAlreadyExits(training);

            if (!exits)
                _activityGroup.CreateActivityGroup(training);

            return RedirectToAction("Details", new { id = idDaddy });
        }

        public ActionResult Pop(int idDaddy, int idSon)
        {
            _activityGroup.DeleteActivityGroup(idDaddy, idSon);

            return RedirectToAction("Details", new { id = idDaddy });
        }

        [HttpPost]
        public ActionResult Create(tblAtividades activity)
        {
            var exits = _activity.checkIfActivityAlreadyExits(activity);
            activity.UsuarioCriacao = "Teste Sem Seg";
            activity.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activity.CreateActivity(activity);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblPerfilAtividade> activityProfile;
            activityProfile = _activityProfile.GetActivityProfiles();
            ViewData["PerfildeAtividade"] = activityProfile;

            if (exits)
                ModelState.AddModelError("Nome", "Atividade já existe");

            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblAtividades activity, int id)
        {
            activity.idAtividade = id;
            var exits = _activity.checkIfActivityAlreadyExits(activity);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activity.UpdateActivity(activity);

                    return RedirectToAction("Index");
                }

            }
            return View(activity);
        }


        // GET: activity/Delete/5
        public ActionResult Delete(int id)
        {

            _activity.DeleteActivity(id);

            return RedirectToAction("Index");

        }


    }
}
