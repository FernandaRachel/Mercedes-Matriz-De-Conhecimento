using Mercedes_Matriz_de_Conhecimento.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Helpers
{
    public class AccessHelper : AuthorizeAttribute
    {

        private bool authenticated;
        private bool authorized;

        public MenuHelper Menu { get; set; }
        public ScreensHelper Screen { get; set; }
        public FeaturesHelper Feature { get; set; }
        public bool Authenticated { get; }
        public bool Authorized { get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //authenticated = base.AuthorizeCore(httpContext);

            var sistema = AuthorizationHelper.GetSystem();

            if(sistema != null)
            {
                authenticated = true;
            }
            if (authenticated)
            {
                //if (sistema.Perfil.Nome.Equals("Master"))
                //    return authorized = true;

                return AuthorizationHelper.HaveAccess(sistema.FuncionalidadeAutorizacao, Menu, Screen, Feature);
            }

            return authorized = false;
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (authenticated && !authorized)
            {
                filterContext.HttpContext.Response.Write("<script>alert('Você não possui acesso a esta tela ou função');</script>");
                filterContext.Result = new RedirectResult("/");
            }
        }

    }
}