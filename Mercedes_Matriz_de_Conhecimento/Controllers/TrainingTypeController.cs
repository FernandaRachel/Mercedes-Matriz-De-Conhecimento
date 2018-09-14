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

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class TrainingTypeController : BaseController
    {



        private TrainingTypeService _trainingType;
        private TrainingProfileService _trainingProfile;

        public TrainingTypeController()
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

            _trainingType = new TrainingTypeService();
            _trainingProfile = new TrainingProfileService();

        }

        // GET: training
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.TipodeTreinamento, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblTipoTreinamento> training;
            training = _trainingType.GetTrainingTypesWithPagination(page,pages_quantity);

            return View(training);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.TipodeTreinamento, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblPerfis> trainingProile;
            trainingProile = _trainingProfile.GetTrainingProfiles();


            ViewData["PerfilTreinamento"] = trainingProile;
            ViewBag.SelectedProfile = _trainingProfile.GetFirstProfile();

            return View("Create");
        }

        //GET: training/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.TipodeTreinamento, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {
            IEnumerable<tblPerfis> trainingProile;
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

            IEnumerable<tblPerfis> trainingProile;
            trainingProile = _trainingProfile.GetTrainingProfiles();
            ViewData["PerfilTreinamento"] = trainingProile;

            if (exits)
                ModelState.AddModelError("Nome", "Tipo de Treinamento já existe");

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
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.TipodeTreinamento, Feature = FeaturesHelper.Deletar)]
        public ActionResult Delete(int id)
        {

            _trainingType.DeleteTrainingType(id);

            return RedirectToAction("Index");

        }


    }
}
