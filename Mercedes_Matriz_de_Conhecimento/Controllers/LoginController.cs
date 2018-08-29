//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using log4net;
//using System.Reflection;
//using System.Configuration;
//using Mercedes_Matriz_de_Conhecimento.Models;
//using System.Web.Mvc;

//namespace Mercedes_Matriz_de_Conhecimento.Controllers
//{
//    public class LoginController : Controller
//    {
//        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        


//        //public string ValidaUsuario(string login, string senha, bool LogarTeste)
//        //{
//        //    try
//        //    {
//        //        string sRetorno = "OK";

//        //        if (!LogarTeste)
//        //        {
//        //            var retornoAD = ValidarUsuarioAD(login, senha);

//        //            if (retornoAD == false)
//        //                sRetorno = "Login ou Usuário invalido!!!";

//        //            if (retornoAD == null)
//        //                sRetorno = "Erro ao tentar validação no AD, Verificar log para maiores detalhes";

//        //        }

//        //        if (sRetorno == "OK")
//        //        {
//        //            UserModel usuarioModel = new UserModel();

//        //            var usuarioDB = usuarioModel.SelectUsuario(login);

//        //            if (usuarioDB == null)
//        //            {
//        //                return "Usuário não registrado no sistema de Tecalon";
//        //            }

//        //            if (usuarioDB.status.ToLower() != "ativo")
//        //            {
//        //                return "Usuário desativado";
//        //            }

//        //            instance.id = usuarioDB.id;
//        //            instance.login = usuarioDB.login;
//        //            instance.nivel = usuarioDB.nivel;
//        //            instance.status = usuarioDB.status;
//        //        }
//        //        return sRetorno;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        log.Error(ex.ToString());
//        //        return ex.ToString();
//        //    }
//        //}

//        private bool? ValidarUsuarioAD(string Usuario, string Senha)
//        {
//            string Domain = ConfigurationManager.AppSettings["Dominio"];
//            string Servidor = ConfigurationManager.AppSettings["Servidor"];
//            string Empresa = ConfigurationManager.AppSettings["Empresa"];
//            string Idioma = ConfigurationManager.AppSettings["Idioma"];
//            string Hash = "";
//            Usuario oUsuario = null;

//            using (Autenticacao oIdentificacao = new Autenticacao())
//            {
//                try
//                {
//                    oIdentificacao.Timeout = 30;
//                    oIdentificacao.Retry = false;
//                    oIdentificacao.Dominio = Domain;
//                    oUsuario = oIdentificacao.VerificarSenha(Autenticacao.TipoAutenticacao.Domain, ref Servidor, ref Empresa, ref Usuario, ref Senha, ref Idioma, ref Hash);
//                    if (oIdentificacao.Status.Equals(0))
//                    {
//                        log.Debug("Usuário Logado!!!");
//                        log.Debug(oUsuario.Chave.ToUpper());
//                        log.Debug(oUsuario.NomeCompleto.ToUpper());

//                    }
//                    else
//                    {
//                        log.Error(oIdentificacao.Mensagem);
//                        log.Error(oIdentificacao.ReturnMessage);
//                        return false;
//                    }
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    log.Error(ex.ToString());
//                    return null;
//                }
//            }
//        }
//    }
//}