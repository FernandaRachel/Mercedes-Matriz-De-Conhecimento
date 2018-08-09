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
    public class TrainingProfileController : Controller
    {


        private TrainingProfileService _trainingProfile;


        public TrainingProfileController()
        {
            _trainingProfile = new TrainingProfileService();

        }

        // GET: TrainingProfile
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfis> trainingProfile;
            trainingProfile = _trainingProfile.GetTrainingProfilesWithPagination(page, pages_quantity);

            return View(trainingProfile);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: TrainingProfile/Details/5
        public ActionResult Details(int id)
        {

            tblPerfis trainingProfile;
            trainingProfile = _trainingProfile.GetTrainingProfileById(id);


            if (trainingProfile == null)
                return View("Index");

            return View("Edit", trainingProfile);
        }



        // GET: TrainingProfile/Create
        [HttpPost]
        public ActionResult Create(tblPerfis trainingProfile)
        {
            var exits = _trainingProfile.checkIfTrainingProfileAlreadyExits(trainingProfile);
            trainingProfile.UsuarioCriacao = "Teste Sem Seg";
            trainingProfile.DataCriacao = DateTime.Now;
            trainingProfile.Tipo = "T";

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingProfile.CreateTrainingProfile(trainingProfile);

                    return RedirectToAction("Index");
                }
            }


            if (exits)
                ModelState.AddModelError("Nome", "Perfil de Treinamento já existe");

            return View("Create");
        }


        // GET: TrainingProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfis trainingProfile, int id)
        {
            trainingProfile.IdPerfis = id;
            var exits = _trainingProfile.checkIfTrainingProfileAlreadyExits(trainingProfile);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _trainingProfile.UpdateTrainingProfile(trainingProfile);

                    return RedirectToAction("Index");
                }

            }
            return View(trainingProfile);
        }


        // GET: TrainingProfile/Delete/5
        public ActionResult Delete(int id)
        {

            _trainingProfile.DeleteTrainingProfile(id);

            return RedirectToAction("Index");

        }


    }
}
