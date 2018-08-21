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
                                treinamentoMatriz.tblPerfilItens = tExisting.tblPerfilItens;
                                //added
                                treinamentoMatriz.tblFuncionarios = tExisting.tblFuncionarios;
                                treinamentoMatriz.tblPerfilItens = tExisting.tblPerfilItens;

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


            //ViewBag.trainingList = trainingList.OrderBy(t => t.tblTipoTreinamento.Nome);
            //ViewBag.activiesList = activiesList;
            ViewBag.trainingList = matriz.tblMatrizFuncXTreinamento;
            ViewBag.activiesList = matriz.tblMatrizFuncXAtividades;
            ViewBag.ttList = ttList;
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();
            ViewBag.Matriz = matriz;

            //foreach (var x in workzone.tblWorkzoneXFuncionario)
            //{
            //    tblMatrizFuncXTreinamento funcXTrein = new tblMatrizFuncXTreinamento();
            //    funcXTreinList.Add(funcXTrein);
            //}

            //ViewBag.funcXTreinList = funcXTreinList;

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

            if (MatrizExists == null)
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
    }
}

/*
 
            
     
     */



/*

  var MatrizWorkzone = _matrizService.GetMatrizByWZId(WorkzoneID);
        tblMatrizFuncXAtividades mAtivividadeObj = new tblMatrizFuncXAtividades();
        tblMatrizFuncXTreinamento mTreinamentoObj = new tblMatrizFuncXTreinamento();

        //Pega func[n]
        foreach (var wFunc in workzone.tblWorkzoneXFuncionario)
        {
            //Pega Atividade[n1]
            foreach (var wAtiv in workzone.tblWorkzoneXAtividades)
            {
                //Verifica se func[n] possui a atividade na posição Atividade[n1]
                var funcTemAval = wAtiv.tblAtividades.tblMatrizFuncXAtividades
                    .Where(m => m.idFuncionario == wFunc.idFuncionario);

                //Verifica se funcionário tem avaliação na Atividade[n1] 
                if (funcTemAval.Count() > 0)
                {
                    //funcComAval retorna cada objeto da tabela Funcionario X Atividades
                    foreach (var funcComAval in funcTemAval)
                    {
                        mAtivividadeObj = new tblMatrizFuncXAtividades();
                        mAtivividadeObj.idFuncionario = funcComAval.idFuncionario;
                        mAtivividadeObj.idAtividade = (int)funcComAval.idAtividade;
                        mAtivividadeObj.idItemPerfil = (int)funcComAval.idItemPerfil;
                        mAtivividadeObj.idMatrizWorkzone = funcComAval.idMatrizWorkzone;
                        mAtivividadeObj.tblPerfilItens = funcComAval.tblPerfilItens;
                        mAtivividadeObj.tblFuncionarios = funcComAval.tblFuncionarios;
                        mAtivividadeObj.tblAtividades = funcComAval.tblAtividades;
                        mAtivividadeObj.tblMatrizWorkzone = funcComAval.tblMatrizWorkzone;

                        matrizAtividades.Add(mAtivividadeObj);
                    }
                }
                // Se func[n] não possui avaliação na Atividade[n1] - 
                // cria um mock de func X Ativi s/func adicionado
                else
                {
                    mAtivividadeObj = new tblMatrizFuncXAtividades();
                    mAtivividadeObj.idFuncionario = 0;
                    mAtivividadeObj.idAtividade = (int)wAtiv.idAtividade;
                    mAtivividadeObj.idItemPerfil = 0;
                    mAtivividadeObj.tblPerfilItens = new tblPerfilItens();
                    mAtivividadeObj.tblFuncionarios = new tblFuncionarios();
                    mAtivividadeObj.tblAtividades = wAtiv.tblAtividades;
                    mAtivividadeObj.tblMatrizWorkzone = new tblMatrizWorkzone();

                    matrizAtividades.Add(mAtivividadeObj);
                }
            }
        } // fim foreach Atividade x Func


        //Pega func[n]
        foreach (var wFunc in workzone.tblWorkzoneXFuncionario)
        {

            foreach (var wAtividades in workzone.tblWorkzoneXAtividades)
            {
                //Pega os Treinamentos necessários para tal atividade
                foreach (var ativXTrein in wAtividades.tblAtividades.tblAtividadeXTreinamentos)
                {
                    //Verifica se func[n] possui o treinamento na posição Treinamento[n1]
                    var TreinTemAval = ativXTrein.tblTreinamento.tblMatrizFuncXTreinamento
                    .Where(w => w.idFuncionario == wFunc.idFuncionario);


                    if (TreinTemAval.Count() > 0)
                    {
                        foreach (var funcXTrein in TreinTemAval)
                        {
                            var treinamentoExist = matrizTreinamento.Where(m => m.idTreinamento == funcXTrein.idTreinamento);
                            if (treinamentoExist.Count() == 0)
                            {
                                mTreinamentoObj = new tblMatrizFuncXTreinamento();

                                mTreinamentoObj.idTreinamento = funcXTrein.idTreinamento;
                                mTreinamentoObj.idItemPerfil = funcXTrein.idItemPerfil;
                                mTreinamentoObj.idMatrizWorkzone = funcXTrein.idMatrizWorkzone;
                                mTreinamentoObj.tblPerfilItens = funcXTrein.tblPerfilItens;
                                mTreinamentoObj.tblTreinamento = funcXTrein.tblTreinamento;
                                mTreinamentoObj.tblMatrizWorkzone = funcXTrein.tblMatrizWorkzone;
                                mTreinamentoObj.idMatrizFuncTrein = funcXTrein.idMatrizFuncTrein;

                                matrizTreinamento.Add(mTreinamentoObj);
                            }
                        }
                    }
                    else
                    // Se func[n] não possui avaliação no Treinamento[n1] - 
                    // cria um mock de func X Treinamento s/func adicionado
                    {
                        var treinamentoExist = matrizTreinamento.Where(m => m.idTreinamento == ativXTrein.idTreinamento);
                        if (treinamentoExist.Count() == 0)
                        {
                            mTreinamentoObj = new tblMatrizFuncXTreinamento();

                            mTreinamentoObj.idTreinamento = ativXTrein.idTreinamento;
                            mTreinamentoObj.idItemPerfil = 0;
                            mTreinamentoObj.idMatrizWorkzone = 0;
                            mTreinamentoObj.tblPerfilItens = new tblPerfilItens();
                            mTreinamentoObj.tblTreinamento = ativXTrein.tblTreinamento;
                            mTreinamentoObj.tblMatrizWorkzone = new tblMatrizWorkzone();

                            matrizTreinamento.Add(mTreinamentoObj);
                        }
                    }
                }
            }
        }

        matriz.tblMatrizFuncXTreinamento = matrizTreinamento;
        matriz.tblMatrizFuncXAtividades = matrizAtividades;

        ViewBag.matriz = matriz;
 */
