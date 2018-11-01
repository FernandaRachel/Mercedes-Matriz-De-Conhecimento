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
using log4net;
using System.Data.SqlClient;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class EmployeeController : BaseController
    {

        private EmployeeService _employee;
        private WorkzoneService _workzone;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EmployeeController()
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

            _employee = new EmployeeService();
            _workzone = new WorkzoneService();
        }

        // GET: Employee
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblFuncionarios> employee;
            employee = _employee.GetEmployeesWithPagination(page, pages_quantity);

            return View(employee);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;

            setBUCCLINHA();

            return View("Create");
        }


        public void setBUCCLINHA(int idEmployee = 0)
        {
            var listaBU = new List<SelectListItem>();
            var indexBU = 0;

            try
            {

                var permissions = AuthorizationHelper.GetSystem();
                var grupo = permissions.GruposDeSistema.Find(g => g.Nome == "Grupo_CentroCusto_Linha");

                foreach (var bu in grupo.Funcionalidades.Filhos)
                {
                    var itemBU = new SelectListItem { Selected = false, Text = bu.Nome, Value = bu.Nome };
                    listaBU.Insert(indexBU, itemBU);
                    indexBU++;

                }
            }
            catch
            {
                if (idEmployee != 0)
                {
                    var employee = _employee.GetEmployeeById(idEmployee);
                    var itemBU = new SelectListItem { Selected = false, Text = employee.idBu_Origem, Value = employee.idBu_Origem };
                    listaBU.Insert(indexBU, itemBU);
                }
                else
                {
                    var itemBU = new SelectListItem { Selected = false, Text = "", Value = "" };
                    listaBU.Insert(indexBU, itemBU);
                }
            }
            SelectList BU = new SelectList(listaBU, "Value", "Text");

            ViewData["BU"] = BU;

        }
        // GET: Employee/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {
            IEnumerable<tblWorkzone> workzone;
            tblFuncionarios employee;

            workzone = _workzone.GetWorkzones();
            employee = _employee.GetEmployeeById(id);

            ViewData["Workzone"] = workzone;

            setBUCCLINHA(id);

            if (employee == null)
                return HttpNotFound("O Funcionário desejado não existe");

            return View("Edit", employee);
        }


        // GET: Employee/Create
        [HttpPost]
        public ActionResult Create(tblFuncionarios employee)
        {
            var exits = _employee.checkIfUserAlreadyExits(employee);
            employee.Ativo = true;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    try
                    {
                        _employee.CreateEmployee(employee);

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        log.Debug(ex.Message.ToString());

                        return RedirectToAction("Index");
                    }
                }
            }

            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;

            if (exits)
                ModelState.AddModelError("Nome", "Funcionário já existe");

            setBUCCLINHA();

            return View(employee);
        }


        // GET: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(tblFuncionarios employee, int id)
        {
            employee.idfuncionario = id;
            var exits = _employee.checkIfUserAlreadyExits(employee);
            employee.Ativo = true;

            if (ModelState.IsValid)
            {
                if (!exits)
                {

                    _employee.UpdateEmployee(employee);

                    return RedirectToAction("Index");
                }
            }

            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;

            if (exits)
                ModelState.AddModelError("Nome", "Funcionário já existe");

            return View("Edit",employee);
        }

        public ActionResult ConfirmEmployeeDelete(int id)
        {
            var workzoneToDelete = _workzone.GetWorkzoneById(id);

            return PartialView("ConfirmDelete", workzoneToDelete);
        }

        // GET: Employee/Delete/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Excluir)]
        public ActionResult Delete(int id)
        {
            try
            {

                _employee.DeleteEmployee(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("conflicted with the REFERENCE constraint "))
                    ViewBag.Erro = "Você não pode executar essa ação, pois essa informação está em uso";
                    return View("Error");
            }
        }


    }
}
