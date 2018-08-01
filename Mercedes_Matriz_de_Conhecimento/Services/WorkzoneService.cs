using Mercedes_Matriz_de_Conhecimento.Services;
using Mercedes_Matriz_de_Conhecimento.Services.Interface;
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

            var query = from f in _db.tblWorkzone
                        where f.Nome == Workzone.Nome
                        orderby f.Nome ascending
                        select f;
            if (query != null)
                _db.SaveChanges();

            return Workzone;
        }

        public tblWorkzone UpdateWorkzone(tblWorkzone Workzone)
        {
            var wzToUpdate = _db.tblWorkzone.Find(Workzone.IdWorkzone);
            wzToUpdate.Nome = Workzone.Nome;
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
    }

}