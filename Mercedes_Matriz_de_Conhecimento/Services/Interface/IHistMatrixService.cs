using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IHistMatrixService
    {
        void SavePdf(HttpPostedFileBase uploadFile, string FilePath);
        ActionResult ReadPdf(int id);
    }
}
