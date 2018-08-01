using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes_Matriz_de_Conhecimento.Services.Interface
{


   
    interface IEmployeeService
    {


        IEnumerable<tblFuncionarios>  GetEmployees();

        tblFuncionarios GetEmployeeById(int id);

        tblFuncionarios CreateEmployee(tblFuncionarios Employee);

        tblFuncionarios UpdateEmployee(tblFuncionarios Employee);

        tblFuncionarios DeleteEmployee(int id);

        bool checkIfUserAlreadyExits(tblFuncionarios Employee);

        IEnumerable<tblFuncionarios> GetEmployeesWithPagination(int pageNumber, int quantity);
    }
}
