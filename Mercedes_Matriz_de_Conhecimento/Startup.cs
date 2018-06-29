using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mercedes_Matriz_de_Conhecimento.Startup))]
namespace Mercedes_Matriz_de_Conhecimento
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
