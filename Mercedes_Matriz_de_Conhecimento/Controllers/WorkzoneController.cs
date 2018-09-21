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
using PagedList;
using System.Configuration;
using Mercedes_Matriz_de_Conhecimento.Helpers;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class WorkzoneController : BaseController
    {


        private WorkzoneService _workzone;
        private EmployeeService _employee;
        private WorzoneXEmployeeService _workzoneXemployee;

        public WorkzoneController()
        {

            SetImage();

            _workzone = new WorkzoneService();
            _employee = new EmployeeService();
            _workzoneXemployee = new WorzoneXEmployeeService();

        }

        public void SetImage()
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
        }

        // GET: workzone
        [HttpGet]
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PostodeTrabalho, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IPagedList<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzonesWithPagination(page, pages_quantity);

            return View(workzone);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PostodeTrabalho, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;


            setBUCCLINHA();

            return View("Create");
        }


        // Cria as listas de BU, CC e Linha
        public void setBUCCLINHA(int idWorkzone = 0)
        {


            var listaBU = new List<SelectListItem>();
            var listaCC = new List<SelectListItem>();
            var listaLinha = new List<SelectListItem>();

            var indexBU = 0;
            var indexCC = 0;
            var indexLinha = 0;

            try
            {
                var permissions = AuthorizationHelper.GetSystem();
                var grupo = permissions.GruposDeSistema.Find(g => g.Nome == "Grupo_CentroCusto_Linha");

                foreach (var bu in grupo.Funcionalidades.Filhos)
                {
                    var itemBU = new SelectListItem { Selected = false, Text = bu.Nome, Value = bu.Nome };
                    listaBU.Insert(indexBU, itemBU);
                    indexBU++;

                    foreach (var f2 in bu.Filhos)
                    {
                        var itemCC = new SelectListItem { Selected = false, Text = f2.Nome, Value = bu.Nome };
                        listaCC.Insert(indexCC, itemCC);
                        indexCC++;

                        foreach (var f3 in f2.Filhos)
                        {
                            var itemLinha = new SelectListItem { Selected = false, Text = f3.Nome, Value = f3.Nome };
                            listaLinha.Insert(indexLinha, itemLinha);
                            indexLinha++;
                        }
                    }
                }
            }
            catch
            {
                if (idWorkzone != 0)
                {
                    var workzone = _workzone.GetWorkzoneById(idWorkzone);

                    var itemBU = new SelectListItem { Selected = false, Text = workzone.idBU, Value = workzone.idBU };
                    listaBU.Insert(indexBU, itemBU);
                    var itemCC = new SelectListItem { Selected = false, Text = workzone.idCC, Value = workzone.idCC };
                    listaCC.Insert(indexCC, itemCC);
                    var itemLinha = new SelectListItem { Selected = false, Text = workzone.idLinha, Value = workzone.idLinha };
                    listaLinha.Insert(indexLinha, itemLinha);
                }
                else
                {
                    var itemBU = new SelectListItem { Selected = false, Text = "", Value = "" };
                    listaBU.Insert(indexBU, itemBU);
                    var itemCC = new SelectListItem { Selected = false, Text = "", Value = "" };
                    listaCC.Insert(indexCC, itemCC);
                    var itemLinha = new SelectListItem { Selected = false, Text = "", Value = "" };
                    listaLinha.Insert(indexLinha, itemLinha);
                }

            }

            SelectList BU = new SelectList(listaBU, "Value", "Text");
            SelectList CC = new SelectList(listaCC, "Value", "Text");
            SelectList Linha = new SelectList(listaLinha, "Value", "Text");

            ViewData["BU"] = BU;
            ViewData["CC"] = CC;
            ViewData["LINHA"] = Linha;

        }


        [OutputCache(Duration = 1)]
        public ActionResult SearchUser(int idWorkzone, string nome = "")
        {
            ModelState.Clear();
            tblWorkzone workzone;
            workzone = _workzone.GetWorkzoneById(idWorkzone);

            /* SELECIONA FUNCIONÁRIOS ADICIONADOS NESSA WZ*/
            IEnumerable<tblFuncionarios> employeeAdded;
            employeeAdded = _workzone.setUpEmployees(idWorkzone);
            IEnumerable<tblFuncionarios> funcFiltrados;
            funcFiltrados = _employee.GetEmployeeByName(nome);

            ViewBag.Name = nome;
            ViewData["Funcionarios"] = funcFiltrados;
            ViewData["FuncionariosAdicionados"] = employeeAdded;

            FuncListModel Func = new FuncListModel();

            Func.idWorkzone = idWorkzone;
            Func.funcionariosAdded = employeeAdded;
            Func.funcionarios = funcFiltrados;
            UpdateModel(Func);

            //return RedirectToAction("Details", new { id = workzone.IdWorkzone, nome = nome });

            return PartialView("_Lista", Func);
        }

        //GET: workzone/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PostodeTrabalho, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id, string nome = "")
        {

            FuncListModel Func = new FuncListModel();
            Func.idWorkzone = id;
            var x = ViewBag.Name;

            ModelState.Clear();
            ViewData.Clear();
            UpdateModel(ViewData);
            /*  MONTANDO SELECT LIST BU, CC E LINHA*/
            setBUCCLINHA(id);
            /*FINALIZANDO SELECT LISTA BU, CC E LINHA*/

            SetImage();

            tblWorkzone workzone;
            workzone = _workzone.GetWorkzoneById(id);
            /*SELECIONA FUNCIONÁRIOS EXISTENTES*/
            IEnumerable<tblFuncionarios> employee;
            if (nome.Length > 0)
                employee = _employee.GetEmployeeByName(nome);
            else
                employee = _employee.GetEmployees();
            Func.funcionarios = employee;
            ViewData["Funcionarios"] = employee;

            /* SELECIONA FUNCIONÁRIOS ADICIONADOS NESSA WZ*/
            IEnumerable<tblFuncionarios> employeeAdded;
            employeeAdded = _workzone.setUpEmployees(id);
            Func.funcionariosAdded = employeeAdded;

            ViewData["FuncionariosAdicionados"] = employeeAdded;

            if (workzone == null)
                return View("Index");

            return View("Edit", workzone);
        }

        public ActionResult Push(int id, int idwz)
        {
            tblWorkzoneXFuncionario infoToPush = new tblWorkzoneXFuncionario();

            infoToPush.idFuncionario = id;
            infoToPush.idWorkzone = idwz;

            var exists = _workzoneXemployee.checkIfWorzoneXEmployeeAlreadyExits(infoToPush);

            if (ModelState.IsValid && !exists)
                _workzoneXemployee.CreateWorzoneXEmployee(infoToPush);

            return RedirectToAction("Details", new { id = idwz });
        }

        public ActionResult Pop(int id, int idwz)
        {
            tblWorkzoneXFuncionario wzXemployee = _workzoneXemployee.GetWorzoneXEmployeeById(idwz, id);

            if (ModelState.IsValid)
                _workzoneXemployee.DeleteWorzoneXEmployee(wzXemployee.idWorkzoneFuncionario);

            var workzone = _workzone.GetWorkzoneById(idwz);


            return RedirectToAction("Details", new { id = idwz });
        }


        // GET: workzone/Create
        [HttpPost]
        public ActionResult Create(tblWorkzone workzone)
        {
            var exits = _workzone.checkIfWorkzoneAlreadyExits(workzone);
            workzone.UsuarioCriacao = "Teste Sem Seg";
            workzone.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    workzone.FlagAtivo = true;

                    var returnedElement = _workzone.CreateWorkzone(workzone);

                    return RedirectToAction("Details", new { id = returnedElement.IdWorkzone });

                }

            }

            if (exits)
                ModelState.AddModelError("Nome", "Workzone já existe");

            return View("Create");
        }


        // GET: workzone/Edit/5
        [HttpPost]
        public ActionResult Edit(tblWorkzone workzone, int id)
        {
            workzone.IdWorkzone = id;
            var exits = _workzone.checkIfWorkzoneAlreadyExits(workzone);

            workzone.UsuarioAlteracao = "Usuário Teste Edit";
            workzone.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _workzone.UpdateWorkzone(workzone);

                    return RedirectToAction("Index");
                }

            }
            return View(workzone);
        }


        // GET: workzone/Delete/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.PostodeTrabalho, Feature = FeaturesHelper.Excluir)]
        public ActionResult Delete(int id)
        {

            _workzone.DeleteWorkzone(id);

            return RedirectToAction("Index");

        }


    }
}
