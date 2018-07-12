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

        public TrainingTypeController()
        {
            _trainingType = new TrainingTypeService();

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

            return View("Create");
        }

        //GET: training/Details/5
        public ActionResult Details(int id)
        {

            tblTipoTreinamento training;
            training = _trainingType.GetTrainingTypeById(id);

            if (training == null)
                return View("Index");

            return View("Edit", training);
        }


        // VERIFICAR
        //public ActionResult Push(int id, int idwz)
        //{
        //    var employe = _trainingType.GetTrainingById(id);
        //    employe.idTipoTreinamento = idwz;

        //    _trainingType.UpdateTraining(employe);



        //    return RedirectToAction("Details", new { id = idwz});
        //}

        // VERIFICAR
        //public ActionResult Pop(int id, int idwz)
        //{
        //    var training = _trainingType.GetTrainingById(id);
        //    training.idTipoTreinamento = null;

        //    _trainingType.UpdateTraining(training);

        //    //var trainingType = _trainingType.GetTrainingTypes(idwz);


        //    return RedirectToAction("Details", new { id = idwz });
        //}

    
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
            return View("training");
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
