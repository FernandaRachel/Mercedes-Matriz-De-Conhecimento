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
    public class PerfilAtivItemXPerfilItemController : Controller
    {


        private PerfilAtivItemXPerfilItemService _perfilAtivItemXPerfilItem;
        private ActivityProfileItemService _activityProfileItem;
        private ActivityProfileService _profileActivity;


        public PerfilAtivItemXPerfilItemController()
        {
            _perfilAtivItemXPerfilItem = new PerfilAtivItemXPerfilItemService();
            _activityProfileItem = new ActivityProfileItemService();
            _profileActivity = new ActivityProfileService();
        }

        // GET: perfilAtivItemXPerfilItem
        public ActionResult Index()
        {
            IEnumerable<tblPerfilAtividadeXPerfilAtItem> perfilAtivItemXPerfilItem;
            perfilAtivItemXPerfilItem = _perfilAtivItemXPerfilItem.GetPerfilAtivItemXPerfilItems();

            return View(perfilAtivItemXPerfilItem);

        }

        public ActionResult Create()
        {
            IEnumerable<tblPerfilAtividade> activityProfile;
            IEnumerable<tblPerfilAtivItem> profileItemActivity;

            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _activityProfileItem.GetActivityProfileItems();

            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int idPAI, int idPA)
        {
            // Declaração de variaveis
            IEnumerable<tblPerfilAtividade> activityProfile;
            IEnumerable<tblPerfilAtivItem> profileItemActivity;
            tblPerfilAtividadeXPerfilAtItem perfilAtivItemXPerfilItem;

            //chamadas dos métodos(no service) e assignment
            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _activityProfileItem.GetActivityProfileItems();
            perfilAtivItemXPerfilItem = _perfilAtivItemXPerfilItem.GetPerfilAtivItemXPerfilItemById(idPAI, idPA);

            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;


            if (perfilAtivItemXPerfilItem == null)
                return View("Index");

            return View("Edit", perfilAtivItemXPerfilItem);
        }

        [HttpPost]
        public ActionResult Create(tblPerfilAtividadeXPerfilAtItem perfilAtivItemXPerfilItem)
        {
            var exits = _perfilAtivItemXPerfilItem.checkIfPerfilAtivItemXPerfilItemAlreadyExits(perfilAtivItemXPerfilItem);
            var orderExits = _perfilAtivItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);


            if (ModelState.IsValid)
            {
                if (!exits && !orderExits)
                {
                    _perfilAtivItemXPerfilItem.CreatePerfilAtivItemXPerfilItem(perfilAtivItemXPerfilItem);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblPerfilAtividade> activityProfile;
            IEnumerable<tblPerfilAtivItem> profileItemActivity;
            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _activityProfileItem.GetActivityProfileItems();
            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;

            if (orderExits)
                ModelState.AddModelError("Ordem", "Ordem já existe");
            else if(exits)
                ModelState.AddModelError("idPerfilAtivItem", " Atividade Item já associada a Perfil Atividade");


            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilAtividadeXPerfilAtItem perfilAtivItemXPerfilItem)
        {
            // Verifica se ordem já existe
            var exits = _perfilAtivItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _perfilAtivItemXPerfilItem.UpdatePerfilAtivItemXPerfilItem(perfilAtivItemXPerfilItem);

                    return RedirectToAction("Index");
                }

                if (exits)
                    ModelState.AddModelError("Ordem", "Ordem já existe");

                return View("Edit");
            }

            IEnumerable<tblPerfilAtividade> activityProfile;
            IEnumerable<tblPerfilAtivItem> profileItemActivity;

            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _activityProfileItem.GetActivityProfileItems();

            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;
            return View(perfilAtivItemXPerfilItem);
        }


        // GET: perfilAtivItemXPerfilItem/Delete/5
        public ActionResult Delete(int idPAI, int idPA)
        {

            _perfilAtivItemXPerfilItem.DeletePerfilAtivItemXPerfilItem(idPAI, idPA);

            return RedirectToAction("Index");

        }


    }
}
