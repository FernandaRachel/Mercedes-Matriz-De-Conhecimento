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
using System.Threading;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityXTrainingController : BaseController
    {


        private ActivityXTrainingService _activityXTraining;
        private ActivityService _activity;
        private TrainingService _training;


        public ActivityXTrainingController()
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

            _activityXTraining = new ActivityXTrainingService();
            _activity = new ActivityService();
            _training = new TrainingService();
        }

        // GET: activityXTraining
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.TreinamentoXAtividade, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblAtividades> activity;
            activity = _activityXTraining.GetActivityXTrainingWithPagination(page, pages_quantity);

            return View(activity);
        }

        [OutputCache(Duration = 1)]
        public ActionResult SearchTraining(int idActivity, string nome = "")
        {
            ViewBag.Name = nome;

            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            TrainingsListModel Training = new TrainingsListModel();

            teste.idAtividade = idActivity;
            activies = _activity.GetActivities();
            trainings = _training.GetTrainingByName(nome, idActivity);
            trainingAdded = _activityXTraining.SetUpTrainingList(idActivity);

            ViewData["Activies"] = activies;

            Training.IdAtividade = idActivity;
            Training.trainings = trainings;
            Training.trainingsAdded = trainingAdded;
            UpdateModel(Training);

            return PartialView("_Lista", Training);
        }

        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.TreinamentoXAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Create(int idActivity = 0)
        {

            IEnumerable<tblAtividades> activies;
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();

            teste.idAtividade = idActivity;
            activies = _activity.GetActivities();

            ViewData["Activies"] = activies;

            return View("Create", teste);
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.TreinamentoXAtividade, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int idActivity = 0)
        {
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingsAddedToActivity;
            IEnumerable<tblAtividades> activies;
            tblAtividadeXTreinamentos ativXTrain = new tblAtividadeXTreinamentos();
            TrainingsListModel Training = new TrainingsListModel();

            ativXTrain.idAtividade = idActivity;
            ativXTrain.tblAtividades = _activity.GetActivityById(idActivity);
            trainingsAddedToActivity = _activityXTraining.SetUpTrainingList(idActivity);
            trainings = _training.GetTrainingsNotAddedInActivity(idActivity);
            activies = _activity.GetActivities();

            Training.IdAtividade = idActivity;
            Training.trainings = trainings;
            Training.trainingsAdded = trainingsAddedToActivity;
            ViewData["Activies"] = activies;
            UpdateModel(Training);

            if (idActivity == 0)
            {
                tblAtividadeXTreinamentos AtivXTrein = new tblAtividadeXTreinamentos();

                AtivXTrein.idAtividade = idActivity;

                ModelState.AddModelError("idAtividade", "Selecione uma Atividade");

                return View("Create", AtivXTrein);
            }

            return View("Edit", ativXTrain);
        }

        public ActionResult Push(int idActivity, int idTraining)
        {
            tblAtividadeXTreinamentos actXTrain = new tblAtividadeXTreinamentos();
            actXTrain.idAtividade = idActivity;
            actXTrain.idTreinamento = idTraining;

            var exits = _activityXTraining.checkIfActivityXTrainingAlreadyExits(actXTrain);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activityXTraining.CreateActivityXTraining(actXTrain);
                    Thread.Sleep(250);

                    return RedirectToAction("Details", new { idActivity = idActivity });
                }
            }

            /*popula as listas*/
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainingsNotAddedInActivity(idActivity);

            trainingAdded = _activityXTraining.SetUpTrainingList(idActivity);
            tblAtividadeXTreinamentos teste = new tblAtividadeXTreinamentos();
            teste.idAtividade = idActivity;

            ViewData["Activies"] = activies;
            ViewData["Trainings"] = trainings;
            ViewData["TrainingsAdded"] = trainingAdded;
            actXTrain.tblAtividades = _activity.GetActivityById(idActivity);

            /*GERANDO MENSAGENS DE VALIDAÇÃO*/
            if (exits)
                ModelState.AddModelError("idAtividade", "Atividade já esta associada a esse treinamento");

            return View("Edit", actXTrain);
        }

        public ActionResult Pop(int idActivity, int idTraining)
        {
            _activityXTraining.DeleteActivityXTraining(idActivity, idTraining);

            Thread.Sleep(250);
            IEnumerable<tblAtividades> activies;
            IEnumerable<tblTreinamento> trainings;
            IEnumerable<tblTreinamento> trainingAdded;

            activies = _activity.GetActivities();
            trainings = _training.GetTrainingsNotAddedInActivity(idActivity);

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
