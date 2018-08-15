using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class MatrixController : Controller
    {

        private MatrizWorkzoneService _matrizService;
        private MatrizWorkzoneService _workzone;

        public MatrixController()
        {
            _matrizService = new MatrizWorkzoneService();

        }
        // GET: Matrix
        public ActionResult Index()
        {

            var workzone = _matrizService.GetMatrizById(1);
            var workzone2 = _workzone.GetMatrizById(1);
            int[] teste = { 1, 2, 3, 1, 2, 3, 5, 6, 3, 2, 2, 1, 2, 3, 5 };
            int[] teste2 = { 1, 2, 3, 1, 2, 3, 5, 6, 3, 2, 2, 1, 2, 3, 5 };
            ViewBag.numbers = teste;
            ViewBag.numbers2 = teste2;

            return View(workzone2);
        }
    }
}