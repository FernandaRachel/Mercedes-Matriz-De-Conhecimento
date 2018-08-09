using DCX.ITLC.AutSis.Services.Integracao.Models.Retornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IPermissionsService
    {
        IEnumerable<Sistema> ObterPermissoesPorUsuarioNoSistema();

        void ObterSistemasPorUsuario();


    }
}
