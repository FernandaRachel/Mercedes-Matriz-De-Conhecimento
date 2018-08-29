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
    public class EmployeeController : Controller
    {

        private EmployeeService _employee;

        private WorkzoneService _workzone;

        public EmployeeController()
        {
            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;

            _employee = new EmployeeService();
            _workzone = new WorkzoneService();
        }

        // GET: Employee
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblFuncionarios> employee;
            employee = _employee.GetEmployeesWithPagination(page, pages_quantity);

            return View(employee);

        }

        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;

            var innerX = new List<SelectListItem>();
            SelectListItem innerXItem = new SelectListItem { Selected = false, Text = "1", Value = "1" };
            SelectListItem innerXItem2 = new SelectListItem { Selected = false, Text = "2", Value = "2" };
            innerX.Insert(0, innerXItem);
            innerX.Insert(0, innerXItem2);
            SelectList BU = new SelectList(innerX, "Value", "Text");

            ViewData["BU"] = BU;

            return View("Create");
        }

        // GET: Employee/Details/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {
            IEnumerable<tblWorkzone> workzone;
            tblFuncionarios employee;

            workzone = _workzone.GetWorkzones();
            employee = _employee.GetEmployeeById(id);

            ViewData["Workzone"] = workzone;

            var innerX = new List<SelectListItem>();
            SelectListItem innerXItem = new SelectListItem { Selected = false, Text = "1", Value = "1" };
            SelectListItem innerXItem2 = new SelectListItem { Selected = false, Text = "2", Value = "2" };
            innerX.Insert(0, innerXItem);
            innerX.Insert(0, innerXItem2);
            SelectList BU = new SelectList(innerX, "Value", "Text");

            ViewData["BU"] = BU;

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

                    _employee.CreateEmployee(employee);
                    return RedirectToAction("Index");
                }
            }

            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;

            if (exits)
                ModelState.AddModelError("Nome", "Funcionário já existe");

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

            return View(employee);
        }

        public ActionResult ConfirmEmployeeDelete(int id)
        {
            var workzoneToDelete = _workzone.GetWorkzoneById(id);

            return PartialView("ConfirmDelete", workzoneToDelete);
        }

        // GET: Employee/Delete/5
        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Deletar)]
        public ActionResult Delete(int id)
        {

            _employee.DeleteEmployee(id);

            return RedirectToAction("Index");
        }


    }
}
