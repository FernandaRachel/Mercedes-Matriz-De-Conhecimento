using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class HistMatrixController : Controller
    {
        HistMatrixService _histMatrix;

        public HistMatrixController()
        {
            _histMatrix = new HistMatrixService();
        }

        // GET: HistMatrix
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SavePdf(HttpPostedFileBase uploadFile, string FilePath)
        {
            _histMatrix.SavePdf(uploadFile, FilePath);

            return RedirectToAction("Index");
        }

        public void ReadPdf(int id)
        {
            _histMatrix.ReadPdf(id);
        }
    }
}