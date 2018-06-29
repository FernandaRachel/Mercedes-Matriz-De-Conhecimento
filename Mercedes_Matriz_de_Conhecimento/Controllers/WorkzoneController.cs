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

        public WorkzoneController()
        {
            _workzone = new WorkzoneService();
        }

        // GET: workzone
        public ActionResult Index()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();

            return View(workzone);

        }

        public ActionResult RedirectToCreate()
        {
            IEnumerable<tblWorkzone> workzone;

            workzone = _workzone.GetWorkzones();

            ViewData["Workzone"] = workzone;

            return PartialView("workzone");
        }

        // GET: workzone/Details/5
        //public ActionResult Details(int id)
        //{

        //    IEnumerable<tblWorkzone> workzone;

        //    workzone = _workzone.GetworkzoneById(id);

        //    if (workzone == null)
        //        return HttpNotFound("O Funcionário desejado não existe");

        //    return View(workzone);
        //}

        // GET: workzone/Create
        [HttpPost]
        public ActionResult Create(tblWorkzone workzone)
        {
            var exits = _workzone.checkIfWorkzoneAlreadyExits(workzone);

            if (ModelState.IsValid)
            {
                if (exits == 1)
                {
                    workzone.FlagAtivo = 1;

                    _workzone.CreateWorkzone(workzone);
                    return RedirectToAction("Index");

                }

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Objeto inválido");

        }


        // GET: workzone/Edit/5
        public ActionResult Edit(int id, tblWorkzone workzone)
        {

            _workzone.UpdateWorkzone(workzone);

            return RedirectToAction("Index");
        }


        // GET: workzone/Delete/5
        public ActionResult Delete(int id)
        {

            _workzone.DeleteWorkzone(id);

            return RedirectToAction("Index");

        }


    }
}
