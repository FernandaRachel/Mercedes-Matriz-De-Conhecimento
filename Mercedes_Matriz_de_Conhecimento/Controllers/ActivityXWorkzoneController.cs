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
    public class ActivityXWorkzoneController : Controller
    {


        private WorzoneXActivityService _activityXWorkzone;
        private ActivityService _activity;
        private WorkzoneService _workzone;


        public ActivityXWorkzoneController()
        {
            _activityXWorkzone = new WorzoneXActivityService();
            _activity = new ActivityService();
            _workzone = new WorkzoneService();
        }

        // GET: activityXWorkzone
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
            activiesFiltrated = _activity.GetActivityByName(nome);
            activiesAdded = _activityXWorkzone.SetUpWorkzoneList(idWorkzone);
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

        public ActionResult Create(int idWorkzone = 0)
        {
            IEnumerable<tblWorkzone> workzones;
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblAtividades> activiesAdded;
            tblWorkzoneXAtividades wzXatv = new tblWorkzoneXAtividades();

            workzones = _workzone.GetWorkzones();
            activies = _activity.GetActivities();
            activiesAdded = _activityXWorkzone.SetUpWorkzoneList(idWorkzone);
            wzXatv.idWorkzone = idWorkzone;

            ViewData["Workzones"] = workzones;
            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;

            return View("Create", wzXatv);
        }

        //GET: Activity/Details/5
        public ActionResult Details(int idWorkzone = 0)
        {
            tblWorkzoneXAtividades workzoneXAtividade = new tblWorkzoneXAtividades();
            workzoneXAtividade.idWorkzone = idWorkzone;
            workzoneXAtividade.tblWorkzone = _workzone.GetWorkzoneById(idWorkzone);

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblWorkzone> workzones;
            IEnumerable<tblAtividades> activiesAdded;

            activies = _activity.GetActivities();
            activiesAdded = _activityXWorkzone.SetUpWorkzoneList(idWorkzone);
            workzones = _workzone.GetWorkzones();

            ViewData["Workzones"] = workzones;
            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;
            //ViewBag.WorkzoneName = workzoneXAtividade.tblWorkzone.Nome;

            if (idWorkzone == 0)
            {
                ModelState.AddModelError("idWorkzone", "Selecione uma Workzone");

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

            if (ModelState.IsValid && ordem != 0)
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
            activiesAdded = _activityXWorkzone.SetUpWorkzoneList(idWorkzone);

            ViewData["Activies"] = activies;
            ViewData["ActiviesAdded"] = activiesAdded;

            /*GERANDO MENSAGENS DE VALIDAÇÃO*/
            if (exits)
                ModelState.AddModelError("idWorkzone", "Workzone já associada a essa atividade");
            if (ordemExists)
                ModelState.AddModelError("Ordem", "Ordem já existente");
            if (ordem == 0)
                ModelState.AddModelError("Ordem", "Ordem deve ser preenchida");

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
            activiesAdded = _activityXWorkzone.SetUpWorkzoneList(idWorkzone);

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
