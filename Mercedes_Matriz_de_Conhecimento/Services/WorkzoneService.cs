using Mercedes_Matriz_de_Conhecimento.Services;
using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class WorkzoneService : IWorkzoneService
    {

        private DbConnection _db = new DbConnection();


        public WorkzoneService()
        {
        }

        public IEnumerable<tblWorkzone> GetWorkzones()
        {
            IEnumerable<tblWorkzone> workzone;

            var query = from f in _db.tblWorkzone
                        orderby f.Nome ascending
                        select f;

            workzone = query.AsEnumerable();

            return workzone;
        }


        public tblWorkzone GetWorkzoneById(int id)
        {
            tblWorkzone workzone;


            var query = from f in _db.tblWorkzone
                        where f.IdWorkzone == id
                        orderby f.Nome ascending
                        select f;

            workzone = query.FirstOrDefault();

            return workzone;
        }

        public tblWorkzone CreateWorkzone(tblWorkzone Workzone)
        {

            _db.tblWorkzone.Add(Workzone);
            _db.SaveChanges();

            var wz = _db.tblWorkzone
                .OrderByDescending(w => w.DataCriacao)
                .FirstOrDefault();

            return wz;
        }

        public tblWorkzone UpdateWorkzone(tblWorkzone Workzone)
        {
            var wzToUpdate = _db.tblWorkzone.Find(Workzone.IdWorkzone);
            wzToUpdate.Nome = Workzone.Nome;
            wzToUpdate.idBU = Workzone.idBU; 
            wzToUpdate.idCC = Workzone.idCC; 
            wzToUpdate.idLinha = Workzone.idLinha;
            wzToUpdate.DataAlteracao = Workzone.DataAlteracao;
            wzToUpdate.MotivoUltimaAlteracao = Workzone.MotivoUltimaAlteracao;
            wzToUpdate.PessoasNecessarias = Workzone.PessoasNecessarias;
            wzToUpdate.UsuarioAlteracao = Workzone.UsuarioAlteracao;
            wzToUpdate.UsuarioDesativacao = Workzone.UsuarioDesativacao;
            wzToUpdate.MotivoUltimaAlteracao = Workzone.MotivoUltimaAlteracao;

            _db.Entry(wzToUpdate).State = EntityState.Modified;
            _db.SaveChanges();

            return Workzone;
        }

        public tblWorkzone DeleteWorkzone(int id)
        {
            tblWorkzone Workzone;


            var query = from f in _db.tblWorkzone
                        where f.IdWorkzone == id
                        orderby f.Nome ascending
                        select f;

            Workzone = query.FirstOrDefault();

            _db.tblWorkzone.Remove(Workzone);
            _db.SaveChanges();

            return Workzone;
        }

        public bool checkIfWorkzoneAlreadyExits(tblWorkzone workzone)
        {
            var query = from f in _db.tblWorkzone
                        where f.Nome == workzone.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdWorkzone != workzone.IdWorkzone)
                return true;

            return false;
        }

        public IPagedList<tblWorkzone> GetWorkzonesWithPagination(int pageNumber, int quantity = 15)
        {
            IPagedList<tblWorkzone> workzone;


            var query = from f in _db.tblWorkzone
                        orderby f.Nome ascending
                        select f;

            workzone = query.ToPagedList(pageNumber, quantity);

            return workzone;
        }

        public List<tblFuncionarios> setUpEmployees(int idWZ)
        {
            List<tblFuncionarios> allEmployee = new List<tblFuncionarios>();

            var query = from f in _db.tblWorkzoneXFuncionario
                        where f.idWorkzone == idWZ
                        select f;

            foreach (var func in query)
            {
                var query2 = from f in _db.tblFuncionarios
                             where f.idfuncionario == func.idFuncionario
                             select f;
                allEmployee.Add(query2.FirstOrDefault());
            }

            return allEmployee;
        }
    }

}