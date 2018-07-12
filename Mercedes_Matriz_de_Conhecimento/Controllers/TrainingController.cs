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
    public class TrainingController : Controller
    {


        private TrainingService _training;

        private TrainingTypeService _trainingType;

        public TrainingController()
        {
            _training = new TrainingService();
            _trainingType = new TrainingTypeService();

        }

        // GET: training
        public ActionResult Index()
        {
            IEnumerable<tblTreinamento> training;
            training = _training.GetTrainings();

            return View(training);

        }

        public ActionResult Create()
        {
            IEnumerable<tblTipoTreinamento> trainingType;

            trainingType = _trainingType.GetTrainingTypes();

            ViewData["TipoTreinamento"] = trainingType;

            return View("Create");
        }

        //GET: training/Details/5
        public ActionResult Details(int id)
        {

            tblTreinamento training;
            training = _training.GetTrainingById(id);

            IEnumerable<tblTipoTreinamento> trainingType;
            trainingType = _trainingType.GetTrainingTypes();

            ViewData["TipoTreinamento"] = trainingType;

            if (training == null)
                return View("Index");

            return View("Edit", training);
        }


        // VERIFICAR
        public ActionResult Push(int id, int idwz)
        {
            var employe = _training.GetTrainingById(id);
            employe.idTipoTreinamento = idwz;

            _training.UpdateTraining(employe);



            return RedirectToAction("Details", new { id = idwz});
        }

        // VERIFICAR
        public ActionResult Pop(int id, int idwz)
        {
            var training = _training.GetTrainingById(id);
            training.idTipoTreinamento = null;

            _training.UpdateTraining(training);

            //var trainingType = _trainingType.GetTrainingTypes(idwz);


            return RedirectToAction("Details", new { id = idwz });
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
            return View("training");
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
