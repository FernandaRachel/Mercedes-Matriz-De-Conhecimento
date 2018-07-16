using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class MatrixController : Controller
    {
        // GET: Matrix
        public ActionResult Index()
        {
            int[] teste = { 1, 1, 1, 1, 1, 2, 3, 2, 2, 1, 2, 3 };
            int[] teste2 = { 1, 2, 3, 1, 2, 3, 5, 6, 3, 2, 2, 1 };
            ViewBag.numbers = teste;
            ViewBag.numbers2 = teste2;

            return View();
        }
    }
}