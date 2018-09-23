using Mercedes_Matriz_de_Conhecimento.Helpers;
using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class HomeController : Controller
    {

        private AutSisWebApiService _autSisService;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeController()
        {
            log.Debug("Home Controller called");

            _autSisService = new AutSisWebApiService();
        }

        public ActionResult Index()
        {

            //Pega o nome do usuário para exibir na barra de navegação

            try
            {

                var username = AuthorizationHelper.GetSystem();
                ViewBag.User = username.Usuario.ChaveAmericas;
                if (username != null)
                {
                    var imgUser = AuthorizationHelper.GetUserImage(username.Usuario.ChaveAmericas);
                    ViewBag.UserPhoto = imgUser;
                }
            }
            catch(Exception ex)
            {
                log.Debug(ex.Message.ToString());

                var imgUser = AuthorizationHelper.GetUserImage("");

                ViewBag.User = "";
                ViewBag.UserPhoto = imgUser;
            }

            return View();
        }

        public void SetMenu(SistemaApi permissions)
        {

        }


        [AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.Funcionario, Feature = FeaturesHelper.Consultar)]
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