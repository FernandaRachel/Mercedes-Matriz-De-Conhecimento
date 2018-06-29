using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
