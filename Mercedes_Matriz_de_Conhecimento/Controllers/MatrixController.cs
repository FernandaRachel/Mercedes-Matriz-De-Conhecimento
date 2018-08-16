using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class MatrixController : Controller
    {

        private MatrizWorkzoneService _matrizService;
        private WorzoneXActivityService _workzoneXActivity;
        private ActivityXTrainingService _activityXTraining;
        private WorzoneXEmployeeService _workzoneXFunc;
        private WorkzoneService _workzone;
        private ActivityService _activity;
        private TrainingService _training;
        private TrainingTypeService _trainingType;
        private PerfilAtivItemXPerfilItemService _perfilAtivItem;

        public MatrixController()
        {
            _matrizService = new MatrizWorkzoneService();
            _workzoneXActivity = new WorzoneXActivityService();
            _activityXTraining = new ActivityXTrainingService();
            _workzoneXFunc = new WorzoneXEmployeeService();
            _workzone = new WorkzoneService();
            _activity = new ActivityService();
            _training = new TrainingService();
            _trainingType = new TrainingTypeService();
            _perfilAtivItem = new PerfilAtivItemXPerfilItemService();

        }
        // GET: Matrix
        public ActionResult Index()
        {
            var WorkzoneID = 1;
            var workzone = _workzone.GetWorkzoneById(WorkzoneID);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();
            List<tblMatrizFuncXTreinamento> funcXTreinList = new List<tblMatrizFuncXTreinamento>();

            //OBTER TODAS OS TREINAMENTOS DE TODAS ATIVIDADES DA ZONA

            foreach (var aList in activiesList)
            {
                //Pega todos IDs de atividades associados a treinamentos DA ZONA
                foreach (var aXt in aList.tblAtividadeXTreinamentos)
                {
                    var aux = _training.GetTrainingById(aXt.idTreinamento);

                    //Verifica se o treinamento já existe na Lista
                    if (trainingList.Exists(t => t.IdTreinamento == aux.IdTreinamento) == false)
                        trainingList.Add(aux);

                    if (ttList.Exists(t => t.IdTipoTreinamento == aux.tblTipoTreinamento.IdTipoTreinamento) == false)
                        ttList.Add(aux.tblTipoTreinamento);
                }
            }


            ViewBag.trainingList = trainingList.OrderBy(t => t.tblTipoTreinamento.Nome);
            ViewBag.activiesList = activiesList;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();
            ViewBag.funcXTreinList = funcXTreinList;

            foreach (var x in workzone.tblWorkzoneXFuncionario)
            {
                tblMatrizFuncXTreinamento funcXTrein = new tblMatrizFuncXTreinamento();
                funcXTreinList.Add(funcXTrein);
            }



            return View(workzone);
        }

        public ActionResult BuscarITem(int idTrain, int idFuncionario)
        {
            List<tblPerfilAtividadeXPerfilAtItem> perfilItemXPerfil = new List<tblPerfilAtividadeXPerfilAtItem>();
            ViewBag.PerfilITem = perfilItemXPerfil;

            return PartialView("_Modal");
        }
    }
}