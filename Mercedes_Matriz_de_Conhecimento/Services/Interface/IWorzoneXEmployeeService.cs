using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{
    interface IWorzoneXEmployeeService
    {

        IEnumerable<tblWorkzoneXFuncionario> GetWorzoneXEmployee();

        tblWorkzoneXFuncionario GetWorzoneXEmployeeById(int idWZ, int idFunc);

        tblWorkzoneXFuncionario CreateWorzoneXEmployee(tblWorkzoneXFuncionario WorzoneXEmployee);

        tblWorkzoneXFuncionario UpdateWorzoneXEmployee(tblWorkzoneXFuncionario WorzoneXEmployee);

        tblWorkzoneXFuncionario DeleteWorzoneXEmployee(int id);

        bool checkIfWorzoneXEmployeeAlreadyExits(tblWorkzoneXFuncionario WorzoneXEmployee);

        IPagedList<tblWorkzoneXFuncionario> GetWorzoneXEmployeePagination(int pageNumber, int quantity);
    }
}
