using Mercedes_Matriz_de_Conhecimento.Helpers;
using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class HomeController : BaseController
    {

        private AutSisWebApiService _autSisService;

        public HomeController()
        {
            _autSisService = new AutSisWebApiService();
        }

        public ActionResult Index()
        {

            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;

            return View();
        }

        public void SetMenu(SistemaApi permissions)
        {
            
        }


        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro,Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Consultar)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}