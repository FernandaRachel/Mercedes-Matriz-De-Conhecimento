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
    public class ActivityXTrainingController : Controller
    {


        private ActivityXTrainingService _activityXTraining;
        private ActivityService _activity;
        private TrainingService _training;


        public ActivityXTrainingController()
        {
            _activityXTraining = new ActivityXTrainingService();
            _activity = new ActivityService();
            _training = new TrainingService();
        }

        // GET: activityXTraining
        public ActionResult Index()
        {
            IEnumerable<tblAtividadeXTreinamentos> activityXTraining;
            activityXTraining = _activityXTraining.GetActivityXWorkzone();

            return View(activityXTraining);
        }

        public ActionResult Create()
        {
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            tblAtividadeXTreinamentos workzoneXAtividade;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();
            workzoneXAtividade = _activityXTraining.GetActivityXTrainingById(id);

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;

            if (workzoneXAtividade == null)
                return View("Index");

            return View("Edit", workzoneXAtividade);
        }


        [HttpPost]
        public ActionResult Create(tblAtividadeXTreinamentos activityXTraining)
        {
            // Valida se nº da ordem ja existe 
            // e se determinada atividade ja esta associada a determinada workzone
            var exits = _activityXTraining.checkIfActivityXTrainingAlreadyExits(activityXTraining);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityXTraining.CreateActivityXTraining(activityXTraining);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();
            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;


            if (exits) 
                ModelState.AddModelError("idAtividade", "Treinamento já associado a atividade");

            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblAtividadeXTreinamentos activityXTraining, int id)
        {
            // Adiciona o ID ao objeto, pois o obejto não esta retornando o ID
            activityXTraining.idAtivTreinamento = id;

            // Valida se nº da ordem ja existe 
            // e se determinada atividade ja esta associada a determinada workzone

            if (ModelState.IsValid)
            {
                _activityXTraining.UpdateActivityXTraining(activityXTraining);

                return RedirectToAction("Index");
            }

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            tblAtividadeXTreinamentos workzoneXAtividade;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();
            workzoneXAtividade = _activityXTraining.GetActivityXTrainingById(id);

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;

            return View(activityXTraining);
        }


        // GET: activityXTraining/Delete/5
        public ActionResult Delete(int id)
        {
            _activityXTraining.DeleteActivityXTraining(id);

            return RedirectToAction("Index");
        }


    }
}
