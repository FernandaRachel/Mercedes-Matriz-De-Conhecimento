using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Helpers
{

    public enum MenuHelper
    {
        VisualizacaoCadastro,
        Associacao,
        HistoricodaMatriz,
        MatrizdeConhecimento,
    }

    public enum ScreensHelper
    {
        Funcionario,
        PostodeTrabalho,
        Treinamento,
        TipodeTreinamento,
        PerfildeTreinamento,
        ItemdeTreinamento,
        Atividades,
        PerfildeAtividades,
        ItemdeAtividade,
        PostodeTrabalhoXAtividade,
        TreinamentoXAtividade,
        PerfilAtividadeItemXPerfilItem,
        PerfilTreinamentoItemXPerfilItem,
        HistoricodaMatriz,
        MatrizdeConhecimento
    }

    public enum FeaturesHelper
    {
        Consultar,
        Editar,
        Deletar
    }
}