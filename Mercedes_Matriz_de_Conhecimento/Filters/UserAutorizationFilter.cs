using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Filters
{
    public class UserAutorizationFilter : ActionFilterAttribute, IActionFilter

    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)

        {
            filterContext.Controller.ViewBag.ConteudoAd = GerarConteudoAD();
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            AtualizarExibicaoAD();
        }

        private string GerarConteudoAD()
        {
            return "<hr/><h2>Propaganda Aqui!<h2 /><hr/>";
        }

        private void AtualizarExibicaoAD()
        {
            // Neste método pode ser executada uma atualização
            // do contador de exibições de determinada propaganda.

        }

    }
}