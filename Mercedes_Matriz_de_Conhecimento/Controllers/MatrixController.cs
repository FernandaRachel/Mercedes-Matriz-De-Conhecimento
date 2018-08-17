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
        private MatrizFuncTreinamentoService _matrizFuncTrainingService;
        private WorzoneXActivityService _workzoneXActivity;
        private ActivityXTrainingService _activityXTraining;
        private WorzoneXEmployeeService _workzoneXFunc;
        private WorkzoneService _workzone;
        private ActivityService _activity;
        private TrainingService _training;
        private TrainingTypeService _trainingType;
        private PerfilAtivItemXPerfilItemService _perfilAtivXperfilItem;
        private TrainingProfileService _profileTraining;
        private ActivityProfileService _profileActivity;
        private ActivityProfileItemService _profileItemActivity;
        private ProfileItemService _profileItemTraining;

        public MatrixController()
        {
            _matrizService = new MatrizWorkzoneService();
            _matrizFuncTrainingService = new MatrizFuncTreinamentoService();
            _workzoneXActivity = new WorzoneXActivityService();
            _activityXTraining = new ActivityXTrainingService();
            _workzoneXFunc = new WorzoneXEmployeeService();
            _perfilAtivXperfilItem = new PerfilAtivItemXPerfilItemService();
            _workzone = new WorkzoneService();
            _activity = new ActivityService();
            _training = new TrainingService();
            _trainingType = new TrainingTypeService();
            _profileTraining = new TrainingProfileService();
            _profileActivity = new ActivityProfileService();
            _profileItemActivity = new ActivityProfileItemService();
            _profileItemTraining = new ProfileItemService();

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

            foreach (var x in workzone.tblWorkzoneXFuncionario)
            {
                tblMatrizFuncXTreinamento funcXTrein = new tblMatrizFuncXTreinamento();
                funcXTreinList.Add(funcXTrein);
            }

            ViewBag.funcXTreinList = funcXTreinList;



            return View(workzone);
        }

        public ActionResult BuscarITem(int idTrain, int idFuncionario)
        {
            tblMatrizFuncXTreinamento matrizFuncXTrain = new tblMatrizFuncXTreinamento();
            matrizFuncXTrain.idTreinamento = idTrain;
            matrizFuncXTrain.idFuncionario = idFuncionario;

            if (idTrain != 0 && idFuncionario != 0)
            {
                List<tblPerfilItens> itensList = new List<tblPerfilItens>();
                List<tblPerfilAtividadeXPerfilAtItem> perfilItemXPerfil = new List<tblPerfilAtividadeXPerfilAtItem>();
               

                var profileID = _training.GetTrainingById(idTrain).idPerfilTreinamento;
                var itemXProfile = _profileTraining.GetTrainingProfileById(profileID).tblPerfilTreinamentoxPerfilItem;
                var itens = new tblPerfilItens();

                foreach (var iXp in itemXProfile)
                {
                    itens = _profileItemTraining.GetProfileItemById(iXp.IdPerfilItem);
                    itensList.Add(itens);
                }

                ViewBag.PerfilITem = itensList;
            }
            else
            {
                ViewBag.PerfilITem = new List<tblPerfilItens>();
            }


            return PartialView("_Modal", matrizFuncXTrain);
        }

        public ActionResult CreateMatrizTraining(int idItem, int idFuncionario, int idTraining, int idWorkzone)
        {
            tblMatrizWorkzone matrizXworzone = new tblMatrizWorkzone();
            matrizXworzone.Usuario = "Teste s/ Seg";
            matrizXworzone.DataCriacao = DateTime.Now;

            var idMatriz = _matrizService.CreateMatriz(matrizXworzone).idMatrizWZ;
            var WorkzoneID = 1;
            tblMatrizFuncXTreinamento matrizFuncTrain = new tblMatrizFuncXTreinamento();
            matrizFuncTrain.idFuncionario = idFuncionario;
            matrizFuncTrain.idItemPerfil = idItem;
            matrizFuncTrain.idMatrizWorkzone = idMatriz;
            matrizFuncTrain.idTreinamento = idTraining;

            _matrizFuncTrainingService.CreateMatriz(matrizFuncTrain);

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idTraining: {0}, idItem: {1}, idFuncionario: {2}", idTraining, idItem, idFuncionario);

            return RedirectToAction("BuscarITem", new { idTrain = 0, idFuncionario = 0 });
        }
    }
}