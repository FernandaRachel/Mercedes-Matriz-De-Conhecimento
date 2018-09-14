using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Mercedes_Matriz_de_Conhecimento.Helpers;
using System.Web.Security;
using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    [LoginAuthorize]
    public class BaseController : Controller
    {

        private AutSisWebApiService _autsisService;

        public BaseController()
        {
            _autsisService = new AutSisWebApiService();
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            return base.BeginExecuteCore(callback, state);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var resultadoAutorizacao =  CheckUserAuthorization();
            switch (resultadoAutorizacao)
            {
                case TipoResultadoAutorizacao.AUTORIZADO:
                    break;
                case TipoResultadoAutorizacao.NAO_AUTENTICADO:
                    string url = $"{FormsAuthentication.LoginUrl}?returnUrl=/{filterContext.RouteData.Values["controller"]}/{filterContext.RouteData.Values["action"]}";
                    filterContext.Result = new RedirectResult(url);
                    break;
                case TipoResultadoAutorizacao.USUARIO_INATIVO:
                    TempData["_AuthenticationError"] = "Usuário Inativo";
                    filterContext.Result = new RedirectResult("/Login/UnauthorizedAccess");
                    AuthorizationHelper.LimparRegistroAutenticacao();
                    break;
                case TipoResultadoAutorizacao.USUARIO_NAO_CADASTRADO:
                    TempData["_AuthenticationError"] = "Usuário não cadastrado";
                    filterContext.Result = new RedirectResult("/Login/UnauthorizedAccess");
                    AuthorizationHelper.LimparRegistroAutenticacao();

                    break;
                default:
                    break;
            }

            base.OnActionExecuting(filterContext);
        }

        enum TipoResultadoAutorizacao
        {
            AUTORIZADO = 0,
            NAO_AUTENTICADO = 1,
            USUARIO_INATIVO = 2,
            USUARIO_NAO_CADASTRADO = 3
        }



        private  TipoResultadoAutorizacao CheckUserAuthorization()
        {
            SistemaApi usuarioPermissions = null;


            bool local = FormsAuthentication.LoginUrl == "/Account/Login" ? true : false;


            // Verifica se o usuário do SGP esta autenticado OU se você está rodando o sistema local
            if (User.Identity.IsAuthenticated || local)
            {
                string user = null;
                try
                {
                    user = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
                }
                catch
                {
                    user = null;
                }

                // Verifica se o usuário logado no sgp não é o mesmo que esta na sessão
                if (user == null || (!User.Identity.Name.ToUpper().Equals(user.ToUpper()) && !local) )
                {

                    // Verifica se existe usário do SGP logado
                    if (!String.IsNullOrEmpty(User.Identity.Name))
                    {
                        string username = User.Identity.Name.ToString();
                        usuarioPermissions = Task.Run(async () => await _autsisService.getPermissions(username)).Result;
                    }

                    else if (user != null)
                    {
                        if (!String.IsNullOrEmpty(user))
                            usuarioPermissions = Task.Run(async () => await _autsisService.getPermissions(user)).Result;
                    }

                    else if (user == null && local)
                    {

                        return TipoResultadoAutorizacao.NAO_AUTENTICADO;
                    }

                    else if (user != null && local)
                    {

                        return TipoResultadoAutorizacao.AUTORIZADO;
                    }


                    if (usuarioPermissions != null) //Verifica se o usuário existe
                    {
                        AuthorizationHelper.SavePermissionSession(usuarioPermissions);

                        return TipoResultadoAutorizacao.AUTORIZADO;
                    }

                    return TipoResultadoAutorizacao.USUARIO_NAO_CADASTRADO;

                }

                return TipoResultadoAutorizacao.AUTORIZADO;
            }

            return TipoResultadoAutorizacao.NAO_AUTENTICADO;
        }



        public void SetMessageSuccess(string message)
        {
            if (TempData["_Success"] == null)
            {
                TempData.Add("_Success", message);
            }
            else
            {
                TempData["_Success"] = message;
            }
        }

        public void SetPopupMessageSuccess(string message)
        {
            if (TempData["_PopupSuccess"] == null)
            {
                TempData.Add("_PopupSuccess", message);
            }
            else
            {
                TempData["_PopupSuccess"] = message;
            }
        }

        public void SetMessageError(string message)
        {
            if (TempData["_Error"] == null)
            {
                TempData.Add("_Error", message);
            }
            else
            {
                TempData["_Error"] = message;
            }
        }

        public ActionResult SetLoginError()
        {
            return View("_Error");
        }

        public ActionResult AcessoNaoAutorizado()
        {
            return View("UnauthorizedAccess", "Account");
        }

        /// <summary>
        /// Renderiza uma view para caso necessite retornar uma view dentro do JSON
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            // assign the model of the controller from which this method was called to the instance of the passed controller (a new instance, by the way)
            controller.ViewData.Model = model;

            // initialize a string builder
            using (StringWriter sw = new StringWriter())
            {
                // find and load the view or partial view, pass it through the controller factory
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                // render it
                viewResult.View.Render(viewContext, sw);

                //return the razorized view/partial-view as a string
                return sw.ToString();
            }
        }

        protected HttpCookie setCookie(string cookieName, object value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);

            if (value != null)
            {
                cookie.Value = value.ToString();
                Response.Cookies.Add(cookie);
            }
            return cookie;
        }

        protected HttpCookie getCookie(string cookieName)
        {
            if (Request.Cookies.AllKeys.Contains(cookieName))
                return ControllerContext.HttpContext.Request.Cookies[cookieName];

            return null;
        }

        protected void removeCookie(string cookieName)
        {
            if (Request.Cookies.AllKeys.Contains(cookieName))
            {
                HttpCookie cookie = Request.Cookies[cookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }

    }
}