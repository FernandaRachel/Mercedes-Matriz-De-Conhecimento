using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class EmployeeService : IEmployeeService
    {

        public DBConnection _db = new DBConnection();


        public EmployeeService()
        {
            //_db = new DBConnection();
        }



        public IEnumerable<tblFuncionarios> GetEmployees()
        {

            IEnumerable<tblFuncionarios> employee;



            var query = from f in _db.tblFuncionarios
                         orderby f.Nome
                         select f;

            employee = query.AsEnumerable();
            return employee;
        }

        public tblFuncionarios GetEmployeeById(int id)
        {
            tblFuncionarios employee;

            var query = from f in _db.tblFuncionarios
                        where f.idfuncionario == id
                        orderby f.Nome
                        select f;

            employee = query.FirstOrDefault();

            return employee;
        }

        public tblFuncionarios CreateEmployee(tblFuncionarios Employee)
        {

           
            _db.tblFuncionarios.Add(Employee);

            _db.SaveChanges();


            return Employee;

        }

        public tblFuncionarios DeleteEmployee(int id)
        {
            tblFuncionarios Employee;



            var query = from f in _db.tblFuncionarios
                        where f.idfuncionario == id
                        orderby f.Nome
                        select f;

            Employee = query.FirstOrDefault();

            _db.tblFuncionarios.Remove(Employee);
            _db.SaveChanges();

            return Employee;

        }

        public tblFuncionarios UpdateEmployee(tblFuncionarios Employee)
        {



            var query = from f in _db.tblFuncionarios
                        orderby f.Nome
                        select f;


            _db.Entry(Employee).State = EntityState.Modified;
            _db.SaveChanges();


            return Employee;
        }

        public bool checkIfUserAlreadyExits(tblFuncionarios Employee)
        {
            var query = from f in _db.tblFuncionarios
                        where f.Nome == Employee.Nome
                        orderby f.Nome
                        select f;

            if (query != null)
                return false;

            return true;
        }
    }
}