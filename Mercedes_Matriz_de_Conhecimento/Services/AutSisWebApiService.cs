using Mercedes_Matriz_de_Conhecimento.Helpers;
using Mercedes_Matriz_de_Conhecimento.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Services
{

    public class AutSisWebApiService
    {
        static HttpClient client;
        private static HttpContext _context { get { return HttpContext.Current; } }

        public AutSisWebApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<SistemaApi> getPermissions(string usuario)
        {
            var url = ConfigurationManager.AppSettings["AutSisAPI"] + usuario + "/tipochave/0";

            SistemaApi sistemaApi = null;
            
            var builder = new UriBuilder(url);
            string urlAll = builder.ToString();


            //Ignora os valores nulos que vierem no Corpo da Resposta
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var result = await client.GetAsync(urlAll);

            if (result.StatusCode == HttpStatusCode.OK && result.Content != null)
            {
                var returnAPI = await client.GetStringAsync(urlAll);
                sistemaApi = JsonConvert.DeserializeObject<SistemaApi>(returnAPI, settings);

                // cria a sessão
                //AuthorizationHelper.SavePermissionSession(sistemaApi);
            }
            else
            {
                Console.WriteLine("chora");
            }

           
            return sistemaApi;


        }
    }
}