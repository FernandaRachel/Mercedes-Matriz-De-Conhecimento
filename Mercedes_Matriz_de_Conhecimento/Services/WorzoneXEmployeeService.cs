using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PagedList;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class WorzoneXEmployeeService : IWorzoneXEmployeeService
    {

        public DbConnection _db = new DbConnection();



        public tblWorkzoneXFuncionario GetWorzoneXEmployeeById(int idWz, int idFunc)
        {
            tblWorkzoneXFuncionario WorzoneXEmployee;

            var query = from f in _db.tblWorkzoneXFuncionario
                        where f.idWorkzone == idWz && f.idFuncionario == idFunc
                        orderby f.idWorkzone ascending
                        select f;

            WorzoneXEmployee = query.FirstOrDefault();

            return WorzoneXEmployee;
        }

        public IEnumerable<tblWorkzoneXFuncionario> GetWorzoneXEmployee()
        {
            IEnumerable<tblWorkzoneXFuncionario> WorzoneXEmployee;

            var query = from f in _db.tblWorkzoneXFuncionario
                        orderby f.idWorkzoneFuncionario ascending
                        select f;

            WorzoneXEmployee = query.AsEnumerable();

            return WorzoneXEmployee;
        }


        public tblWorkzoneXFuncionario CreateWorzoneXEmployee(tblWorkzoneXFuncionario WorzoneXEmployee)
        {
            _db.tblWorkzoneXFuncionario.Add(WorzoneXEmployee);

            _db.SaveChanges();


            return WorzoneXEmployee;
        }

        public tblWorkzoneXFuncionario DeleteWorzoneXEmployee(int id)
        {
            tblWorkzoneXFuncionario WorzoneXEmployee;

            var query = from f in _db.tblWorkzoneXFuncionario
                        where f.idWorkzoneFuncionario == id
                        orderby f.idWorkzoneFuncionario ascending
                        select f;

            WorzoneXEmployee = query.FirstOrDefault();

            _db.tblWorkzoneXFuncionario.Remove(WorzoneXEmployee);
            _db.SaveChanges();

            return WorzoneXEmployee;
        }


        public tblWorkzoneXFuncionario UpdateWorzoneXEmployee(tblWorkzoneXFuncionario WorzoneXEmployee)
        {
            var trainingToUpdate = _db.tblWorkzoneXFuncionario.Find(WorzoneXEmployee.idWorkzoneFuncionario);
            trainingToUpdate.idWorkzone = WorzoneXEmployee.idWorkzone;
            trainingToUpdate.idFuncionario = WorzoneXEmployee.idFuncionario;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfWorzoneXEmployeeAlreadyExits(tblWorkzoneXFuncionario WorzoneXEmployee)
        {
            var query = from f in _db.tblWorkzoneXFuncionario
                        where f.idFuncionario == WorzoneXEmployee.idFuncionario &&
                        f.idWorkzone == WorzoneXEmployee.idWorkzone
                        select f;
            if (query.Count() == 1)
                return true;

            return false;
        }


        public IPagedList<tblWorkzoneXFuncionario> GetWorzoneXEmployeePagination(int pageNumber, int quantity)
        {
            IPagedList<tblWorkzoneXFuncionario> WorzoneXEmployee;

            var query = from f in _db.tblWorkzoneXFuncionario
                        orderby f.idWorkzoneFuncionario ascending
                        select f;

            WorzoneXEmployee = query.ToPagedList(pageNumber,quantity);

            return WorzoneXEmployee;
        }
    }
}