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
    public class HomeController : Controller
    {

        private AutSisWebApiService _autSisService;

        public HomeController()
        {
            _autSisService = new AutSisWebApiService();
        }

        public async Task<SistemaApi>  setCookies()
        {
            var usuario = "brleite";

            var permissions = await _autSisService.getPermissions(usuario);

            ViewBag.JSON = permissions;

            return permissions;
        }

        //[UserAutorizationFilter]
        public async Task<ActionResult> Index()
        {
            var permissions  = await setCookies();

            //Pega o nome do usuário para exibir na barra de navegação
            var username = AuthorizationHelper.GetSystem();
            ViewBag.User = username.Usuario.ChaveAmericas;
            //SetMenu(permissions);
            return View();
        }

        public void SetMenu(SistemaApi permissions)
        {
            //foreach(var f in permissions.FuncionalidadeAutorizacao.Filhos)
            //{
            //    foreach(var f2 in f.Filhos)
            //    {

            //        switch (f2.Filhos)
            //        {
            //            default:
            //                break;
            //        }
            //    }
                   
            //}
            //var menuEncontrado = permissions.FuncionalidadeAutorizacao.Filhos
            //    .Where(f => f.Nome.ToString().Replace("\\", "").Trim() == MenuHelper.VisualizacaoCadastro.ToString()).FirstOrDefault();


            //ViewBag.Funcionario = true;
            //ViewBag.Posto = true;
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