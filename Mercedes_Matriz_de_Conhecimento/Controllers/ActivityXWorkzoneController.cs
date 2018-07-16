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
        public ActionResult Index()
        {
            IEnumerable<tblWorkzoneXAtividades> activityXWorkzone;
            activityXWorkzone = _activityXWorkzone.GetWorzoneXActivities();

            return View(activityXWorkzone);
        }

        public ActionResult Create()
        {
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblWorkzone> workzones;

            activies = _activity.GetActivities();
            workzones = _workzone.GetWorkzones();

            ViewData["Activies"] = activies;
            ViewData["Workzones"] = workzones;

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblWorkzone> workzones;
            tblWorkzoneXAtividades workzoneXAtividade;

            activies = _activity.GetActivities();
            workzones = _workzone.GetWorkzones();
            workzoneXAtividade = _activityXWorkzone.GetWorzoneXActivityById(id);

            ViewData["Activies"] = activies;
            ViewData["Workzones"] = workzones;

            if (workzoneXAtividade == null)
                return View("Index");

            return View("Edit", workzoneXAtividade);
        }


        [HttpPost]
        public ActionResult Create(tblWorkzoneXAtividades activityXWorkzone)
        {
            var exits = _activityXWorkzone.checkIfWorzoneXActivityAlreadyExits(activityXWorkzone);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityXWorkzone.CreateWorzoneXActivity(activityXWorkzone);
                    return RedirectToAction("Index");

                }

            }
            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblWorkzoneXAtividades activityXWorkzone, int id)
        {
            activityXWorkzone.idAtividade = id;
            var exits = _activityXWorkzone.checkIfWorzoneXActivityAlreadyExits(activityXWorkzone);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityXWorkzone.UpdateWorzoneXActivity(activityXWorkzone);

                    return RedirectToAction("Index");
                }

            }
            return View(activityXWorkzone);
        }


        // GET: activityXWorkzone/Delete/5
        public ActionResult Delete(int id)
        {
            _activityXWorkzone.DeleteWorzoneXActivity(id);

            return RedirectToAction("Index");
        }


    }
}
