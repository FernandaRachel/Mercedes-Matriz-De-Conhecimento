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


        public ActivityController()
        {
            _activity = new ActivityService();
            _activityProfile = new ActivityProfileService();

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

            tblAtividades activity;
            activity = _activity.GetActivityById(id);

            IEnumerable<tblPerfilAtividade> activityProfile;

            activityProfile = _activityProfile.GetActivityProfiles();

            ViewData["PerfildeAtividade"] = activityProfile;

            if (activity == null)
                return View("Index");

            return View("Edit", activity);
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
