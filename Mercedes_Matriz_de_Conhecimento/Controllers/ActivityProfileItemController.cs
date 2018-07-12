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
    public class ActivityProfileItemController : Controller
    {


        private ActivityProfileItemService _activityProfileItem;


        public ActivityProfileItemController()
        {
            _activityProfileItem = new ActivityProfileItemService();

        }

        // GET: activityProfile
        public ActionResult Index()
        {
            IEnumerable<tblPerfilAtivItem> activityProfile;
            activityProfile = _activityProfileItem.GetActivityProfileItems();

            return View(activityProfile);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            tblPerfilAtivItem activityProfile;
            activityProfile = _activityProfileItem.GetActivityProfileItemById(id);

            if (activityProfile == null)
                return View("Index");

            return View("Edit", activityProfile);
        }


        [HttpPost]
        public ActionResult Create(tblPerfilAtivItem activityProfile)
        {
            var exits = _activityProfileItem.checkIfActivityProfileItemAlreadyExits(activityProfile);
            activityProfile.LogarTransicao = true;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfileItem.CreateActivityProfileItem(activityProfile);

                    return RedirectToAction("Index");
                }

            }

            return View("Create", activityProfile);
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilAtivItem activityProfile, int id)
        {
            activityProfile.idPerfilAtivItem = id;
            var exits = _activityProfileItem.checkIfActivityProfileItemAlreadyExits(activityProfile);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfileItem.UpdateActivityProfileItem(activityProfile);

                    return RedirectToAction("Index");
                }

            }
            return View(activityProfile);
        }


        // GET: activityProfile/Delete/5
        public ActionResult Delete(int id)
        {

            _activityProfileItem.DeleteActivityProfileItem(id);

            return RedirectToAction("Index");

        }


    }
}
