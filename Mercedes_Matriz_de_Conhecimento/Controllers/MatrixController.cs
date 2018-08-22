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
        private MatrizFuncAtividadeService _matrizFuncActivityService;
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
            _matrizFuncActivityService = new MatrizFuncAtividadeService();
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

        public ActionResult Index()
        {
            IEnumerable<tblWorkzone> workzones;
            workzones = _workzone.GetWorkzones();
            ViewData["Workzones"] = workzones;

            return View();
        }

        // GET: Matrix
        public ActionResult Matriz(int WorkzoneID)
        {
            var exits = _matrizService.GetMatrizByWZId(WorkzoneID);
            var matrizWz = new tblMatrizWorkzone();
            matrizWz.idMatrizWZ = 0;

            if (exits == null)
            {
                tblMatrizWorkzone matrizXworzone = new tblMatrizWorkzone();
                matrizXworzone.Usuario = "Teste s/ Seg";
                matrizXworzone.DataCriacao = DateTime.Now;
                matrizXworzone.idWorkzone = WorkzoneID;

                //Se a wzWZ já existe ele retorna a WZ
                matrizWz = _matrizService.CreateMatriz(matrizXworzone);

            }
            else
            {
                matrizWz = exits;
            }
            var workzone = _workzone.GetWorkzoneById(WorkzoneID);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();

            var MatrizWorkzone = _matrizService.GetMatrizByWZId(WorkzoneID);
            tblMatrizFuncXAtividades mAtivividadeObj = new tblMatrizFuncXAtividades();
            tblMatrizFuncXTreinamento mTreinamentoObj = new tblMatrizFuncXTreinamento();

            //OBTER TODAS OS TREINAMENTOS DE TODAS ATIVIDADES DA ZONA
            // E OS SEUS TIPOS
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



            ViewBag.trainingList = trainingList;
            ViewBag.activiesList = activiesList;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();

            return View("Matriz", workzone);
        }

        public ActionResult BuscarItemAtiv(int idActivity, int idFuncionario)
        {
            tblMatrizFuncXAtividades matrizFuncXTrain = new tblMatrizFuncXAtividades();
            matrizFuncXTrain.idAtividade = idActivity;
            matrizFuncXTrain.idFuncionario = idFuncionario;

            if (idActivity != 0 && idFuncionario != 0)
            {
                List<tblPerfilItens> itensList = new List<tblPerfilItens>();

                var itemXProfile = _activity.GetActivityById(idActivity).tblPerfis.tblPerfilAtividadeXPerfilAtItem;
                var itens = new tblPerfilItens();

                foreach (var iXp in itemXProfile)
                {
                    itens = _profileItemActivity.GetActivityProfileItemById(iXp.idPerfilAtivItem);
                    itensList.Add(itens);
                }

                ViewBag.PerfilITem = itensList;
            }
            else
            {
                ViewBag.PerfilITem = new List<tblPerfilItens>();
            }


            return PartialView("_ModalActivity", matrizFuncXTrain);
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


            return PartialView("_ModalTrein", matrizFuncXTrain);
        }

        public ActionResult CreateMatrizTraining(int idItem, int idFuncionario, int idTraining, int idWorkzone)
        {
            tblMatrizWorkzone matrizXworzone = new tblMatrizWorkzone();
            matrizXworzone.Usuario = "Teste s/ Seg";
            matrizXworzone.DataCriacao = DateTime.Now;

            var MatrizExists = _matrizService.GetMatrizByWZId(idWorkzone);
            var idMatriz = 0;
            var avaliationExist = _matrizFuncTrainingService.GetMatrizByFuncXTrain(idTraining, idFuncionario);

            if (MatrizExists == null)
                idMatriz = _matrizService.CreateMatriz(matrizXworzone).idMatrizWZ;

            else
                idMatriz = MatrizExists.idMatrizWZ;


            var WorkzoneID = idWorkzone;
            tblMatrizFuncXTreinamento matrizFuncTrain = new tblMatrizFuncXTreinamento();
            matrizFuncTrain.idFuncionario = idFuncionario;
            matrizFuncTrain.idItemPerfil = idItem;
            matrizFuncTrain.idMatrizWorkzone = idMatriz;
            matrizFuncTrain.idTreinamento = idTraining;

            if (avaliationExist == null)
                _matrizFuncTrainingService.CreateMatriz(matrizFuncTrain);

            else
            {
                matrizFuncTrain.idMatrizFuncTrein = avaliationExist.idMatrizFuncTrein;
                _matrizFuncTrainingService.UpdateMatriz(matrizFuncTrain);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idTraining: {0}, idItem: {1}, idFuncionario: {2}", idTraining, idItem, idFuncionario);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }

        public ActionResult CreateMatrizActivity(int idItem, int idFuncionario, int idActivity, int idWorkzone)
        {
            tblMatrizWorkzone matrizXworzone = new tblMatrizWorkzone();
            matrizXworzone.Usuario = "Teste s/ Seg";
            matrizXworzone.DataCriacao = DateTime.Now;

            var MatrizExists = _matrizService.GetMatrizByWZId(idWorkzone);
            var idMatriz = 0;
            var avaliationExist = _matrizFuncActivityService.GetMatrizByFuncXAtiv(idActivity, idFuncionario);

            if (MatrizExists == null)
                idMatriz = _matrizService.CreateMatriz(matrizXworzone).idMatrizWZ;

            else
                idMatriz = MatrizExists.idMatrizWZ;


            var WorkzoneID = idWorkzone;
            tblMatrizFuncXAtividades matrizFuncActiv = new tblMatrizFuncXAtividades();
            matrizFuncActiv.idFuncionario = idFuncionario;
            matrizFuncActiv.idItemPerfil = idItem;
            matrizFuncActiv.idMatrizWorkzone = idMatriz;
            matrizFuncActiv.idAtividade = idActivity;

            if (avaliationExist == null)
                _matrizFuncActivityService.CreateMatriz(matrizFuncActiv);

            else
            {
                matrizFuncActiv.idMatrizFuncAtiv = avaliationExist.idMatrizFuncAtiv;
                _matrizFuncActivityService.UpdateMatriz(matrizFuncActiv);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idActivity: {0}, idItem: {1}, idFuncionario: {2}", idActivity, idItem, idFuncionario);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }


        public ActionResult DeleteMatrizTraining( int idFuncionario, int idTraining, int idWorkzone)
        {

            var mWz = _matrizService.GetMatrizByWZId(idWorkzone);

            _matrizFuncTrainingService.DeleteMatriz(mWz.idMatrizWZ, idFuncionario, idTraining);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }

        public ActionResult DeleteMatrizActivity(int idFuncionario, int idActivity, int idWorkzone)
        {
            var mWz = _matrizService.GetMatrizByWZId(idWorkzone);

            _matrizFuncActivityService.DeleteMatriz(mWz.idMatrizWZ, idFuncionario, idActivity);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }


    }
}