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
    public class TrainingStatusController : Controller
    {


        private TrainingStatusService _trainingStatus;


        public TrainingStatusController()
        {
            _trainingStatus = new TrainingStatusService();
        }

        // GET: trainingStatus
        public ActionResult Index()
        {
            IEnumerable<tblTreinamentoStatus> trainingStatus;
            trainingStatus = _trainingStatus.GetTrainingStatuss();

            return View(trainingStatus);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: trainingStatus/Details/5
        public ActionResult Details(int id)
        {
            // Declaração de variaveis
            tblTreinamentoStatus trainingStatus;

            //chamadas dos métodos(no service) e assignment
            trainingStatus = _trainingStatus.GetTrainingStatusById(id);


            if (trainingStatus == null)
                return View("Index");

            return View("Edit", trainingStatus);
        }


        // GET: trainingStatus/Create
        [HttpPost]
        public ActionResult Create(tblTreinamentoStatus trainingStatus)
        {
            var exits = _trainingStatus.checkIfTrainingStatusAlreadyExits(trainingStatus);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingStatus.CreateTrainingStatus(trainingStatus);

                    return RedirectToAction("Index");
                }
            }


            if (exits)
                ModelState.AddModelError("Nome", "Status já existe");

            return View("Create");
        }


        // GET: trainingStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(tblTreinamentoStatus trainingStatus, int id)
        {
            trainingStatus.idStatusTreinamento = id;
            var exits = _trainingStatus.checkIfTrainingStatusAlreadyExits(trainingStatus);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingStatus.UpdateTrainingStatus(trainingStatus);

                    return RedirectToAction("Index");
                }

            }
            return View(trainingStatus);
        }


        // GET: trainingStatus/Delete/5
        public ActionResult Delete(int id)
        {

            _trainingStatus.DeleteTrainingStatus(id);

            return RedirectToAction("Index");

        }


    }
}
