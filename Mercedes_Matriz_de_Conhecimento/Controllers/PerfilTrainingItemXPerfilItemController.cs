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
    public class PerfilTrainingItemXPerfilItemController : BaseController
    {


        private PerfilTrainingItemXPerfilItemService _perfilTrainingItemXPerfilItem;
        private TrainingProfileService _profileTraining;
        private ProfileItemService _profileItemTraining;


        public PerfilTrainingItemXPerfilItemController()
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

            _perfilTrainingItemXPerfilItem = new PerfilTrainingItemXPerfilItemService();
            _profileTraining = new TrainingProfileService();
            _profileItemTraining = new ProfileItemService();
        }

        // GET: perfilAtivItemXPerfilItem
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilTreinamentoItemXPerfilItem, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfis> perfis;
            perfis = _perfilTrainingItemXPerfilItem.GetPerfilTrainingItemXPerfilItemsWithPagination(page, pages_quantity);

            return View(perfis);

        }

        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilTreinamentoItemXPerfilItem, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblPerfis> activityProfile;
            activityProfile = _profileTraining.GetTrainingProfiles();
            ViewData["PerfilTreinamentos"] = activityProfile;

            return View("Create");
        }

        [OutputCache(Duration = 1)]
        public ActionResult SearchProfileItem(int idProfile, string nome = "")
        {
            ViewBag.Name = nome;

            IEnumerable<tblPerfilItens> profileItemFiltered;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemFiltered = _profileItemTraining.GetProfileItemByName(nome);
            profilesAdded = _perfilTrainingItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemFiltered;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileTraining.GetTrainingProfileById(idProfile).Nome;
            UpdateModel(profileItem);

            return PartialView("_Lista", profileItem);
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilTreinamentoItemXPerfilItem, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int idProfile)
        {
            // Declaração de variaveis
            tblPerfilTreinamentoxPerfilItem profileXprofileItem = new tblPerfilTreinamentoxPerfilItem();
            profileXprofileItem.IdPerfilTreinamento = idProfile;
            profileXprofileItem.tblPerfis = _profileTraining.GetTrainingProfileById(idProfile);


            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemTraining.GetProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilTrainingItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileTraining.GetTrainingProfileById(idProfile).Nome;
            UpdateModel(profileItem);



            return View("Edit", profileXprofileItem);
        }

        [HttpPost]
        public ActionResult Create(tblPerfilTreinamentoxPerfilItem perfilAtivItemXPerfilItem)
        {
            var exits = _perfilTrainingItemXPerfilItem.checkIfPerfilTrainingItemXPerfilItemAlreadyExits(perfilAtivItemXPerfilItem);
            var orderExits = _perfilTrainingItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);


            if (ModelState.IsValid)
            {
                if (!exits && !orderExits)
                {
                    _perfilTrainingItemXPerfilItem.CreatePerfilTrainingItemXPerfilItem(perfilAtivItemXPerfilItem);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblPerfis> activityProfile;
            IEnumerable<tblPerfilItens> profileItemActivity;
            activityProfile = _profileTraining.GetTrainingProfiles();
            profileItemActivity = _profileItemTraining.GetProfileItems();
            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;

            if (orderExits)
                ModelState.AddModelError("Ordem", "Ordem já existe");
            else if (exits)
                ModelState.AddModelError("IdPerfilItem", " Atividade Item já associada a Perfil Atividade");


            return View("Create");
        }



        public ActionResult Push(int idItem, int idProfile, int ordem = 0)
        {
            tblPerfilTreinamentoxPerfilItem profileXprofileItem = new tblPerfilTreinamentoxPerfilItem();
            profileXprofileItem.IdPerfilTreinamento = idProfile;
            profileXprofileItem.IdPerfilItem = idItem;
            profileXprofileItem.Ordem = ordem;

            ViewBag.ProfileName = _profileTraining.GetTrainingProfileById(idProfile).Nome;


            var exits = _perfilTrainingItemXPerfilItem.checkIfPerfilTrainingItemXPerfilItemAlreadyExits(profileXprofileItem);
            var ordemExists = _perfilTrainingItemXPerfilItem.checkIfOrderAlreadyExits(profileXprofileItem);

            if (ModelState.IsValid && ordem != 0)
            {
                if (!exits && !ordemExists)
                {
                    _perfilTrainingItemXPerfilItem.CreatePerfilTrainingItemXPerfilItem(profileXprofileItem);

                    return RedirectToAction("Details", new { idProfile = idProfile });
                }

            }

            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemTraining.GetProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilTrainingItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileTraining.GetTrainingProfileById(idProfile).Nome;
            //UpdateModel(profileItem);

            /*GERANDO MENSAGENS DE VALIDAÇÃO*/
            if (exits)
                ModelState.AddModelError("IdPerfilTreinamento", "Perfil já associado a esse item");
            if (ordemExists)
                ModelState.AddModelError("Ordem", "Ordem já existente");
            if (ordem == 0)
                ModelState.AddModelError("Ordem", "Ordem deve ser preenchida(apenas números)");
            return View("Edit", profileXprofileItem);
        }

        public ActionResult Pop(int idItem, int idProfile)
        {
            _perfilTrainingItemXPerfilItem.DeletePerfilTrainingItemXPerfilItem(idProfile, idItem);

            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemTraining.GetProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilTrainingItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileTraining.GetTrainingProfileById(idProfile).Nome;
            UpdateModel(profileItem);


            return RedirectToAction("Details", new { idProfile = idProfile });
        }

        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilTreinamentoxPerfilItem perfilAtivItemXPerfilItem)
        {
            // Verifica se ordem já existe
            var exits = _perfilTrainingItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _perfilTrainingItemXPerfilItem.UpdatePerfilTrainingItemXPerfilItem(perfilAtivItemXPerfilItem);

                    return RedirectToAction("Index");
                }

                if (exits)
                    ModelState.AddModelError("Ordem", "Ordem já existe");

                return View("Edit");
            }

            IEnumerable<tblPerfis> activityProfile;
            IEnumerable<tblPerfilItens> profileItemActivity;

            activityProfile = _profileTraining.GetTrainingProfiles();
            profileItemActivity = _profileItemTraining.GetProfileItems();

            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;
            return View(perfilAtivItemXPerfilItem);
        }
    }
}
