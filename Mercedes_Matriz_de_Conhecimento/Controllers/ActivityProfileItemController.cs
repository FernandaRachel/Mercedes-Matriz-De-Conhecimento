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
using Mercedes_Matriz_de_Conhecimento.Helpers;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityProfileItemController : BaseController
    {


        private ActivityProfileItemService _activityProfileItem;


        public ActivityProfileItemController()
        {
            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;

            _activityProfileItem = new ActivityProfileItemService();
        }

        // GET: activityProfile
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfilItens> activityProfile;
            activityProfile = _activityProfileItem.GetActivityProfileItemsWithPagination(page,pages_quantity);

            return View(activityProfile);
        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {

            tblPerfilItens activityProfile;
            activityProfile = _activityProfileItem.GetActivityProfileItemById(id);

            if (activityProfile == null)
                return View("Index");

            return View("Edit", activityProfile);
        }


        [HttpPost]
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Create(tblPerfilItens activityProfile)
        {
            var exits = _activityProfileItem.checkIfActivityProfileItemAlreadyExits(activityProfile);
            activityProfile.Tipo = "A";

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityProfileItem.CreateActivityProfileItem(activityProfile);

                    return RedirectToAction("Index");
                }

            }
            if (exits)
                ModelState.AddModelError("Nome", "Item de Perfil de Atividade já existe");

            return View("Create", activityProfile);
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilItens activityProfile, int id)
        {
            activityProfile.IdPerfilItem = id;
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
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Deletar)]
        public ActionResult Delete(int id)
        {

            _activityProfileItem.DeleteActivityProfileItem(id);

            return RedirectToAction("Index");

        }


    }
}
