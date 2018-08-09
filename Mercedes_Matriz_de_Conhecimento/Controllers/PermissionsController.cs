using DCX.ITLC.AutSis.Services.Integracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class PermissionsController : Controller
    {
        // GET: Permissions
        public ActionResult Index()
        {
            return View(new IntegracaoAutSis().ObterPermissoes("StepNet", "SILALIS", 0));
        }

    }
}