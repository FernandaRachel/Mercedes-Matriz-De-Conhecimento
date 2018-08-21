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
        // GET: Matrix
        public ActionResult Index()
        {
            var WorkzoneID = 1;
            var workzone = _workzone.GetWorkzoneById(WorkzoneID);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();
            //List<tblMatrizFuncXTreinamento> funcXTreinList = new List<tblMatrizFuncXTreinamento>();
            List<tblMatrizFuncXTreinamento> matrizTreinamento = new List<tblMatrizFuncXTreinamento>();
            List<tblMatrizFuncXAtividades> matrizAtividades = new List<tblMatrizFuncXAtividades>();
            tblMatrizWorkzone matriz = new tblMatrizWorkzone();
            List<tblMatrizWorkzone> matrizList = new List<tblMatrizWorkzone>();

            var MatrizWorkzone = _matrizService.GetMatrizByWZId(WorkzoneID);
            tblMatrizFuncXAtividades mAtivividadeObj = new tblMatrizFuncXAtividades();
            tblMatrizFuncXTreinamento mTreinamentoObj = new tblMatrizFuncXTreinamento();

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


            tblMatrizFuncXAtividades atividadeMatriz = new tblMatrizFuncXAtividades();
            tblMatrizFuncXTreinamento treinamentoMatriz = new tblMatrizFuncXTreinamento();

            foreach (var a in activiesList)
            {
                if (MatrizWorkzone.tblMatrizFuncXAtividades.Count > 0)
                {

                    foreach (var aExisting in MatrizWorkzone.tblMatrizFuncXAtividades)
                    {
                        if (aExisting.idAtividade == a.idAtividade)
                        {
                            atividadeMatriz.tblAtividades = aExisting.tblAtividades;

                            matriz.idMatrizWZ = 0;
                            matriz.idWorkzone = WorkzoneID;
                            matriz.tblMatrizFuncXAtividades.Add(atividadeMatriz);
                            atividadeMatriz = new tblMatrizFuncXAtividades();
                        }
                        else
                        {
                            atividadeMatriz.tblAtividades = _activity.GetActivityById(a.idAtividade);

                            matriz.idMatrizWZ = 0;
                            matriz.idWorkzone = WorkzoneID;
                            matriz.tblMatrizFuncXAtividades.Add(atividadeMatriz);
                            atividadeMatriz = new tblMatrizFuncXAtividades();
                        }
                    }
                }
                else
                {
                    atividadeMatriz.tblAtividades = _activity.GetActivityById(a.idAtividade);

                    matriz.idMatrizWZ = 0;
                    matriz.idWorkzone = WorkzoneID;
                    matriz.tblMatrizFuncXAtividades.Add(atividadeMatriz);
                    atividadeMatriz = new tblMatrizFuncXAtividades();
                }
            }

            foreach (var t in trainingList)
            {
                //t.tblMatrizFuncXTreinamento
                if (MatrizWorkzone.tblMatrizFuncXTreinamento.Count > 0)
                {

                    foreach (var tExisting in MatrizWorkzone.tblMatrizFuncXTreinamento)
                    {
                        var exist = matriz.tblMatrizFuncXTreinamento.Where(w => w.tblTreinamento.IdTreinamento == t.IdTreinamento).Count();

                        // Verifica se o Treinamento ja foi adicionado
                        if (exist == 0)
                        {

                            // Verifica se o ID do treinamento existe na MATRIZ
                            if (tExisting.idTreinamento == t.IdTreinamento)
                            {
                                treinamentoMatriz.tblTreinamento = tExisting.tblTreinamento;
                                treinamentoMatriz.idTreinamento = tExisting.idTreinamento;
                                treinamentoMatriz.idItemPerfil = tExisting.idItemPerfil;
                                treinamentoMatriz.tblPerfilItens = tExisting.tblPerfilItens;
                                //added
                                treinamentoMatriz.tblFuncionarios = tExisting.tblFuncionarios;
                                treinamentoMatriz.idFuncionario = tExisting.idFuncionario;

                                matriz.idMatrizWZ = tExisting.idMatrizWorkzone;
                                matriz.idWorkzone = WorkzoneID;
                                matriz.tblMatrizFuncXTreinamento.Add(treinamentoMatriz);
                                treinamentoMatriz = new tblMatrizFuncXTreinamento();
                            }
                            else
                            {
                                var treinamento = _training.GetTrainingById(t.IdTreinamento);
                                treinamentoMatriz.tblTreinamento = treinamento;

                                matriz.idMatrizWZ = 0;
                                matriz.idWorkzone = WorkzoneID;
                                matriz.tblMatrizFuncXTreinamento.Add(treinamentoMatriz);
                                treinamentoMatriz = new tblMatrizFuncXTreinamento();
                            }
                        }
                    }
                }
                else
                {
                    treinamentoMatriz.tblTreinamento = _training.GetTrainingById(t.IdTreinamento);

                    matriz.idMatrizWZ = 0;
                    matriz.idWorkzone = WorkzoneID;
                    matriz.tblMatrizFuncXTreinamento.Add(treinamentoMatriz);
                    treinamentoMatriz = new tblMatrizFuncXTreinamento();
                }

            }


      
            ViewBag.trainingList = trainingList;
            ViewBag.activiesList = activiesList;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();
            ViewBag.Matriz = matriz;

         

            return View(workzone);
        }

        public ActionResult BuscarItemAtiv(int idActivity, int idFuncionario)
        {
            tblMatrizFuncXAtividades matrizFuncXTrain = new tblMatrizFuncXAtividades();
            matrizFuncXTrain.idAtividade = idActivity;
            matrizFuncXTrain.idFuncionario = idFuncionario;

            if (idActivity != 0 && idFuncionario != 0)
            {
                List<tblPerfilItens> itensList = new List<tblPerfilItens>();

                var profileID = _training.GetTrainingById(idActivity).idPerfilTreinamento;
                var itemXProfile = _profileTraining.GetTrainingProfileById(profileID).tblPerfilTreinamentoxPerfilItem;
                var itens = new tblPerfilItens();

                foreach (var iXp in itemXProfile)
                {
                    itens = _profileItemActivity.GetActivityProfileItemById(iXp.IdPerfilItem);
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}