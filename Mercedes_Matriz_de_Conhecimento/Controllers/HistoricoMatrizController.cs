using Mercedes_Matriz_de_Conhecimento.Models;
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
            List<tblFuncionarios> funcList = new List<tblFuncionarios>();
            tblFuncionarios funcObj = new tblFuncionarios();
            AvaliacaoTreinamentoModel avalTrein = new AvaliacaoTreinamentoModel();
            AvaliacaoAtividadesModel avalAtiv = new AvaliacaoAtividadesModel();
            List<AvaliacaoTreinamentoModel> avalTreinList = new List<AvaliacaoTreinamentoModel>();
            List<AvaliacaoAtividadesModel> avalAtivList = new List<AvaliacaoAtividadesModel>();

            foreach (var t in VersaoMatriz.tblMatrizFuncTreinHistorico)
            {
                var aux = t.idTipoTreinamento;
                avalTrein = new AvaliacaoTreinamentoModel();
                avalTrein.idFuncionario = t.idFuncionario;
                avalTrein.idTreinamento = t.idTreinamento;
                avalTrein.sigla = t.siglaItemPerfil;
                avalTreinList.Add(avalTrein);

               

                if (funcList.Where(f => f.idfuncionario == t.idFuncionario).Count() == 0)
                {
                    funcObj = new tblFuncionarios();

                    funcObj.Nome = t.nomeFuncionario;
                    funcObj.idfuncionario = t.idFuncionario;
                    funcObj.RE = t.REFuncionario;
                    funcObj.idBu_Origem = t.BUFuncionario;
                    funcList.Add(funcObj);
                }
                var tObj = new tblTreinamento();
                //Verifica se o treinamento já existe na Lista
                if (trainingList.Exists(t2 => t2.IdTreinamento == t.idTreinamento) == false)
                {
                    var objTipo = new tblTipoTreinamento();
                    objTipo.Nome = t.nomeTipoTreinamento;
                    objTipo.IdTipoTreinamento = t.idTipoTreinamento;

                    tObj.Nome = t.nomeTreinamento;
                    tObj.IdTreinamento = t.idTreinamento;
                    tObj.idTipoTreinamento = t.idTipoTreinamento;
                    //tObj.tblTipoTreinamento = objTipo;
                    //tObj.tblTipoTreinamento.tblTreinamento.Add(tObj);
                    trainingList.Add(tObj);
                }
                var ttObj = new tblTipoTreinamento();

                if (ttList.Exists(t2 => t2.IdTipoTreinamento == t.idTipoTreinamento) == false)
                {
                    ttObj.Nome = t.nomeTipoTreinamento;
                    ttObj.IdTipoTreinamento = t.idTipoTreinamento;
                    ttObj.Sigla = t.siglaTipoTreinamento;
                    ttObj.tblTreinamento = trainingList.Where(t2 => t2.idTipoTreinamento == t.idTipoTreinamento).ToList();
                    ttList.Add(ttObj);
                }
                //else
                //{
                //    ttObj.tblTreinamento = trainingList.Where(t2 => t2.idTipoTreinamento == t.idTipoTreinamento).ToList();
                //    ttList.Add(ttObj);
                //}
            }

            foreach (var a in VersaoMatriz.tblMatrizFuncActivityHistorico)
            {
                avalAtiv = new AvaliacaoAtividadesModel();
                avalAtiv.idFuncionario = a.idFuncionario;
                avalAtiv.idAtividade = a.idAtividade;
                avalAtiv.sigla = a.siglaItemPerfil;
                avalAtiv.cor = a.cor;
                avalAtivList.Add(avalAtiv);


                if (activiesList.Exists(t => t.idAtividade == a.idAtividade) == false)
                {
                    var aObj = new tblAtividades();
                    aObj.Nome = a.nomeAtividade;
                    aObj.idAtividade = a.idAtividade;
                    aObj.Sigla = a.siglaAtividade;

                    activiesList.Add(aObj);
                }

            }


            ViewBag.avalTreinList = avalTreinList;
            ViewBag.avalAtivList = avalAtivList;
            ViewBag.trainingList = trainingList;
            ViewBag.activiesList = activiesList;
            ViewBag.funcionarios = funcList;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();


            return View("MatrizHistorico", VersaoMatriz);
        }
    }
}
