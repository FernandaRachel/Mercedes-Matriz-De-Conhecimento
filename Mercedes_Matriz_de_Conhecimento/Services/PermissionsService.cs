using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCX.ITLC.AutSis.Services.Integracao;
using DCX.ITLC.AutSis.Services.Integracao.Models.Retornos;


namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class PermissionsService : IPermissionsService
    {
        public IEnumerable<Sistema> ObterPermissoesPorUsuarioNoSistema()
        {
            Console.WriteLine("**************************************************");

            var sistemas = new IntegracaoAutSis().ObterSistemasPorUsuario("SILALIS", 0);

            foreach (var sistema in sistemas)
            {
                Console.WriteLine($"Sistema: {sistema.Nome}");
                Console.WriteLine($"Descrição: {sistema.Descricao}");
            }

            Console.WriteLine("**************************************************");

            return sistemas;
        }

        public void ObterSistemasPorUsuario()
        {
            try
            {
                var sistema = new IntegracaoAutSis().ObterPermissoes("StepNet", "SILALIS", 0);

                if (!string.IsNullOrEmpty(sistema.MensagemDeErro))
                {
                    Console.WriteLine("Houve um erro para obter as informações do sistema");
                    Console.WriteLine("Erro: {0}", sistema.MensagemDeErro);
                }
                else
                {
                    var nomeGrupo = sistema.GrupoDeAcesso ?? new GrupoDeAcesso();
                    var nomePerfil = sistema.Perfil ?? new PerfilSistema();

                    Console.WriteLine($"Sistema: {sistema.Nome}");
                    Console.WriteLine($"Descrição: {sistema.Descricao}");
                    Console.WriteLine($"Perfil: {nomePerfil.Nome}");
                    Console.WriteLine($"Grupo de Acesso: {nomeGrupo.Nome}");
                    Console.WriteLine($"Usuario: {sistema.Usuario.Nome}");
                    Console.WriteLine($"E-mail: {sistema.Usuario.Email}");
                    Console.WriteLine($"Chave Americas: {sistema.Usuario.ChaveAmericas}");
                    Console.WriteLine("Funcionalidades de Autorização");
                    Console.WriteLine("**************************************************");
                    Console.WriteLine(sistema.FuncionalidadesDeAutorizacao.Nome);
                    MostrarFuncionalidades(sistema
                        .FuncionalidadesDeAutorizacao);
                    Console.WriteLine("**************************************************");

                    FuncionalidadeAutorizacao teste = new FuncionalidadeAutorizacao();
                    //teste.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void MostrarFuncionalidades(FuncionalidadeAutorizacao funcionalidadePai)
        {
            foreach (var filho in funcionalidadePai.Filhos)
            {
                Console.WriteLine(filho.Nome);

                if (filho.Filhos.Count > 0)
                {
                    MostrarFuncionalidades(filho);
                }
            }
        }
    }
}