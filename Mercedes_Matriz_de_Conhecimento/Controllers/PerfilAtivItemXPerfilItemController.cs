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
    public class PerfilAtivItemXPerfilItemController : BaseController
    {


        private PerfilAtivItemXPerfilItemService _perfilAtivItemXPerfilItem;
        private ActivityProfileItemService _activityProfileItem;
        private ActivityProfileService _profileActivity;
        private ActivityProfileItemService _profileItemActivity;


        public PerfilAtivItemXPerfilItemController()
        {
            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;

            _perfilAtivItemXPerfilItem = new PerfilAtivItemXPerfilItemService();
            _activityProfileItem = new ActivityProfileItemService();
            _profileActivity = new ActivityProfileService();
            _profileItemActivity = new ActivityProfileItemService();
        }

        // GET: perfilAtivItemXPerfilItem
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilAtividadeItemXPerfilItem, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfis> perfis;
            perfis = _perfilAtivItemXPerfilItem.GetPerfilAtivItemXPerfilItemsWithPagination(page, pages_quantity);

            return View(perfis);

        }

        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilAtividadeItemXPerfilItem, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblPerfis> activityProfile;
            activityProfile = _profileActivity.GetActivityProfiles();
            ViewData["PerfilAtividade"] = activityProfile;

            return View("Create");
        }

        [OutputCache(Duration = 1)]
        public ActionResult SearchProfileItem(int idProfile, string nome = "")
        {
            ViewBag.Name = nome;

            IEnumerable<tblPerfilItens> profileItemFiltered;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemFiltered = _profileItemActivity.GetProfileItemActvByName(nome);
            profilesAdded = _perfilAtivItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemFiltered;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileActivity.GetActivityProfileById(idProfile).Nome;
            UpdateModel(profileItem);

            return PartialView("_Lista", profileItem);
        }

        //GET: Activity/Details/5
        [AccessHelper(Menu = MenuHelper.Associacao,Screen = ScreensHelper.PerfilAtividadeItemXPerfilItem, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int idProfile)
        {
            // Declaração de variaveis
            tblPerfilAtividadeXPerfilAtItem profileXprofileItem = new tblPerfilAtividadeXPerfilAtItem();
            profileXprofileItem.idPerfilAtividade = idProfile;
            profileXprofileItem.tblPerfis = _profileActivity.GetActivityProfileById(idProfile);


            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemActivity.GetActivityProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilAtivItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileActivity.GetActivityProfileById(idProfile).Nome;
            UpdateModel(profileItem);



            return View("Edit", profileXprofileItem);
        }

        [HttpPost]
        public ActionResult Create(tblPerfilAtividadeXPerfilAtItem perfilAtivItemXPerfilItem)
        {
            var exits = _perfilAtivItemXPerfilItem.checkIfPerfilAtivItemXPerfilItemAlreadyExits(perfilAtivItemXPerfilItem);
            var orderExits = _perfilAtivItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);


            if (ModelState.IsValid)
            {
                if (!exits && !orderExits)
                {
                    _perfilAtivItemXPerfilItem.CreatePerfilAtivItemXPerfilItem(perfilAtivItemXPerfilItem);
                    return RedirectToAction("Index");

                }

            }

            IEnumerable<tblPerfis> activityProfile;
            IEnumerable<tblPerfilItens> profileItemActivity;
            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _profileItemActivity.GetActivityProfileItems();
            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;

            if (orderExits)
                ModelState.AddModelError("Ordem", "Ordem já existe");
            else if (exits)
                ModelState.AddModelError("idPerfilAtivItem", " Atividade Item já associada a Perfil Atividade");


            return View("Create");
        }



        public ActionResult Push(int idItem, int idProfile, int ordem = 0)
        {
            tblPerfilAtividadeXPerfilAtItem profileXprofileItem = new tblPerfilAtividadeXPerfilAtItem();
            profileXprofileItem.idPerfilAtividade = idProfile;
            profileXprofileItem.idPerfilAtivItem = idItem;
            profileXprofileItem.Ordem = ordem;

            ViewBag.ProfileName = _profileActivity.GetActivityProfileById(idProfile).Nome;


            var exits = _perfilAtivItemXPerfilItem.checkIfPerfilAtivItemXPerfilItemAlreadyExits(profileXprofileItem);
            var ordemExists = _perfilAtivItemXPerfilItem.checkIfOrderAlreadyExits(profileXprofileItem);

            if (ModelState.IsValid && ordem != 0)
            {
                if (!exits && !ordemExists)
                {
                    _perfilAtivItemXPerfilItem.CreatePerfilAtivItemXPerfilItem(profileXprofileItem);

                    return RedirectToAction("Details", new { idProfile = idProfile });
                }

            }

            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemActivity.GetActivityProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilAtivItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileActivity.GetActivityProfileById(idProfile).Nome;
            UpdateModel(profileItem);

            /*GERANDO MENSAGENS DE VALIDAÇÃO*/
            if (exits)
                ModelState.AddModelError("idPerfilAtividade", "Perfil já associado a esse item");
            if (ordemExists)
                ModelState.AddModelError("Ordem", "Ordem já existente");
            if (ordem == 0)
                ModelState.AddModelError("Ordem", "Ordem deve ser preenchida");
            return View("Edit", profileXprofileItem);
        }

        public ActionResult Pop(int idItem, int idProfile)
        {
            _perfilAtivItemXPerfilItem.DeletePerfilAtivItemXPerfilItem(idProfile, idItem);

            IEnumerable<tblPerfilItens> profileItemList;
            IEnumerable<tblPerfilItens> profilesAdded;

            profileItemList = _profileItemActivity.GetActivityProfileItems();
            //Retorna todos os itens que fazem referencia ao Perfil chamado 'idProfile'
            profilesAdded = _perfilAtivItemXPerfilItem.SetUpPerfilItensLista(idProfile);

            ProfileItemListModel profileItem = new ProfileItemListModel();

            profileItem.idProfile = idProfile;
            profileItem.ProfileItem = profileItemList;
            profileItem.ProfileItemAdded = profilesAdded;
            profileItem.ProfileName = _profileActivity.GetActivityProfileById(idProfile).Nome;
            UpdateModel(profileItem);


            return RedirectToAction("Details", new { idProfile = idProfile });
        }

        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilAtividadeXPerfilAtItem perfilAtivItemXPerfilItem)
        {
            // Verifica se ordem já existe
            var exits = _perfilAtivItemXPerfilItem.checkIfOrderAlreadyExits(perfilAtivItemXPerfilItem);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _perfilAtivItemXPerfilItem.UpdatePerfilAtivItemXPerfilItem(perfilAtivItemXPerfilItem);

                    return RedirectToAction("Index");
                }

                if (exits)
                    ModelState.AddModelError("Ordem", "Ordem já existe");

                return View("Edit");
            }

            IEnumerable<tblPerfis> activityProfile;
            IEnumerable<tblPerfilItens> profileItemActivity;

            activityProfile = _profileActivity.GetActivityProfiles();
            profileItemActivity = _profileItemActivity.GetActivityProfileItems();

            ViewData["PerfilAtividade"] = activityProfile;
            ViewData["PerfilAtividadeItem"] = profileItemActivity;
            return View(perfilAtivItemXPerfilItem);
        }
    }
}
