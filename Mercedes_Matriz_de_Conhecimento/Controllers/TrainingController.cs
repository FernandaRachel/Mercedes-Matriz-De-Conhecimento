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
    public class TrainingController : Controller
    {


        private TrainingService _training;
        private TrainingTypeService _trainingType;
        private TrainingProfileService _trainingProfile;
        private TrainingGroupService _trainingGroup;


        public TrainingController()
        {
            _training = new TrainingService();
            _trainingType = new TrainingTypeService();
            _trainingProfile = new TrainingProfileService();
            _trainingGroup = new TrainingGroupService();

        }

        // GET: training
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblTreinamento> training;
            training = _training.GetTrainingsWithPagination(page, pages_quantity);

            return View(training);

        }

        public ActionResult Create()
        {
            IEnumerable<tblTipoTreinamento> trainingType;
            IEnumerable<tblPerfis> trainingProfile;

            trainingType = _trainingType.GetTrainingTypes();
            trainingProfile = _trainingProfile.GetTrainingProfilesByType("T");

            ViewData["TipoTreinamento"] = trainingType;
            ViewData["PerfilTreinamento"] = trainingProfile;

            return View("Create");
        }

        //GET: training/Details/5
        public ActionResult Details(int id)
        {
            // Declaração de variaveis
            IEnumerable<tblTipoTreinamento> trainingType;
            tblTreinamento training;
            IEnumerable<tblTreinamento> allTrainings;
            IEnumerable<tblTreinamento> trainingGroup;

            //chamadas dos métodos(no service) e assignment
            trainingType = _trainingType.GetTrainingTypes();
            trainingGroup = _trainingGroup.setUpTrainings(id);
            allTrainings = _training.GetTrainings();
            training = _training.GetTrainingById(id);

            ViewData["TipoTreinamento"] = trainingType;
            ViewData["TreinamentosFilhos"] = trainingGroup;
            ViewData["TodosTreinamentos"] = allTrainings;


            if (training == null)
                return View("Index");

            return View("Edit", training);
        }

        public ActionResult checkIfGroupTagIsTrue(tblTreinamento training)
        {
            if (training.IndicaGrupoDeTreinamentos)
                return PartialView("_TrainingGroup");

            return PartialView("_TrainingGroup");
        }

        public ActionResult Push(int idDaddy, int idSon)
        {

            tblGrupoTreinamentos training = new tblGrupoTreinamentos();
            training.IdTreinamentoPai = idDaddy;
            training.IdTreinamentoFilho = idSon;
            var exits = _trainingGroup.checkIfTrainingGroupAlreadyExits(training);

            if (!exits)
                _trainingGroup.CreateTrainingGroup(training);

            return RedirectToAction("Details", new { id = idDaddy });
        }

        public ActionResult Pop(int idDaddy, int idSon)
        {
            _trainingGroup.DeleteTrainingGroup(idDaddy, idSon);

            return RedirectToAction("Details", new { id = idDaddy });
        }


        // GET: training/Create
        [HttpPost]
        public ActionResult Create(tblTreinamento training)
        {
            var exits = _training.checkIfTrainingAlreadyExits(training);
            training.UsuarioCriacao = "Teste Sem Seg";
            training.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _training.CreateTraining(training);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblTipoTreinamento> trainingType;
            trainingType = _trainingType.GetTrainingTypes();
            ViewData["TipoTreinamento"] = trainingType;

            if (exits)
                ModelState.AddModelError("Nome", "Treinamento já existe");

            return View("Create");
        }


        // GET: training/Edit/5
        [HttpPost]
        public ActionResult Edit(tblTreinamento training, int id)
        {
            training.IdTreinamento = id;
            var exits = _training.checkIfTrainingAlreadyExits(training);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _training.UpdateTraining(training);

                    return RedirectToAction("Index");
                }

            }
            return View(training);
        }


        // GET: training/Delete/5
        public ActionResult Delete(int id)
        {

            _training.DeleteTraining(id);

            return RedirectToAction("Index");

        }


    }
}
