using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using DCX.ITLC.AutSis.Services.Integracao;
using Mercedes_Matriz_de_Conhecimento.Helpers;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ActivityController : Controller
    {


        private ActivityService _activity;
        private ActivityProfileService _activityProfile;
        private ActivityGroupService _activityGroup;


        public ActivityController()
        {
            _activity = new ActivityService();
            _activityProfile = new ActivityProfileService();
            _activityGroup = new ActivityGroupService();

            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;
        }

        // GET: activity
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Atividades, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            //var Teste = new IntegracaoAutSis().ObterPermissoes("StepNet", "SILALIS", 0);
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblAtividades> activity;
            activity = _activity.GetActivitiesWithPagination(page, pages_quantity);

            return View(activity);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Atividades, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblPerfis> activityProfile;

            activityProfile = _activityProfile.GetActivityProfilesByType();

            ViewData["PerfildeAtividade"] = activityProfile;

            return View("Create");
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Atividades, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {

            // Declaração de variaveis
            tblAtividades activity;
            IEnumerable<tblPerfis> activityProfile;
            IEnumerable<tblAtividades> allActivies;
            IEnumerable<tblAtividades> activitiesGroup;

            //chamadas dos métodos(no service) e assignment
            activity = _activity.GetActivityById(id);
            activityProfile = _activityProfile.GetActivityProfilesByType();
            activitiesGroup = _activityGroup.setUpActivitys(id);
            allActivies = _activity.GetActivities();

            ViewData["PerfildeAtividade"] = activityProfile;
            ViewData["TodasAtividades"] = allActivies;
            ViewData["AtividadesFilhos"] = activitiesGroup;

            if (activity == null)
                return View("Index");

            return View("Edit", activity);
        }

        public ActionResult Push(int idDaddy, int idSon)
        {

            tblGrupoAtividades training = new tblGrupoAtividades();
            training.idAtividadePai = idDaddy;
            training.idAtividadeFilho = idSon;
            var exits = _activityGroup.checkIfActivityGroupAlreadyExits(training);

            if (!exits)
                _activityGroup.CreateActivityGroup(training);

            return RedirectToAction("Details", new { id = idDaddy });
        }

        public ActionResult Pop(int idDaddy, int idSon)
        {
            _activityGroup.DeleteActivityGroup(idDaddy, idSon);

            return RedirectToAction("Details", new { id = idDaddy });
        }

        [HttpPost]
        public ActionResult Create(tblAtividades activity)
        {
            var exits = _activity.checkIfActivityAlreadyExits(activity);
            activity.UsuarioCriacao = "Teste Sem Seg";
            activity.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activity.CreateActivity(activity);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblPerfis> activityProfile;
            activityProfile = _activityProfile.GetActivityProfiles();
            ViewData["PerfildeAtividade"] = activityProfile;

            if (exits)
                ModelState.AddModelError("Nome", "Atividade já existe");

            return View("Create");
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblAtividades activity, int id)
        {
            activity.idAtividade = id;
            var exits = _activity.checkIfActivityAlreadyExits(activity);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _activity.UpdateActivity(activity);

                    return RedirectToAction("Index");
                }

            }
            return View(activity);
        }


        // GET: activity/Delete/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Atividades, Feature = FeaturesHelper.Deletar)]
        public ActionResult Delete(int id)
        {

            _activity.DeleteActivity(id);

            return RedirectToAction("Index");

        }


    }
}
