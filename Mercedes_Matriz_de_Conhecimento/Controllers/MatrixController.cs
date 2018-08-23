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

        public MatrixController()
        {
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
            var matrizWzTemp = new tblMatrizWorkzoneTemp();
            matrizWz.idMatrizWZ = 0;
            matrizWzTemp.idMatrizWZTemp = 0;

            if (exits == null)
            {
                // CRIA MATRIZ OFICIAL SE ELA N EXISTIR
                tblMatrizWorkzone matrizXworzoneTemp = new tblMatrizWorkzone();
                matrizXworzoneTemp.Usuario = "Teste s/ Seg";
                matrizXworzoneTemp.DataCriacao = DateTime.Now;
                matrizXworzoneTemp.idWorkzone = WorkzoneID;
                matrizWz = _matrizService.CreateMatriz(matrizXworzoneTemp);

                // CRIA MATRIZ TEMPORÁRIA ONDE A EDIÇÃO SERÁ FEITA
                tblMatrizWorkzoneTemp matrizXworzoneTempTemp = new tblMatrizWorkzoneTemp();
                matrizXworzoneTempTemp.Usuario = "Teste s/ Seg";
                matrizXworzoneTempTemp.DataCriacao = DateTime.Now;
                matrizXworzoneTempTemp.idWorkzone = WorkzoneID;
                matrizWzTemp = _matrizTempService.CreateMatrizTemp(matrizXworzoneTempTemp);

            }
            else
            {
                matrizWz = exits;

                var exitsMTemp = _matrizTempService.GetMatrizTempByWZId(WorkzoneID);

                if (exitsMTemp == null)
                {


                    // CRIA MATRIZ TEMPORÁRIA ONDE A EDIÇÃO SERÁ FEITA
                    tblMatrizWorkzoneTemp matrizXworzoneTempTemp = new tblMatrizWorkzoneTemp();
                    matrizXworzoneTempTemp.Usuario = "Teste s/ Seg";
                    matrizXworzoneTempTemp.DataCriacao = DateTime.Now;
                    matrizXworzoneTempTemp.idWorkzone = WorkzoneID;
                    matrizWzTemp = _matrizTempService.CreateMatrizTemp(matrizXworzoneTempTemp);

                    // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM ATIVIDADE
                    var avalAtiv = _matrizFuncActivityService.GetMatrizByMWZId(matrizWz.idMatrizWZ);

                    if (avalAtiv.Count() > 0)
                    {
                        tblMatrizFuncXAtividadesTemp newAvalObj = new tblMatrizFuncXAtividadesTemp();

                        foreach (var aval in avalAtiv)
                        {
                            newAvalObj.idAtividade = aval.idAtividade;
                            newAvalObj.idFuncionario = aval.idFuncionario;
                            newAvalObj.idItemPerfil = aval.idItemPerfil;
                            newAvalObj.idMatrizWorkzoneTemp = matrizWzTemp.idMatrizWZTemp;

                            _matrizFuncActivityTempService.CreateMatrizTemp(newAvalObj);
                        }
                    }


                    // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM TREINAMENTOS
                    var avalTrein = _matrizFuncTrainingService.GetMatrizByIdMWZ(matrizWz.idMatrizWZ);

                    if (avalTrein.Count() > 0)
                    {
                        tblMatrizFuncXTreinamentoTemp newTreinObj = new tblMatrizFuncXTreinamentoTemp();

                        foreach (var aval in avalTrein)
                        {
                            newTreinObj.idTreinamento = aval.idTreinamento;
                            newTreinObj.idFuncionario = aval.idFuncionario;
                            newTreinObj.idItemPerfil = aval.idItemPerfil;
                            newTreinObj.idMatrizWorkzoneTemp = matrizWzTemp.idMatrizWZTemp;

                            _matrizFuncTrainingTempService.CreateMatrizTemp(newTreinObj);
                        }
                    }
                }
            }


            var workzone = _workzone.GetWorkzoneById(WorkzoneID);
            var activiesList = _workzoneXActivity.SetUpActivitiesList(WorkzoneID);
            List<tblTreinamento> trainingList = new List<tblTreinamento>();
            List<tblTipoTreinamento> ttList = new List<tblTipoTreinamento>();


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

        public ActionResult CreateMatrizTraining(int idItem, int idFuncionario, int idTraining, int idWorkzone)
        {
            tblMatrizWorkzoneTemp matrizXworzoneTemp = new tblMatrizWorkzoneTemp();
            matrizXworzoneTemp.Usuario = "Teste s/ Seg";
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
                _matrizFuncTrainingTempService.CreateMatrizTemp(matrizFuncTrain);

            else
            {
                matrizFuncTrain.idMatrizFuncTreinTemp = avaliationExist.idMatrizFuncTreinTemp;
                _matrizFuncTrainingTempService.UpdateMatriz(matrizFuncTrain);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idTraining: {0}, idItem: {1}, idFuncionario: {2}", idTraining, idItem, idFuncionario);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }

        public ActionResult CreateMatrizActivity(int idItem, int idFuncionario, int idActivity, int idWorkzone)
        {
            tblMatrizWorkzoneTemp matrizXworzoneTemp = new tblMatrizWorkzoneTemp();
            matrizXworzoneTemp.Usuario = "Teste s/ Seg";
            matrizXworzoneTemp.DataCriacao = DateTime.Now;

            var MatrizExists = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var idMatriz = 0;
            var avaliationExist = _matrizFuncActivityService.GetMatrizByFuncXAtiv(idActivity, idFuncionario);

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

            if (avaliationExist == null)
                _matrizFuncActivityTempService.CreateMatrizTemp(matrizFuncActiv);

            else
            {
                matrizFuncActiv.idMatrizFuncAtivTemp = avaliationExist.idMatrizFuncAtiv;
                _matrizFuncActivityTempService.UpdateMatrizTemp(matrizFuncActiv);
            }

            var workzone = _workzone.GetWorkzoneById(WorkzoneID);

            Console.WriteLine("idActivity: {0}, idItem: {1}, idFuncionario: {2}", idActivity, idItem, idFuncionario);

            return RedirectToAction("Matriz", new { WorkzoneID = idWorkzone });
        }


        public ActionResult DeleteMatrizTraining(int idFuncionario, int idTraining, int idWorkzone)
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


        public ActionResult SalvarHistorico(int idWorkzone)
        {
            tblMatrizWorkzoneHistorico matrizHistorico = new tblMatrizWorkzoneHistorico();

            // OBJETOS PARA ADICIONAR TREINAMENTOS E ATIVIDADES NAS LISTAS
            tblMatrizFuncTreinHistorico newTrainingHistoricoObj = new tblMatrizFuncTreinHistorico();
            tblMatrizFuncActivityHistorico newActivityHistoricoObj = new tblMatrizFuncActivityHistorico();

            //OBTÉM O ID DA MATRIZ PARA CRIAR O HISTÓRICO E AS INFOS DA WORKZONE(Nome,BU,CC,Linha,Id)
            var TempMatriz = _matrizTempService.GetMatrizTempByWZId(idWorkzone);
            var workzone = _workzone.GetWorkzoneById(TempMatriz.idWorkzone);

            //  ----------- CRIA A TABELA PRINCIPAL DO HISTÓRICO - 'MATRIZ HISTÓRICO'
            matrizHistorico.idWorkzone = TempMatriz.idWorkzone;
            matrizHistorico.nomeWorkzone = workzone.Nome;
            matrizHistorico.BUWorkzone = workzone.idBU;
            matrizHistorico.CCWorkzone = workzone.idCC.ToString();
            matrizHistorico.LinhaWorkzone = workzone.idLinha.ToString();
            matrizHistorico.DataCriacao = DateTime.Now;
            matrizHistorico.UsuarioCriacao = "Usuário Histórico";

            var matrizHistoricoCreated = _matrizHistoricoService.SalvarHistoricoMatrizWorkzone(matrizHistorico);

            // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM ATIVIDADE
            var avalAtiv = _matrizFuncActivityTempService.GetMatrizTempByIdMWZ(TempMatriz.idMatrizWZTemp);


            // POPULA A TABELA DE HISTÓRICO DE ATIVIDADES AVALIADAS NA MATRIZ
            if (avalAtiv.Count() > 0)
            {
                newActivityHistoricoObj = new tblMatrizFuncActivityHistorico();

                foreach (var aval in avalAtiv)
                {
                    newActivityHistoricoObj.idAtividade = aval.idAtividade;
                    newActivityHistoricoObj.nomeAtividade = aval.tblAtividades.Nome;
                    newActivityHistoricoObj.siglaAtividade = aval.tblAtividades.Sigla;
                    newActivityHistoricoObj.idFuncionario = aval.idFuncionario;
                    newActivityHistoricoObj.nomeFuncionario = aval.tblFuncionarios.Nome;
                    newActivityHistoricoObj.REFuncionario = aval.tblFuncionarios.RE;
                    newActivityHistoricoObj.BUFuncionario = aval.tblFuncionarios.idBu_Origem.ToString();
                    newActivityHistoricoObj.idItemPerfil = aval.idItemPerfil;
                    newActivityHistoricoObj.siglaItemPerfil = aval.tblPerfilItens.Sigla;
                    newActivityHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;

                    _matrizHistoricoService.SalvarActivityHistorico(newActivityHistoricoObj);
                }
            }


            // VERIFICA SE A MATRIZ POSSUI AVALIAÇÕES EM TREINAMENTOS
            var avalTrein = _matrizFuncTrainingTempService.GetMatrizTempByIdMWZ(TempMatriz.idMatrizWZTemp);

            if (avalTrein.Count() > 0)
            {
                newTrainingHistoricoObj = new tblMatrizFuncTreinHistorico();

                foreach (var aval in avalTrein)
                {
                    newTrainingHistoricoObj.idTreinamento = aval.idTreinamento;
                    newTrainingHistoricoObj.nomeTreinamento = aval.tblTreinamento.Nome;
                    newTrainingHistoricoObj.idTipoTreinamento = (int)aval.tblTreinamento.idTipoTreinamento;
                    newTrainingHistoricoObj.nomeTipoTreinamento = aval.tblTreinamento.tblTipoTreinamento.Nome;
                    newTrainingHistoricoObj.siglaTipoTreinamento = aval.tblTreinamento.tblTipoTreinamento.Sigla;
                    newTrainingHistoricoObj.idFuncionario = aval.idFuncionario;
                    newTrainingHistoricoObj.nomeFuncionario = aval.tblFuncionarios.Nome;
                    newTrainingHistoricoObj.REFuncionario = aval.tblFuncionarios.RE;
                    newTrainingHistoricoObj.BUFuncionario = aval.tblFuncionarios.idBu_Origem.ToString();
                    newTrainingHistoricoObj.idItemPerfil = aval.idItemPerfil;
                    newTrainingHistoricoObj.siglaItemPerfil = aval.tblPerfilItens.Sigla;
                    newTrainingHistoricoObj.idMatrizWorkzoneHistorico = matrizHistoricoCreated.idMatrizHistorico;

                    _matrizHistoricoService.SalvarTreinHistorico(newTrainingHistoricoObj);
                }

            }

            return RedirectToAction("Index");
        }

    }
}