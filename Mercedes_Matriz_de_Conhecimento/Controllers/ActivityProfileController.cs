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
using System.Configuration;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityProfileController : Controller
    {


        private ActivityProfileService _activityProfile;


        public ActivityProfileController()
        {
            _activityProfile = new ActivityProfileService();

        }

        // GET: activityProfile
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfis> activityProfile;
            activityProfile = _activityProfile.GetActivityProfilesWithPagination(page, pages_quantity);

            return View(activityProfile);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            tblPerfis activityProfile;
            activityProfile = _activityProfile.GetActivityProfileById(id);

            if (activityProfile == null)
                return View("Index");

            return View("Edit", activityProfile);
        }


        [HttpPost]
        public ActionResult Create(tblPerfis activityProfile)
        {
            var exits = _activityProfile.checkIfActivityProfileAlreadyExits(activityProfile);
            activityProfile.UsuarioCriacao = "Teste Sem Seg";
            activityProfile.DataCriacao = DateTime.Now;
            activityProfile.Tipo = "A";

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfile.CreateActivityProfile(activityProfile);

                    return RedirectToAction("Index");
                }

            }

            if (exits)
                ModelState.AddModelError("Nome", "Perfil de Atividade já existe");

            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfis activityProfile, int id)
        {
            activityProfile.IdPerfis = id;
            var exits = _activityProfile.checkIfActivityProfileAlreadyExits(activityProfile);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfile.UpdateActivityProfile(activityProfile);

                    return RedirectToAction("Index");
                }

            }
            return View(activityProfile);
        }


        // GET: activityProfile/Delete/5
        public ActionResult Delete(int id)
        {

            _activityProfile.DeleteActivityProfile(id);

            return RedirectToAction("Index");

        }


    }
}
