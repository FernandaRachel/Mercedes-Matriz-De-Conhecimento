using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class HistoricoMatrizController : Controller
    {

        private MatrizHistoricoService _matrizHistoricoService;

        public HistoricoMatrizController()
        {
            _matrizHistoricoService = new MatrizHistoricoService();
        }

        // GET: HistoricoMatriz
        public ActionResult Index()
        {
            ViewBag.ListaHistorico = _matrizHistoricoService.GetMatrizHistorico();

            return View();
        }


        public ActionResult MatrizHistorico(int idMatrizHistorico)
        {
            var VersaoMatriz = _matrizHistoricoService.GetMatrizHistoricoById(idMatrizHistorico);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblAtividades> activiesList = new List<tblAtividades>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();


            foreach (var t in VersaoMatriz.tblMatrizFuncTreinHistorico)
            {
                var aux = t.idTipoTreinamento;

                //Verifica se o treinamento já existe na Lista
                if (trainingList.Exists(t2 => t2.IdTreinamento == t.idTreinamento) == false)
                {
                    var tObj = new tblTreinamento();
                    tObj.Nome = t.nomeTipoTreinamento;
                    tObj.IdTreinamento = t.idTreinamento;
                    tObj.idTipoTreinamento = t.idTipoTreinamento;
                    trainingList.Add(tObj);
                }

                if (ttList.Exists(t2 => t2.IdTipoTreinamento == t.idTipoTreinamento) == false)
                {
                    var ttObj = new tblTipoTreinamento();
                    ttObj.Nome = t.nomeTipoTreinamento;
                    ttObj.IdTipoTreinamento = t.idTipoTreinamento;
                    ttObj.Sigla = t.siglaTipoTreinamento;

                    ttList.Add(ttObj);
                }
            }

            foreach (var a in VersaoMatriz.tblMatrizFuncActivityHistorico)
            {
                if (activiesList.Exists(t => t.idAtividade == a.idAtividade) == false)
                {
                    var aObj = new tblAtividades();
                    aObj.Nome = a.nomeAtividade;
                    aObj.idAtividade = a.idAtividade;
                    aObj.Sigla = a.siglaAtividade;

                    activiesList.Add(aObj);
                }

            }

            ViewBag.trainingList = trainingList;
            ViewBag.activiesList = activiesList;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();


            return View("MatrizHistorico", VersaoMatriz);
        }
    }
}
