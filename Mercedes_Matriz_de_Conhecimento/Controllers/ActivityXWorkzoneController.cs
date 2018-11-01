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
    public class ActivityXWorkzoneController : BaseController
    {


        private WorzoneXActivityService _activityXWorkzone;
        private ActivityService _activity;
        private WorkzoneService _workzone;


        public ActivityXWorkzoneController()
        {
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

            _activityXWorkzone = new WorzoneXActivityService();
            _activity = new ActivityService();
            _workzone = new WorkzoneService();
        }

        // GET: activityXWorkzone
        [AccessHelper[AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PostodeTrabalhoXAtividade, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblWorkzone> activityXWorkzone;
            activityXWorkzone = _activityXWorkzone.GetWorzoneXActivitiesPagination(page, pages_quantity);

            return View(activityXWorkzone);
        }

        [OutputCache(Duration = 1)]
        public ActionResult SearchActivity(int idWorkzone, string nome = "")
        {
            ViewBag.Name = nome;

            IEnumerable<tblWorkzone> workzones;
            IEnumerable<tblAtividades> activiesFiltrated;
            IEnumerable<tblAtividades> activiesAdded;
            tblWorkzoneXAtividades wzXatv = new tblWorkzoneXAtividades();

            workzones = _workzone.GetWorkzones();
            activiesFiltrated = _activity.GetActivityByName(nome, idWorkzone);
            activiesAdded = _activityXWorkzone.SetUpActivitiesList(idWorkzone);
            wzXatv.idWorkzone = idWorkzone;

            ViewData["Workzones"] = workzones;
            ViewData["Activies"] = activiesFiltrated;
            ViewData["ActiviesAdded"] = activiesAdded;


            ActiviesListModel Actv = new ActiviesListModel();

            Actv.idWorkzone = idWorkzone;
            Actv.activies = activiesFiltrated;
            Actv.activiesAdded = activiesAdded;
            UpdateModel(Actv);

            return PartialView("_Lista", Actv);
        }

        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PostodeTrabalhoXAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Create(int idWorkzone = 0)
        {
            IEnumerable<tblWorkzone> workzones;
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblAtividades> activiesAdded;
            tblWorkzoneXAtividades wzXatv = new tblWorkzoneXAtividades();

            workzones = _workzone.GetWorkzones();
            activies = _activity.GetActivities();
            activiesAdded = _activityXWorkzone.SetUpActivitiesList(idWorkzone);
            wzXatv.idWorkzone = idWorkzone;

            ViewData["Workzones"] = workzones;
            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;

            return View("Create", wzXatv);
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PostodeTrabalhoXAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int idWorkzone = 0)
        {
            tblWorkzoneXAtividades workzoneXAtividade = new tblWorkzoneXAtividades();
            workzoneXAtividade.idWorkzone = idWorkzone;
            workzoneXAtividade.tblWorkzone = _workzone.GetWorkzoneById(idWorkzone);

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblWorkzone> workzones;
            IEnumerable<tblAtividades> activiesAdded;

            activies = _activity.GetActivitiesNotAdded(idWorkzone);
            activiesAdded = _activityXWorkzone.SetUpActivitiesList(idWorkzone);
            workzones = _workzone.GetWorkzones();

            ViewData["Workzones"] = workzones;
            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;
            //ViewBag.WorkzoneName = workzoneXAtividade.tblWorkzone.Nome;

            if (idWorkzone == 0)
            {
                ModelState.AddModelError("idWorkzone", "Selecione um Posto de Trabalho");

                return View("Create", workzoneXAtividade);
            }

            return View("Edit", workzoneXAtividade);
        }

        public ActionResult Push(int idWorkzone, int idActivity, int ordem = 0)
        {
            tblWorkzoneXAtividades workzoneXAtividade = new tblWorkzoneXAtividades();
            workzoneXAtividade.idWorkzone = idWorkzone;
            workzoneXAtividade.idAtividade = idActivity;
            workzoneXAtividade.Ordem = ordem;
            ViewBag.WorkzoneName = _workzone.GetWorkzoneById(idWorkzone).Nome;

            var exits = _activityXWorkzone.checkIfWorzoneXActivityAlreadyExits(workzoneXAtividade);
            var ordemExists = _activityXWorkzone.checkIfOrderAlreadyExits(workzoneXAtividade);

            if (ModelState.IsValid && ordem != 0 && ordem.ToString().Length <= 4)
            {
                if (!exits && !ordemExists)
                {
                    _activityXWorkzone.CreateWorzoneXActivity(workzoneXAtividade);

                    return RedirectToAction("Details", new { idWorkzone = idWorkzone });
                }

            }

            /*popula as listas*/
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblAtividades> activiesAdded;

            activies = _activity.GetActivities();
            activiesAdded = _activityXWorkzone.SetUpActivitiesList(idWorkzone);
            workzoneXAtividade.tblWorkzone = _workzone.GetWorkzoneById(idWorkzone);

            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;

            /*GERANDO MENSAGENS DE VALIDAÇÃO*/
            if (exits)
                ModelState.AddModelError("idWorkzone", "Posto de Trabalho já associada a essa atividade");
            if (ordemExists)
                ModelState.AddModelError("Ordem", "Ordem já existente");
            if (ordem == 0)
                ModelState.AddModelError("Ordem", "Ordem deve ser preenchida");
            if (ordem.ToString().Length > 4)
                ModelState.AddModelError("Ordem", "Ordem deve ter no máximo 4 digitos");

            return View("Edit", workzoneXAtividade);
        }

        public ActionResult Pop(int idWorkzone, int idActivity)
        {
            _activityXWorkzone.DeleteWorzoneXActivity(idWorkzone, idActivity);


            tblWorkzoneXAtividades workzoneXAtividade = new tblWorkzoneXAtividades();
            workzoneXAtividade.idWorkzone = idWorkzone;
            workzoneXAtividade.tblAtividades = _activity.GetActivityById(idWorkzone);

            /*popula as listas*/
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblAtividades> activiesAdded;

            activies = _activity.GetActivities();
            activiesAdded = _activityXWorkzone.SetUpActivitiesList(idWorkzone);

            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;


            return RedirectToAction("Details", new { idWorkzone = idWorkzone });
        }
        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblWorkzoneXAtividades activityXWorkzone, int id)
        {
            // Adiciona o ID ao objeto, pois o obejto não esta retornando o ID
            activityXWorkzone.idWorkzoneAtividade = id;

            // Valida se nº da ordem ja existe 
            // e se determinada atividade ja esta associada a determinada workzone
            var exits = _activityXWorkzone.checkIfWorzoneXActivityAlreadyExits(activityXWorkzone);
            var orderExits = _activityXWorkzone.checkIfOrderAlreadyExits(activityXWorkzone);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityXWorkzone.UpdateWorzoneXActivity(activityXWorkzone);

                    return RedirectToAction("Index");
                }

            }
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblWorkzone> workzones;
            tblWorkzoneXAtividades workzoneXAtividade;

            activies = _activity.GetActivities();
            workzones = _workzone.GetWorkzones();
            workzoneXAtividade = _activityXWorkzone.GetWorzoneXActivityById(id);

            ViewData["Activies"] = activies;
            ViewData["Workzones"] = workzones;

            return View(activityXWorkzone);
        }




    }
}
