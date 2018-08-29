using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Mercedes_Matriz_de_Conhecimento.Helpers;
using System.Web.Security;
using Mercedes_Matriz_de_Conhecimento.Models;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        public BaseController()
        {
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            return base.BeginExecuteCore(callback, state);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var resultadoAutorizacao = CheckUserAuthorization();
            switch (resultadoAutorizacao)
            {
                case TipoResultadoAutorizacao.AUTORIZADO:
                case TipoResultadoAutorizacao.NAO_AUTENTICADO:
                    break;
                case TipoResultadoAutorizacao.USUARIO_INATIVO:
                    TempData["_AuthenticationError"] = "Usuário Inativo";
                    filterContext.Result = new RedirectResult("/Account/UnauthorizedAccess");
                    AuthenticationHelper.LimparRegistroAutenticacao();
                    break;
                case TipoResultadoAutorizacao.USUARIO_NAO_CADASTRADO:
                    TempData["_AuthenticationError"] = "Usuário não cadastrado";
                    filterContext.Result = new RedirectResult("/Account/UnauthorizedAccess");
                    AuthenticationHelper.LimparRegistroAutenticacao();

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



        private TipoResultadoAutorizacao CheckUserAuthorization()
        {

            Account usuario = null;

            bool local = FormsAuthentication.LoginUrl == "/Login/Login" ? true : false;

            if (User.Identity.IsAuthenticated || local)
            {
                var user = AuthenticationHelper.GetCurrentUser();

                if (user == null || (!User.Identity.Name.ToUpper().Equals(user.Username.ToUpper()) && !local))
                {

                    if (!String.IsNullOrEmpty(User.Identity.Name))
                    {
                        usuario = _accountAppService.GetAutsisUser(User.Identity.Name);
                    }
                    else if (user != null)
                    {
                        if (!String.IsNullOrEmpty(user.Username))
                            usuario = _accountAppService.GetAutsisUser(user.Username);
                    }
                    else if (user == null && local)
                    {
                        return TipoResultadoAutorizacao.NAO_AUTENTICADO;
                    }
                    else if (user != null && local)
                    {
                        return TipoResultadoAutorizacao.AUTORIZADO;
                    }

                    if (usuario != null) //Verifica se o usuário existe
                    {

                        AuthenticationHelper.Authenticated(usuario, 360, true);


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