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
    public class TrainingTypeController : Controller
    {



        private TrainingTypeService _trainingType;
        private TrainingProfileService _trainingProfile;

        public TrainingTypeController()
        {
            _trainingType = new TrainingTypeService();
            _trainingProfile = new TrainingProfileService();

        }

        // GET: training
        public ActionResult Index()
        {
            IEnumerable<tblTipoTreinamento> training;
            training = _trainingType.GetTrainingTypes();

            return View(training);

        }

        public ActionResult Create()
        {
            IEnumerable<tblPerfilTreinamento> trainingProile;
            trainingProile = _trainingProfile.GetTrainingProfiles();


            ViewData["PerfilTreinamento"] = trainingProile;
            ViewBag.SelectedProfile = _trainingProfile.GetFirstProfile();

            return View("Create");
        }

        //GET: training/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<tblPerfilTreinamento> trainingProile;
            trainingProile = _trainingProfile.GetTrainingProfiles();


            ViewData["PerfilTreinamento"] = trainingProile;

            tblTipoTreinamento training;
            training = _trainingType.GetTrainingTypeById(id);

            if (training == null)
                return View("Index");

            return View("Edit", training);
        }


    
        // GET: training/Create
        [HttpPost]
        public ActionResult Create(tblTipoTreinamento training)
        {
            var exits = _trainingType.checkIfTrainingTypeAlreadyExits(training);
            training.UsuarioCriacao = "Teste Sem Seg";
            training.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingType.CreateTrainingType(training);
                    return RedirectToAction("Index");

                }

            }
            return View(training);
        }


        // GET: training/Edit/5
        [HttpPost]
        public ActionResult Edit(tblTipoTreinamento training, int id)
        {
            training.IdTipoTreinamento = id;
            var exits = _trainingType.checkIfTrainingTypeAlreadyExits(training);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingType.UpdateTrainingType(training);

                    return RedirectToAction("Index");
                }

            }
            return View(training);
        }


        // GET: training/Delete/5
        public ActionResult Delete(int id)
        {

            _trainingType.DeleteTrainingType(id);

            return RedirectToAction("Index");

        }


    }
}
