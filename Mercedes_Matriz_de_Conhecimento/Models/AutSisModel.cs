using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class SistemaApi
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public GrupoDeAcessoApi GrupoDeAcesso { get; set; }

        public PerfilSistemaApi Perfil { get; set; }

        public UsuarioApi Usuario { get; set; }

        public FuncionalidadeAutorizacao FuncionalidadeAutorizacao { get; set; }

        public List<FuncionalidadeAutorizacao> FuncionalidadesDeAutorizacao { get; set; }

        public List<FuncionalidadeAssociacaoApi> FuncionalidadeAssociacaoApi { get; set; }

        public List<GrupoSistemaApi> GruposDeSistema { get; set; }

        public string MensagemDeErro { get; set; }
    }


    public class GrupoDeAcessoApi
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }

    public class PerfilSistemaApi
    {

        public string Nome { get; set; }

        public string Descricao { get; set; }
    }

    public class UsuarioApi
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string ChaveMainframe { get; set; }

        public string ChaveAmericas { get; set; }

        public string MensagemDeErro { get; set; }
    }

    public class FuncionalidadeAutorizacao
    {
        public int CodigoFuncionalidade { get; set; }

        public string Nome { get; set; }

        public string Parametros { get; set; }

        public int CodigoFuncionalidadePai { get; set; }

        public List<FuncionalidadeAutorizacao> Filhos { get; set; }
    }

    public class FuncionalidadeAssociacaoApi
    {
        public string Nome { get; set; }

    }

    public class GrupoSistemaApi
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public FuncionalidadeGrupoDeSistemaApi Funcionalidades { get; set; }
    }


    public class FuncionalidadeGrupoDeSistemaApi
    {
        public int CodigoFuncionalidade { get; set; }

        public int CodigoFuncionalidadePai { get; set; }

        public string Nome { get; set; }

        public string Parametros { get; set; }

        public List<FuncionalidadeGrupoDeSistemaApi> Filhos { get; set; }
    }

}