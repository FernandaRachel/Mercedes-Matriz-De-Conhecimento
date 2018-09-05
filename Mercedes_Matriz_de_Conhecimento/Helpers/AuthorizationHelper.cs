using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Helpers
{
    public class AuthorizationHelper
    {
        private static HttpContext _context { get { return HttpContext.Current; } }

        private static AutSisWebApiService _autSisService;
        static HttpClient client;

        public AuthorizationHelper()
        {
            _autSisService = new AutSisWebApiService();
            client = new HttpClient();
        }

        public static SistemaApi GetSystem()
        {
            SistemaApi sistema = new SistemaApi();

            //AuthorizationContext aContext = new AuthorizationContext();
            if (GetSession() == null)
                return sistema;

            return JsonConvert.DeserializeObject<SistemaApi>(GetSession());
        }

        public static void SavePermissionSession(SistemaApi permissoes)
        {

            var jsonSystem = JsonConvert.SerializeObject(permissoes, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });

            _context.Session["nomeSession"] = jsonSystem;
            _context.Session.Timeout = 3600;


        }

        public static void DeletePermissionSession()
        {
            _context.Session["nomeSession"] = null;
        }

        public static bool CheckPermission(MenuHelper menu, ScreensHelper screen, FeaturesHelper feature)
        {
            return HaveAccess(GetSystem().FuncionalidadeAutorizacao, menu, screen, feature);
        }

        private static string GetSession()
        {
            //AuthorizationContext aContext = new AuthorizationContext();

            if (_context.Session.Count == 0)
                return null; 

            var strValorSession = _context.Session["nomeSession"].ToString();

            return strValorSession;
        }

        public static bool HaveAccess(FuncionalidadeAutorizacao funcionalidade, MenuHelper menu, ScreensHelper tela, FeaturesHelper acao)
        {

            var menuEncontrado = funcionalidade.Filhos.Where(f => f.Nome.ToString().Replace("\\", "").Replace(" ", "") == menu.ToString()).FirstOrDefault();

            if (menuEncontrado == null)
                return false;

            var telaEncontrada = menuEncontrado.Filhos.Where(f2 => f2.Nome.Replace(" ", "") == tela.ToString()).FirstOrDefault();


            if (telaEncontrada == null)
                return false;

            if (acao == FeaturesHelper.Consultar)
                return true;

            foreach (var filho in telaEncontrada.Filhos)
            {
                if (filho.Nome == acao.ToString())
                    return true;
            }



            return false;
        }
    }
}