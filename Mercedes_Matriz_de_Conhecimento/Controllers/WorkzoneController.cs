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
    public class WorkzoneController : Controller
    {


        private WorkzoneService _workzone;
        private EmployeeService _employee;

        public WorkzoneController()
        {
            _workzone = new WorkzoneService();
            _employee = new EmployeeService();

        }

        // GET: workzone
        public ActionResult Index()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();

            return View(workzone);

        }

        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;

            workzone = _workzone.GetWorkzones();

            ViewData["Workzone"] = workzone;

            return View("Create");
        }

        //GET: workzone/Details/5
        public ActionResult Details(int id)
        {

            tblWorkzone workzone;
            workzone = _workzone.GetWorkzoneById(id);

            IEnumerable<tblFuncionarios> employee;
            employee = _employee.GetEmployees();
            ViewData["Funcionarios"] = employee;

            if (workzone == null)
                return View("Index");

            return View("Edit", workzone);
        }

        public ActionResult Push(int id, int idwz)
        {
            var employe = _employee.GetEmployeeById(id);
            employe.idWorkzone = idwz;

            _employee.UpdateEmployee(employe);

            var workzone = _workzone.GetWorkzoneById(idwz);


            return RedirectToAction("Details", new { id = idwz});
        }

        public ActionResult Pop(int id, int idwz)
        {
            var employe = _employee.GetEmployeeById(id);
            employe.idWorkzone = null;

            _employee.UpdateEmployee(employe);

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

                    _workzone.CreateWorkzone(workzone);
                    return RedirectToAction("Index");

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
        public ActionResult Delete(int id)
        {

            _workzone.DeleteWorkzone(id);

            return RedirectToAction("Index");

        }


    }
}
