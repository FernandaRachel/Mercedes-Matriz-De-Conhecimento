using log4net;
using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mercedes_Matriz_de_Conhecimento.Helpers
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public Account.AccessLevels AccessLevel { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        private bool _authenticated;
        private bool _authorized;
        private bool _Json;
        private static HttpContext _context { get { return HttpContext.Current; } }
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private AutSisWebApiService _autsisService;


        public LoginAuthorizeAttribute()
        {
            _autsisService = new AutSisWebApiService();
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            log.Debug("Sem autorização");

            base.HandleUnauthorizedRequest(filterContext);

            _Json = filterContext.HttpContext.Request.IsAjaxRequest();

            if (!_authorized)
                if (_authenticated || filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("Account/HandleUnauthorizedAccess");
                }
                else
                {
                    string url = $"{FormsAuthentication.LoginUrl}?returnUrl=/{filterContext.RouteData.Values["controller"]}/{filterContext.RouteData.Values["action"]}";
                    filterContext.Result = new RedirectResult(url);
                }
            else
            {

                if (!_Json)
                {
                    filterContext.Result = new RedirectResult("Account/HandleUnauthorizedAccess");
                }
                else
                {
                    var viewResult = new JsonResult();
                    viewResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    viewResult.Data = (new { error = "errAccess" });
                    filterContext.Result = viewResult;
                }
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {

                _authenticated = base.AuthorizeCore(httpContext);


                bool local = FormsAuthentication.LoginUrl == "/Account/Login" ? true : false;

                var usr = AuthorizationHelper.GetSystem();

                if (_authenticated || (usr.Nome != null && local))
                {
                    _authenticated = true;

                    var request = new UserRequest(httpContext);


                    _authorized = true;


                }
                else
                {
                    var resultadoAutorizacao = Task.Run(async () => await _autsisService.getPermissions(httpContext.User.Identity.Name)).Result;

                    if (resultadoAutorizacao != null)
                    {
                        _authenticated = true;
                        _authorized = true;
                    }
                    else
                    {
                        _authenticated = false;
                        _authorized = false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return _authorized && _authenticated;
        }
        private class UserRequest
        {
            public string Controller { get; private set; }
            public string Action { get; private set; }
            public UserRequest(HttpContextBase httpContext)
            {
                var url = httpContext.Request.RawUrl.Split('/');
                if (url.Count() > 0)
                {
                    this.Controller = url[1];
                    if (url.Count() > 2)
                        this.Action = url[2].Split('?')[0];
                }
            }
        }
    }
}