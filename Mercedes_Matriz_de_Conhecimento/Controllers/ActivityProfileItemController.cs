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
using log4net;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityProfileItemController : BaseController
    {


        private ActivityProfileItemService _activityProfileItem;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public ActivityProfileItemController()
        {
            log.Debug("ActivityProfileItem Controller called");

            //Pega o nome do usuário para exibir na barra de navegação
            SistemaApi username = new SistemaApi();

            try
            {

                username = AuthorizationHelper.GetSystem();
                ViewBag.User = username.Usuario.ChaveAmericas;
                if (username != null)
                {
                    var imgUser = AuthorizationHelper.GetUserImage(username.Usuario.ChaveAmericas);
                    ViewBag.UserPhoto = imgUser;
                }
            }
            catch
            {
                var imgUser = AuthorizationHelper.GetUserImage("");

                ViewBag.User = "";
                ViewBag.UserPhoto = imgUser;
            }

            _activityProfileItem = new ActivityProfileItemService();
        }

        // GET: activityProfile
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            IEnumerable<tblPerfilItens> activityProfile = new List<tblPerfilItens>();

            try
            {
                var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

                activityProfile = _activityProfileItem.GetActivityProfileItemsWithPagination(page, pages_quantity);
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());
            }

            return View(activityProfile);
        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {

            tblPerfilItens activityProfile;
            activityProfile = _activityProfileItem.GetActivityProfileItemById(id);

            if (activityProfile == null)
                return View("Index");

            return View("Edit", activityProfile);
        }


        [HttpPost]
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Editar)]
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
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeAtividade, Feature = FeaturesHelper.Excluir)]
        public ActionResult Delete(int id)
        {

            try
            {
                _activityProfileItem.DeleteActivityProfileItem(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("conflicted with the REFERENCE constraint "))
                    ViewBag.Erro = "Você não pode executar essa ação, pois essa informação está em uso";
                return View("Error");
            }
        }


    }
}
