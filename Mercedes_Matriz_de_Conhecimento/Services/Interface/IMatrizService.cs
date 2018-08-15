using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{


   
    interface IMatrizService
    {


        IEnumerable<tblMatrizWorkzone>  GetMatriz();

        tblMatrizWorkzone GetMatrizById(int id);

        tblMatrizWorkzone CreateMatriz(tblMatrizWorkzone Matriz);

        tblMatrizWorkzone UpdateMatriz(tblMatrizWorkzone Matriz);

        tblMatrizWorkzone DeleteMatriz(int id);
    }
}
