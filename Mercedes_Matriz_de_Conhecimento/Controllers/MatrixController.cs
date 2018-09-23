using log4net;
using Mercedes_Matriz_de_Conhecimento.Helpers;
using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class MatrixController : BaseController
    {

        private MatrizHistoricoService _matrizHistoricoService;
        private MatrizWorkzoneService _matrizService;
        private MatrizFuncTreinamentoService _matrizFuncTrainingService;
        private MatrizFuncAtividadeService _matrizFuncActivityService;
        private MatrizWorkzoneTempService _matrizTempService;
        private MatrizFuncTreinamentoTempService _matrizFuncTrainingTempService;
        private MatrizFuncAtividadeTempService _matrizFuncActivityTempService;
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
        private ActivityGroupService _activityGroup;
        private TrainingGroupService _trainingGroup;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MatrixController()
        {
            log.Debug("Matrix Controller called");

            // HISTÓRICO
            _matrizHistoricoService = new MatrizHistoricoService();
            // SERVIÇOS REFERENTES A MATRIZ OFICIAL
            _matrizService = new MatrizWorkzoneService();
            _matrizFuncTrainingService = new MatrizFuncTreinamentoService();
            _matrizFuncActivityService = new MatrizFuncAtividadeService();
            // SERVIÇOS REFERENTES A MATRIZ TEMPORÁRIA
            _matrizTempService = new MatrizWorkzoneTempService();
            _matrizFuncTrainingTempService = new MatrizFuncTreinamentoTempService();
            _matrizFuncActivityTempService = new MatrizFuncAtividadeTempService();
            // FIM
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
            _activityGroup = new ActivityGroupService();
            _trainingGroup = new TrainingGroupService();

            //Pega o nome do usuário para exibir na barra de navegação
            SistemaApi username = new SistemaApi();

            SetImage();
        }


        public void SetImage()
        {
            //Pega o nome do usuário para exibir na barra de navegação
            SistemaApi username = new SistemaApi();

            try
            {

                username = AuthorizationHelper.GetSystem();
                ViewBag.User = username.Usuario.ChaveAmericas;
                if (username != null)
                {
                    var imgUser = AuthorizationHelper.GetUserImage(username.Usuario.ChaveAmericas);
                    ViewBag.UserPhoto = imgUser;
                }
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());

                var imgUser = AuthorizationHelper.GetUserImage("");

                ViewBag.User = "";
                ViewBag.UserPhoto = imgUser;
            }
        }


        public bool AllowCC(string CC)
        {
            var allowed = false;

            try
            {
                var grupo = AuthorizationHelper.GetSystem()
                    .GruposDeSistema.Find(g => g.Nome == "Grupo_CentroCusto_Linha");


                foreach (var bu in grupo.Funcionalidades.Filhos)
                {
                    foreach (var cc in bu.Filhos)
                    {

                        if (CC.Replace(" ", "").Equals(CC.Replace(" ", "")))
                            allowed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());

                allowed = false;
            }

            return allowed;
        }

        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index()
        {
            IEnumerable<tblWorkzone> workzones;
            workzones = _workzone.GetWorkzones();
            ViewData["Workzones"] = workzones;

            return View();
        }

        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Consultar)]
        public ActionResult Matriz(int WorkzoneID, bool catchValueFromOficial = true)
        {
            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            try
            {

                //Veriica se o cara possui permissão para acessar a matriz de acordo com o CC
                if (!AllowCC(workzone.idCC))
                    return RedirectToAction("Index");

                SetImage();

                var exits = _matrizService.GetMatrizByWZId(WorkzoneID);
                var matrizWz = new tblMatrizWorkzone();
                var matrizWzTemp = new tblMatrizWorkzoneTemp();
                matrizWz.idMatrizWZ = 0;
                matrizWzTemp.idMatrizWZTemp = 0;

                //VERIFICA SE A MATRIZ TEMPORÁRIA ESTÁ LIMPA
                var matrizTemp = _matrizTempService.GetMatrizTempByWZId(WorkzoneID);
                if (matrizTemp != null)
                {
                    _matrizFuncActivityTempService.DeleteMatrizTempAll(matrizTemp.idMatrizWZTemp);
                    _matrizFuncTrainingTempService.DeleteMatrizTempAll(matrizTemp.idMatrizWZTemp);
                    _matrizTempService.DeleteMatrizTemp(matrizTemp.idMatrizWZTemp);

                }


                if (exits == null)
                {
                    var username = "";
                    try
                    {
                        username = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
                    }
                    catch (Exception ex)
                    {
                        username = "";
                    }
                    // CRIA MATRIZ OFICIAL SE ELA N EXISTIR
                    tblMatrizWorkzone matrizXworzoneTemp = new tblMatrizWorkzone();
                    matrizXworzoneTemp.Usuario = username;
                    matrizXworzoneTemp.DataCriacao = DateTime.Now;
                    matrizXworzoneTemp.idWorkzone = WorkzoneID;
                    matrizWz = _matrizService.CreateMatriz(matrizXworzoneTemp);

                }
                else
                {
                    matrizWz = exits;
                }
                var exitsMTemp = _matrizTempService.GetMatrizTempByWZId(WorkzoneID);

                if (exitsMTemp == null)
                {
                    var username = "";
                    try
                    {
                        username = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
                    }
                    catch (Exception ex)
                    {
                        username = "";
                    }

                    // CRIA MATRIZ TEMPORÁRIA ONDE A EDIÇÃO SERÁ FEITA
                    tblMatrizWorkzoneTemp matrizXworzoneTempTemp = new tblMatrizWorkzoneTemp();
                    matrizXworzoneTempTemp.Usuario = username;
                    matrizXworzoneTempTemp.DataCriacao = DateTime.Now;
                    matrizXworzoneTempTemp.idWorkzone = WorkzoneID;
                    matrizWzTemp = _matrizTempService.CreateMatrizTemp(matrizXworzoneTempTemp);
                    exitsMTemp = _matrizTempService.GetMatrizTempByWZId(WorkzoneID);

                    // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM ATIVIDADE
                    var avalAtiv = _matrizFuncActivityService.GetMatrizByMWZId(matrizWz.idMatrizWZ);
                    var idsActv = new List<int>();

                    if (avalAtiv.Count() > 0)
                    {
                        tblMatrizFuncXAtividadesTemp newAvalObj = new tblMatrizFuncXAtividadesTemp();
                        var existInActivityAssociation = _workzoneXActivity.GetWorzoneXActivityListByIdWz(matrizWz.idWorkzone);

                        foreach (var aval in avalAtiv)
                        {
                            if (existInActivityAssociation.Where(a => a.tblAtividades.idAtividade == aval.idAtividade) != null)
                            {
                                newAvalObj = new tblMatrizFuncXAtividadesTemp();

                                newAvalObj.idAtividade = aval.idAtividade;
                                newAvalObj.idFuncionario = aval.idFuncionario;
                                newAvalObj.idItemPerfil = aval.idItemPerfil;
                                newAvalObj.idMatrizWorkzoneTemp = matrizWzTemp.idMatrizWZTemp;

                                _matrizFuncActivityTempService.CreateMatrizTemp(newAvalObj);
                            }
                            else
                            {
                                idsActv.Add(aval.idAtividade);
                            }
                        }
                    }


                    // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM TREINAMENTOS
                    var avalTrein = _matrizFuncTrainingService.GetMatrizByIdMWZ(matrizWz.idMatrizWZ);

                    if (avalTrein.Count() > 0)
                    {
                        tblMatrizFuncXTreinamentoTemp newTreinObj = new tblMatrizFuncXTreinamentoTemp();


                        foreach (var aval in avalTrein)
                        {
                            var training = _training.GetTrainingById(aval.idTreinamento);
                            var existTrainingInAssociation = false;

                            //VERIFICA SE AQUELE TREINAMENTO[N] QUE POSSUI AVALIAÇÃO EXISTE AINDA NA ASSOCIAÇÃO
                            // OU SE FOI DESASSOCIADO
                            foreach (var id in idsActv)
                            {
                                var returned = training.tblAtividadeXTreinamentos.Where(a => a.tblAtividades.idAtividade == id);
                                if (returned.Count() > 0)
                                {
                                    existTrainingInAssociation = true;
                                }
                            }

                            if (existTrainingInAssociation)
                            {

                                newTreinObj = new tblMatrizFuncXTreinamentoTemp();

                                newTreinObj.idTreinamento = aval.idTreinamento;
                                newTreinObj.idFuncionario = aval.idFuncionario;
                                newTreinObj.idItemPerfil = aval.idItemPerfil;
                                newTreinObj.idMatrizWorkzoneTemp = matrizWzTemp.idMatrizWZTemp;

                                _matrizFuncTrainingTempService.CreateMatrizTemp(newTreinObj);
                            }
                        }
                    }

                }

                var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
                List<tblTreinamento> trainingList = new List<tblTreinamento>();
                List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();


                // OBTER TODAS OS TREINAMENTOS DE TODAS ATIVIDADES DA ZONA
                // E OS SEUS TIPOS
                foreach (var aList in activiesList)
                {
                    //Pega todos IDs de atividades associados a treinamentos DA ZONA
                    foreach (var aXt in aList.tblAtividadeXTreinamentos.OrderBy(t => t.tblTreinamento.idTipoTreinamento))
                    {
                        var x = aList.tblAtividadeXTreinamentos.OrderBy(t => t.tblTreinamento.idTipoTreinamento);

                        var aux = _training.GetTrainingById(aXt.idTreinamento);


                        //Verifica se o treinamento já existe na Lista
                        if (trainingList.Exists(t => t.IdTreinamento == aux.IdTreinamento) == false)
                            trainingList.Add(aux);

                        if (ttList.Exists(t => t.IdTipoTreinamento == aux.tblTipoTreinamento.IdTipoTreinamento) == false)
                            ttList.Add(aux.tblTipoTreinamento);

                    }
                }



                ViewBag.trainingList = trainingList.OrderBy(t => t.idTipoTreinamento);
                ViewBag.activiesList = activiesList;
                ViewBag.ttList = ttList.OrderBy(t => t.IdTipoTreinamento);
                ViewBag.tListCount = trainingList.Count();
                ViewBag.activiesCount = activiesList.Count();
                ViewBag.ttListCount = ttList.Count();
                ViewBag.MWZID = matrizWz.idMatrizWZ;
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message.ToString());
            }

            return View("Matriz", workzone);
        }


        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Consultar)]
        public ActionResult MatrizTemp(int WorkzoneID)
        {
            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            //Veriica se o cara possui permissão para acessar a matriz de acordo com o CC
            if (!AllowCC(workzone.idCC))
                return RedirectToAction("Index");

            var exits = _matrizService.GetMatrizByWZId(WorkzoneID);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();
            List<tblGrupoTreinamentos> trainingGroup = new List<tblGrupoTreinamentos>();


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


            ViewBag.trainingList = trainingList.OrderBy(t => t.idTipoTreinamento);
            ViewBag.activiesList = activiesList;
            ViewBag.ttList = ttList.OrderBy(t => t.IdTipoTreinamento);
            ViewBag.tListCount = trainingList.Count();
            ViewBag.activiesCount = activiesList.Count();
            ViewBag.ttListCount = ttList.Count();
            ViewBag.trainingGroup = trainingGroup;

            return View("MatrizTemp", workzone);
        }
        public ActionResult BuscarItemAtiv(int idActivity, int idFuncionario)
        {
            tblMatrizFuncXAtividadesTemp matrizFuncXTrainTemp = new tblMatrizFuncXAtividadesTemp();
            matrizFuncXTrainTemp.idAtividade = idActivity;
            matrizFuncXTrainTemp.idFuncionario = idFuncionario;

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


            return PartialView("_ModalActivity", matrizFuncXTrainTemp);
        }

        public ActionResult BuscarITem(int idTrain, int idFuncionario)
        {
            tblMatrizFuncXTreinamentoTemp matrizFuncXTrainTemp = new tblMatrizFuncXTreinamentoTemp();
            matrizFuncXTrainTemp.idTreinamento = idTrain;
            matrizFuncXTrainTemp.idFuncionario = idFuncionario;

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

            return PartialView("_ModalTrein", matrizFuncXTrainTemp);
        }

        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Editar)]
        public ActionResult CreateMatrizTraining(int idFuncionario, int idTraining, int idWorkzone, int idItem = 0)
        {
            if (idItem == 0)
            {

                return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
            }

            var username = "";
            try
            {
                username = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
            }
            catch (Exception ex)
            {
                username = "";
            }

            tblMatrizWorkzoneTemp matrizXworzoneTemp = new tblMatrizWorkzoneTemp();
            matrizXworzoneTemp.Usuario = username;
            matrizXworzoneTemp.DataCriacao = DateTime.Now;

            var MatrizExists = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var idMatriz = 0;
            var avaliationExist = _matrizFuncTrainingTempService.GetMatrizTempByFuncXTrain(idTraining, idFuncionario);

            if (MatrizExists == null)
                idMatriz = _matrizTempService.CreateMatrizTemp(matrizXworzoneTemp).idMatrizWZTemp;

            else
                idMatriz = MatrizExists.idMatrizWZTemp;


            var WorkzoneID = idWorkzone;
            tblMatrizFuncXTreinamentoTemp matrizFuncTrain = new tblMatrizFuncXTreinamentoTemp();
            matrizFuncTrain.idFuncionario = idFuncionario;
            matrizFuncTrain.idItemPerfil = idItem;
            matrizFuncTrain.idMatrizWorkzoneTemp = idMatriz;
            matrizFuncTrain.idTreinamento = idTraining;

            if (avaliationExist == null)
            {
                _matrizFuncTrainingTempService.CreateMatrizTemp(matrizFuncTrain);
            }

            else
            {
                matrizFuncTrain.idMatrizFuncTreinTemp = avaliationExist.idMatrizFuncTreinTemp;
                _matrizFuncTrainingTempService.UpdateMatriz(matrizFuncTrain);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idTraining: {0}, idItem: {1}, idFuncionario: {2}", idTraining, idItem, idFuncionario);

            return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
        }

        public void SetColor(int idFuncionario, int idActivity, int idWorkzone)
        {
            var matriz = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var matrizTemp = _matrizFuncActivityTempService.GetMatrizTempByFuncXAtiv(matriz.idMatrizWZTemp, idActivity, idFuncionario);
            var ativ = _activity.GetActivityById(matrizTemp.idAtividade);
            var treinAvaliation = 0;
            var trainamentosQtd = ativ.tblAtividadeXTreinamentos
                .Where(a => a.idAtividade == idActivity).Count();


            foreach (var treinInsideAtiv in ativ.tblAtividadeXTreinamentos)
            {
                foreach (var aval in treinInsideAtiv.tblTreinamento.tblMatrizFuncXTreinamentoTemp)
                {
                    if (treinInsideAtiv.idTreinamento == aval.idTreinamento && idFuncionario == aval.idFuncionario)
                    {
                        treinAvaliation++;
                    }
                }


            }

            if (trainamentosQtd == treinAvaliation)
            {
                matrizTemp.cor = "#3fd44a";
                _matrizFuncActivityTempService.UpdateMatrizTemp(matrizTemp);

            }
            else
            {
                matrizTemp.cor = "#e46a6a";
                _matrizFuncActivityTempService.UpdateMatrizTemp(matrizTemp);
            }

        }

        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Editar)]
        public ActionResult CreateMatrizActivity(int idFuncionario, int idActivity, int idWorkzone, string cor, string alocacaoForcada, int idItem = 0)
        {
            if (idItem == 0)
            {

                return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
            }

            var username = "";
            try
            {
                username = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
            }
            catch (Exception ex)
            {
                username = "";
            }

            tblMatrizWorkzoneTemp matrizXworzoneTemp = new tblMatrizWorkzoneTemp();
            matrizXworzoneTemp.Usuario = username;
            matrizXworzoneTemp.DataCriacao = DateTime.Now;

            var MatrizExists = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var idMatriz = 0;
            var avaliationExist = _matrizFuncActivityTempService.GetMatrizTempByFuncXAtiv(MatrizExists.idMatrizWZTemp, idActivity, idFuncionario);

            if (MatrizExists == null)
                idMatriz = _matrizTempService.CreateMatrizTemp(matrizXworzoneTemp).idMatrizWZTemp;

            else
                idMatriz = MatrizExists.idMatrizWZTemp;


            var WorkzoneID = idWorkzone;
            tblMatrizFuncXAtividadesTemp matrizFuncActiv = new tblMatrizFuncXAtividadesTemp();
            matrizFuncActiv.idFuncionario = idFuncionario;
            matrizFuncActiv.idItemPerfil = idItem;
            matrizFuncActiv.idMatrizWorkzoneTemp = idMatriz;
            matrizFuncActiv.idAtividade = idActivity;
            matrizFuncActiv.alocacaoForcada = alocacaoForcada;
            matrizFuncActiv.cor = cor;

            if (avaliationExist == null)
                _matrizFuncActivityTempService.CreateMatrizTemp(matrizFuncActiv);

            else
            {
                matrizFuncActiv.idMatrizFuncAtivTemp = avaliationExist.idMatrizFuncAtivTemp;
                _matrizFuncActivityTempService.UpdateMatrizTemp(matrizFuncActiv);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idActivity: {0}, idItem: {1}, idFuncionario: {2}", idActivity, idItem, idFuncionario);

            SetColor(idFuncionario, idActivity, idWorkzone);

            return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
        }


        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Excluir)]
        public ActionResult DeleteMatrizTraining(int idFuncionario, int idTraining, int idWorkzone)
        {

            var mWz = _matrizTempService.GetMatrizTempByWZId(idWorkzone);

            _matrizFuncTrainingTempService.DeleteMatrizTemp(mWz.idMatrizWZTemp, idFuncionario, idTraining);

            return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
        }

        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Excluir)]
        public ActionResult DeleteMatrizActivity(int idFuncionario, int idActivity, int idWorkzone)
        {
            var mWz = _matrizTempService.GetMatrizTempByWZId(idWorkzone);

            _matrizFuncActivityTempService.DeleteMatrizTemp(mWz.idMatrizWZTemp, idFuncionario, idActivity);

            return RedirectToAction("MatrizTemp", new { WorkzoneID = idWorkzone });
        }


        public void SetColorToHistory(List<tblMatrizFuncTreinHistorico> trainHist, List<tblMatrizFuncActivityHistorico> ativhist)
        {

            var ativ = new tblAtividades();
            var treinAvaliation = 0;
            var treinamentosQtd = 0;
            //ativ.tblAtividadeXTreinamentos
            //    .Where(a => a.idAtividade == idActivity).Count()



            foreach (var m in ativhist)
            {

                ativ = _activity.GetActivityById(m.idAtividade);

                treinAvaliation = 0;
                treinamentosQtd = ativ.tblAtividadeXTreinamentos
                    .Where(a => a.idAtividade == m.idAtividade).Count();

                foreach (var treinInsideAtiv in ativ.tblAtividadeXTreinamentos)
                {

                    foreach (var aval in treinInsideAtiv.tblTreinamento.tblMatrizFuncXTreinamentoTemp)
                    {
                        if (treinInsideAtiv.idTreinamento == aval.idTreinamento && m.idFuncionario == aval.idFuncionario)
                        {
                            treinAvaliation++;
                        }
                    }
                }


                if (treinamentosQtd == treinAvaliation)
                {
                    m.cor = "#3fd44a";
                }
                else
                {
                    m.cor = "#e46a6a";
                }

            }

            _matrizHistoricoService.SalvarActivityAllHistorico(ativhist);
            _matrizHistoricoService.SalvarTreinAllHistorico(trainHist);

        }


        [AccessHelper(Menu = MenuHelper.MatrizdeConhecimento, Screen = ScreensHelper.MatrizdeConhecimento, Feature = FeaturesHelper.Editar)]
        public ActionResult SalvarHistorico(int idWorkzone)
        {

            tblMatrizWorkzoneHistorico matrizHistorico = new tblMatrizWorkzoneHistorico();
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblMatrizFuncActivityHistorico> atividadeHist = new List<tblMatrizFuncActivityHistorico>();
            List<tblMatrizFuncTreinHistorico> trainingHist = new List<tblMatrizFuncTreinHistorico>();

            // OBJETOS PARA ADICIONAR TREINAMENTOS E ATIVIDADES NAS LISTAS
            tblMatrizFuncTreinHistorico newTrainingHistoricoObj = new tblMatrizFuncTreinHistorico();
            tblMatrizFuncActivityHistorico newActivityHistoricoObj = new tblMatrizFuncActivityHistorico();

            //OBTÉM O ID DA MATRIZ PARA CRIAR O HISTÓRICO E AS INFOS DA WORKZONE(Nome,BU,CC,Linha,Id)
            var TempMatriz = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var workzone = _workzone.GetWorkzoneById(TempMatriz.idWorkzone);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(idWorkzone);

            //  ----------- CRIA A TABELA PRINCIPAL DO HISTÓRICO - 'MATRIZ HISTÓRICO'
            var username = "";
            try
            {
                username = AuthorizationHelper.GetSystem().Usuario.ChaveAmericas;
            }
            catch (Exception ex)
            {
                username = "";
            }

            matrizHistorico.idWorkzone = TempMatriz.idWorkzone;
            matrizHistorico.nomeWorkzone = workzone.Nome;
            matrizHistorico.BUWorkzone = workzone.idBU;
            matrizHistorico.CCWorkzone = workzone.idCC.ToString();
            matrizHistorico.LinhaWorkzone = workzone.idLinha.ToString();
            matrizHistorico.DataCriacao = DateTime.Now;
            matrizHistorico.UsuarioCriacao = username;

            var matrizHistoricoCreated = _matrizHistoricoService.SalvarHistoricoMatrizWorkzone(matrizHistorico);

            // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM ATIVIDADE
            var avalAtiv = _matrizFuncActivityTempService.GetMatrizTempByIdMWZ(TempMatriz.idMatrizWZTemp);


            foreach (var a in activiesList)
            {
                foreach (var wz in workzone.tblWorkzoneXFuncionario)
                {
                    //Verifica se algum usuário possui avaliação na Atividade[n] 
                    var existAvalInActivity = avalAtiv
                        .Where(aa => aa.idAtividade == a.idAtividade
                    && aa.idFuncionario == wz.idFuncionario);

                    var objAtivAval = existAvalInActivity.FirstOrDefault();

                    //Se a Atividade[n] possui avaliação para aquele usuário
                    // Ele adiciona no histórico


                    if (existAvalInActivity.Count() > 0)
                    {
                        // var existeAtiv =
                        //_matrizHistoricoService.getMatrizHistoricoActivityByWZIdFuncAtiv(matrizHistoricoCreated.idMatrizHistorico, objAtivAval.idFuncionario, objAtivAval.idAtividade);

                        var existeAtiv =
                           atividadeHist.Exists(m => m.idFuncionario == objAtivAval.idFuncionario
                          && m.idMatrizWorkzoneHistorico == matrizHistoricoCreated.idMatrizHistorico
                          && m.idAtividade == objAtivAval.idAtividade);

                        if (!existeAtiv)
                        {
                            newActivityHistoricoObj = new tblMatrizFuncActivityHistorico();

                            newActivityHistoricoObj.idAtividade = objAtivAval.idAtividade;
                            newActivityHistoricoObj.nomeAtividade = objAtivAval.tblAtividades.Nome;
                            newActivityHistoricoObj.siglaAtividade = objAtivAval.tblAtividades.Sigla;
                            newActivityHistoricoObj.idFuncionario = objAtivAval.idFuncionario;
                            newActivityHistoricoObj.nomeFuncionario = objAtivAval.tblFuncionarios.Nome;
                            newActivityHistoricoObj.REFuncionario = objAtivAval.tblFuncionarios.RE;
                            newActivityHistoricoObj.BUFuncionario = objAtivAval.tblFuncionarios.idBu_Origem.ToString();
                            newActivityHistoricoObj.idItemPerfil = objAtivAval.idItemPerfil;
                            newActivityHistoricoObj.siglaItemPerfil = objAtivAval.tblPerfilItens.Sigla;
                            newActivityHistoricoObj.alocacaoForcada = objAtivAval.alocacaoForcada;
                            newActivityHistoricoObj.cor = objAtivAval.cor;
                            newActivityHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;


                            atividadeHist.Add(newActivityHistoricoObj);
                            //_matrizHistoricoService.SalvarActivityHistorico(newActivityHistoricoObj);

                        }
                    }
                    else
                    // Senão ele adiciona aquela atividade[n] sem avaliação
                    {
                        var existeAtiv =
                           atividadeHist.Exists(m => m.idFuncionario == wz.idFuncionario
                          && m.idMatrizWorkzoneHistorico == matrizHistoricoCreated.idMatrizHistorico
                          && m.idAtividade == a.idAtividade);

                        // var existeAtiv =
                        //_matrizHistoricoService.getMatrizHistoricoActivityByWZIdFuncAtiv(matrizHistoricoCreated.idMatrizHistorico, wz.idFuncionario, a.idAtividade);
                        if (!existeAtiv)
                        {
                            newActivityHistoricoObj = new tblMatrizFuncActivityHistorico();

                            newActivityHistoricoObj.idAtividade = a.idAtividade;
                            newActivityHistoricoObj.nomeAtividade = a.Nome;
                            newActivityHistoricoObj.siglaAtividade = a.Sigla;
                            newActivityHistoricoObj.idFuncionario = wz.idFuncionario;
                            newActivityHistoricoObj.nomeFuncionario = wz.tblFuncionarios.Nome;
                            newActivityHistoricoObj.REFuncionario = wz.tblFuncionarios.RE;
                            newActivityHistoricoObj.BUFuncionario = wz.tblFuncionarios.idBu_Origem.ToString();
                            newActivityHistoricoObj.idItemPerfil = 0;
                            newActivityHistoricoObj.cor = "gray";
                            newActivityHistoricoObj.siglaItemPerfil = "";
                            newActivityHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;

                            atividadeHist.Add(newActivityHistoricoObj);

                            //_matrizHistoricoService.SalvarActivityHistorico(newActivityHistoricoObj);
                        }
                    }
                }
            }

            // MONTA A LISTA DE TREINAMENTOS
            foreach (var aList in activiesList)
            {
                //Pega todos IDs de atividades associados a treinamentos DA ZONA
                foreach (var aXt in aList.tblAtividadeXTreinamentos)
                {
                    var aux = _training.GetTrainingById(aXt.idTreinamento);

                    //Verifica se o treinamento já existe na Lista
                    if (trainingList.Exists(t => t.IdTreinamento == aux.IdTreinamento) == false)
                        trainingList.Add(aux);
                }
            }


            var avalTraining = _matrizFuncTrainingTempService.GetMatrizTempByIdMWZ(TempMatriz.idMatrizWZTemp);

            foreach (var t in trainingList)
            {
                foreach (var wz in workzone.tblWorkzoneXFuncionario)
                {

                    //Verifica se algum usuário possui avaliação na Atividade[n] 
                    var existAvalInTraining = avalTraining
                        .Where(tt => tt.idTreinamento == t.IdTreinamento
                    && tt.idFuncionario == wz.idFuncionario);

                    var objTrainAval = existAvalInTraining.FirstOrDefault();


                    //Se o Treinamento[n] possui avaliação para aquele usuário
                    // Ele adiciona no histórico
                    if (existAvalInTraining.Count() > 0)
                    {
                        var existeTrein =
                            trainingHist.Exists(m => m.idFuncionario == objTrainAval.idFuncionario
                            && m.idMatrizWorkzoneHistorico == matrizHistoricoCreated.idMatrizHistorico
                            && m.idTreinamento == objTrainAval.idTreinamento);
                        //_matrizHistoricoService.getMatrizHistoricoTrainingByWZIdFuncAtiv(matrizHistoricoCreated.idMatrizHistorico, objTrainAval.idFuncionario, objTrainAval.idTreinamento);

                        if (!existeTrein)
                        {

                            newTrainingHistoricoObj = new tblMatrizFuncTreinHistorico();

                            newTrainingHistoricoObj.idTreinamento = objTrainAval.idTreinamento;
                            newTrainingHistoricoObj.nomeTreinamento = objTrainAval.tblTreinamento.Nome;
                            newTrainingHistoricoObj.idTipoTreinamento = (int)objTrainAval.tblTreinamento.idTipoTreinamento;
                            newTrainingHistoricoObj.nomeTipoTreinamento = objTrainAval.tblTreinamento.tblTipoTreinamento.Nome;
                            newTrainingHistoricoObj.siglaTipoTreinamento = objTrainAval.tblTreinamento.tblTipoTreinamento.Sigla;
                            newTrainingHistoricoObj.idFuncionario = objTrainAval.idFuncionario;
                            newTrainingHistoricoObj.nomeFuncionario = objTrainAval.tblFuncionarios.Nome;
                            newTrainingHistoricoObj.REFuncionario = objTrainAval.tblFuncionarios.RE;
                            newTrainingHistoricoObj.BUFuncionario = objTrainAval.tblFuncionarios.idBu_Origem.ToString();
                            newTrainingHistoricoObj.idItemPerfil = objTrainAval.idItemPerfil;
                            newTrainingHistoricoObj.siglaItemPerfil = objTrainAval.tblPerfilItens.Sigla;
                            newTrainingHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;

                            trainingHist.Add(newTrainingHistoricoObj);

                            //_matrizHistoricoService.SalvarTreinHistorico(newTrainingHistoricoObj);

                        }
                    }
                    else
                    // Senão ele adiciona aquele treinaemnto[n] sem avaliação
                    {
                        var existeTrein =
                             trainingHist.Exists(m => m.idFuncionario == wz.idFuncionario
                            && m.idMatrizWorkzoneHistorico == matrizHistoricoCreated.idMatrizHistorico
                            && m.idTreinamento == t.IdTreinamento);

                        //_matrizHistoricoService.getMatrizHistoricoTrainingByWZIdFuncAtiv(matrizHistoricoCreated.idMatrizHistorico, wz.idFuncionario, t.IdTreinamento);
                        if (!existeTrein)
                        {
                            newTrainingHistoricoObj = new tblMatrizFuncTreinHistorico();

                            newTrainingHistoricoObj.idTreinamento = t.IdTreinamento;
                            newTrainingHistoricoObj.nomeTreinamento = t.Nome;
                            newTrainingHistoricoObj.idTipoTreinamento = (int)t.tblTipoTreinamento.IdTipoTreinamento;
                            newTrainingHistoricoObj.nomeTipoTreinamento = t.tblTipoTreinamento.Nome;
                            newTrainingHistoricoObj.siglaTipoTreinamento = t.tblTipoTreinamento.Sigla;
                            newTrainingHistoricoObj.idFuncionario = wz.idFuncionario;
                            newTrainingHistoricoObj.nomeFuncionario = wz.tblFuncionarios.Nome;
                            newTrainingHistoricoObj.REFuncionario = wz.tblFuncionarios.RE;
                            newTrainingHistoricoObj.BUFuncionario = wz.tblFuncionarios.idBu_Origem.ToString();
                            newTrainingHistoricoObj.idItemPerfil = 0;
                            newTrainingHistoricoObj.siglaItemPerfil = "";
                            newTrainingHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;

                            trainingHist.Add(newTrainingHistoricoObj);

                            //_matrizHistoricoService.SalvarTreinHistorico(newTrainingHistoricoObj);
                        }
                    }

                }

            }

            SetColorToHistory(trainingHist, atividadeHist);

            TransferirValoresMatrizTempToOficial(idWorkzone);

            // Apaga os valores da Matriz Temporária equivalente ao idWorkzone passado
            // Tabela que foi usada pra criar o histórico e passou seus valores pra matriz OFICIAL
            var matrizTemp = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            if (matrizTemp != null)
            {
                _matrizFuncActivityTempService.DeleteMatrizTempAll(matrizTemp.idMatrizWZTemp);
                _matrizFuncTrainingTempService.DeleteMatrizTempAll(matrizTemp.idMatrizWZTemp);
                _matrizTempService.DeleteMatrizTemp(matrizTemp.idMatrizWZTemp);

            }

            return RedirectToAction("Index");
        }

        public void TransferirValoresMatrizTempToOficial(int WorkzoneID)
        {
            var exits = _matrizService.GetMatrizByWZId(WorkzoneID);
            var matrizWz = new tblMatrizWorkzone();
            var exitsMTemp = _matrizTempService.GetMatrizTempByWZId(WorkzoneID);

            //Se a matriz oficial do WZID existe -> deleta para criar uma nova
            // E deleta seus filhos tbm (Atividades e Treinamentos)
            if (exits != null)
            {
                _matrizFuncActivityService.DeleteMatrizAll(exits.idMatrizWZ);
                _matrizFuncTrainingService.DeleteMatrizAll(exits.idMatrizWZ);
            }



            // VERIFICA SE A MATRIZ TEMPORARIA POSSUI AVALIAÇÕES EM ATIVIDADE
            var avalAtiv = _matrizFuncActivityTempService.GetMatrizTempByIdMWZ(exitsMTemp.idMatrizWZTemp);

            tblMatrizFuncXAtividades newAtivObj = new tblMatrizFuncXAtividades();
            List<tblMatrizFuncXAtividades> newAtivList = new List<tblMatrizFuncXAtividades>();

            foreach (var aval in avalAtiv)
            {
                //var matrizOficialAtiv = _matrizFuncActivityService.GetMatrizByFuncXAtiv(exits.idMatrizWZ, aval.idAtividade, aval.idFuncionario);

                //if (matrizOficialAtiv == null)
                //{
                newAtivObj = new tblMatrizFuncXAtividades();

                newAtivObj.idAtividade = aval.idAtividade;
                newAtivObj.idFuncionario = aval.idFuncionario;
                newAtivObj.idItemPerfil = aval.idItemPerfil;
                newAtivObj.cor = aval.cor;
                newAtivObj.alocacaoForcada = aval.alocacaoForcada;
                newAtivObj.idMatrizWorkzone = exits.idMatrizWZ;

                newAtivList.Add(newAtivObj);
                //}
            }

            //Cria todas as atividades e suas respectivas avaliações
            if (newAtivList.Count() > 0)
            {
                _matrizFuncActivityService.CreateAllMatriz(newAtivList);
            }


            // VERIFICA SE A MATRIZ TEMP POSSUI AVALIAÇÕES EM TREINAMENTOS
            var avalTrein = _matrizFuncTrainingTempService.GetMatrizTempByIdMWZ(exitsMTemp.idMatrizWZTemp);


            tblMatrizFuncXTreinamento newTreinObj = new tblMatrizFuncXTreinamento();
            List<tblMatrizFuncXTreinamento> newTreinList = new List<tblMatrizFuncXTreinamento>();

            foreach (var aval in avalTrein)
            {
                newTreinObj = new tblMatrizFuncXTreinamento();

                newTreinObj.idTreinamento = aval.idTreinamento;
                newTreinObj.idFuncionario = aval.idFuncionario;
                newTreinObj.idItemPerfil = aval.idItemPerfil;
                newTreinObj.idMatrizWorkzone = exits.idMatrizWZ;

                newTreinList.Add(newTreinObj);
            }

            if (newTreinList.Count > 0)
                _matrizFuncTrainingService.CreateAllMatriz(newTreinList);

        }
    }
}