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
    public class ActivityProfileController : Controller
    {


        private ActivityProfileService _activityProfile;


        public ActivityProfileController()
        {
            _activityProfile = new ActivityProfileService();

        }

        // GET: activityProfile
        public ActionResult Index()
        {
            IEnumerable<tblPerfilAtividade> activityProfile;
            activityProfile = _activityProfile.GetActivityProfiles();

            return View(activityProfile);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            tblPerfilAtividade activityProfile;
            activityProfile = _activityProfile.GetActivityProfileById(id);

            if (activityProfile == null)
                return View("Index");

            return View("Edit", activityProfile);
        }


        [HttpPost]
        public ActionResult Create(tblPerfilAtividade activityProfile)
        {
            var exits = _activityProfile.checkIfActivityProfileAlreadyExits(activityProfile);
            activityProfile.UsuarioCriacao = "Teste Sem Seg";
            activityProfile.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfile.CreateActivityProfile(activityProfile);
                    return RedirectToAction("Index");

                }

            }

            if (exits)
                ModelState.AddModelError("Nome", "Perfil de Atividade já existente");

            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilAtividade activityProfile, int id)
        {
            activityProfile.idPerfilAtividade = id;
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
