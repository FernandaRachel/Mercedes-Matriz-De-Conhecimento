using Mercedes_Matriz_de_Conhecimento.Services;
using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class WorkzoneService : IWorkzoneService
    {

        private DBConnection _db = new DBConnection();


        public WorkzoneService()
        {
        }

        public IEnumerable<tblWorkzone> GetWorkzones()
        {
            IEnumerable<tblWorkzone> workzone;

           
                var query = from f in _db.tblWorkzone
                            orderby f.Nome
                            select f;

                workzone = query.AsEnumerable();

                return workzone;
        }

        public tblWorkzone GetWorkzoneById(int id)
        {
            tblWorkzone workzone;


            var query = from f in _db.tblWorkzone
                        where f.IdWorkzone == id
                        orderby f.Nome
                        select f;

            workzone = query.FirstOrDefault();

            return workzone;
        }

        public tblWorkzone CreateWorkzone(tblWorkzone Workzone)
        {


            _db.tblWorkzone.Add(Workzone);

            var query = from f in _db.tblWorkzone
                        where f.Nome == Workzone.Nome
                        orderby f.Nome
                        select f;
            if (query != null)
                _db.SaveChanges();

            return Workzone;
        }

        public tblWorkzone UpdateWorkzone(tblWorkzone Workzone)
        {


            var query = from f in _db.tblWorkzone
                        orderby f.Nome
                        select f;


            _db.Entry(Workzone).State = EntityState.Modified;
            _db.SaveChanges();


            return Workzone;
        }

        public tblWorkzone DeleteWorkzone(int id)
        {
            tblWorkzone Workzone;


            var query = from f in _db.tblWorkzone
                        where f.IdWorkzone == id
                        orderby f.Nome
                        select f;

            Workzone = query.FirstOrDefault();

            _db.tblWorkzone.Remove(Workzone);
            _db.SaveChanges();

            return Workzone;
        }

        public long checkIfWorkzoneAlreadyExits(tblWorkzone workzone)
        {
            var query = from f in _db.tblWorkzone
                        where f.Nome == workzone.Nome
                        orderby f.Nome
                        select f;

            if (query != null)
                return 1;

            return 0;
        }
    }
}