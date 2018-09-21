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
    public class ActivityProfileController : BaseController
    {


        private ActivityProfileService _activityProfile;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public ActivityProfileController()
        {
            log.Debug("ActivityProfile Controller called");

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
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());

                var imgUser = AuthorizationHelper.GetUserImage("");

                ViewBag.User = "";
                ViewBag.UserPhoto = imgUser;
            }

            _activityProfile = new ActivityProfileService();

        }

        // GET: activityProfile
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PerfildeAtividades, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            IEnumerable<tblPerfis> activityProfile = new List<tblPerfis>();

            try
            {
                var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

                activityProfile = _activityProfile.GetActivityProfilesWithPagination(page, pages_quantity);
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());

            }

            return View(activityProfile);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PerfildeAtividades, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PerfildeAtividades, Feature = FeaturesHelper.Editar)]
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
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PerfildeAtividades, Feature = FeaturesHelper.Editar)]
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
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PerfildeAtividades, Feature = FeaturesHelper.Excluir)]
        public ActionResult Delete(int id)
        {
            _activityProfile.DeleteActivityProfile(id);

            return RedirectToAction("Index");

        }


    }
}
