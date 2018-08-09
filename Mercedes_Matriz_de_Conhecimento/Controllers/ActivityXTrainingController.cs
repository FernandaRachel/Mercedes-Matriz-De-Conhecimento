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
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblAtividadeXTreinamentos> activityXTraining;
            activityXTraining = _activityXTraining.GetActivityXTrainingWithPagination(page, pages_quantity);

            return View(activityXTraining);
        }

        public ActionResult Create(int idActivity = 0)
        {
            
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;

            

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();

            trainingAdded = _activityXTraining.SetUpTrainingList(idActivity);
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            teste.idAtividade = idActivity;

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;
            ViewData["TrainingsAdded"] = trainingAdded;

            return View("Create",teste);
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {
            tblAtividadeXTreinamentos ativXTrain = new tblAtividadeXTreinamentos();
            ativXTrain.idAtividade = id;
            ativXTrain.tblAtividades = _activity.GetActivityById(id);

            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingsAddedToActivity;

            trainingsAddedToActivity = _activityXTraining.SetUpTrainingList(id);
            trainings = _training.GetTrainings();
            //workzoneXAtividade = _activityXTraining.GetActivityXTrainingById(id);

            ViewData["TrainingsAdded"] = trainingsAddedToActivity;
            ViewData["Trainings"] = trainings;

           // if (workzoneXAtividade == null)
            //    return View("Index");

            return View("Edit", ativXTrain);
        }

        public ActionResult Push(int idActivity, int idTraining)
        {

            tblAtividadeXTreinamentos actXTrain = new tblAtividadeXTreinamentos();
            actXTrain.idAtividade = idActivity;
            actXTrain.idTreinamento = idTraining;

            var exits = _activityXTraining.checkIfActivityXTrainingAlreadyExits(actXTrain);

            if (!exits)
                _activityXTraining.CreateActivityXTraining(actXTrain);


            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();

            trainingAdded = _activityXTraining.SetUpTrainingList(idActivity);
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            teste.idAtividade = idActivity;

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;
            ViewData["TrainingsAdded"] = trainingAdded;

            if (exits)
                ModelState.AddModelError("idAtividade", "Atividade já associada a esse treinamento");

            return RedirectToAction("Create", new { idActivity = idActivity });
        }

        public ActionResult Pop(int idActivity, int idTraining)
        {
            _activityXTraining.DeleteActivityXTraining(idActivity, idTraining);

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();

            trainingAdded = _activityXTraining.SetUpTrainingList(idActivity);
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            teste.idAtividade = idActivity;

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;
            ViewData["TrainingsAdded"] = trainingAdded;

            return RedirectToAction("Details", new { idActivity = idActivity });
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
            IEnumerable<tblTreinamento> trainingAdded;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainings();

            trainingAdded = _activityXTraining.SetUpTrainingList(activityXTraining.idAtividade);
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            teste.idAtividade = activityXTraining.idAtividade;

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;
            ViewData["TrainingsAdded"] = trainingAdded;

            activityXTraining = _activityXTraining.GetActivityXTrainingById(id);


            return View(activityXTraining);
        }


        // GET: activityXTraining/Delete/5
        //VERIFICAR
        //public ActionResult Delete(int idActivity)
        //{
        //    _activityXTraining.DeleteActivityXTraining(idActivy);

        //    return RedirectToAction("Index");
        //}


    }
}
