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
    public class EmployeeController : Controller
    {

        private EmployeeService _employee;

        private WorkzoneService _workzone;

        public EmployeeController()
        {
            _employee = new EmployeeService();
            _workzone = new WorkzoneService();
        }

        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<tblFuncionarios> employee;
            employee = _employee.GetEmployees();

            return View(employee);

        }

        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;

            workzone = _workzone.GetWorkzones();

            ViewData["Workzone"] = workzone;

            return View("Employee");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<tblWorkzone> workzone;
            tblFuncionarios employee;

            workzone = _workzone.GetWorkzones();
            employee = _employee.GetEmployeeById(id);

            ViewData["Workzone"] = workzone;


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
                ModelState.AddModelError("Nome", "Funcionário já existente");

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
        public ActionResult Delete(int id)
        {

            _employee.DeleteEmployee(id);

            return RedirectToAction("Index");

        }


    }
}
