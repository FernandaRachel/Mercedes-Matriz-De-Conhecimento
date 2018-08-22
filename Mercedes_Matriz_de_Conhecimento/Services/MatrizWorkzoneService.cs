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
    public class MatrizWorkzoneService : IMatrizService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizWorkzone GetMatrizById(int id)
        {
            tblMatrizWorkzone Matriz;

            var query = from f in _db.tblMatrizWorkzone
                        where f.idMatrizWZ == id
                        orderby f.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public tblMatrizWorkzone GetMatrizByWZId(int idWz)
        {
            tblMatrizWorkzone Matriz;

            var query = from f in _db.tblMatrizWorkzone
                        where f.idWorkzone == idWz
                        orderby f.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public IEnumerable<tblMatrizWorkzone> GetMatriz()
        {
            IEnumerable<tblMatrizWorkzone> Matriz;



            var query = from f in _db.tblMatrizWorkzone
                        orderby f.tblWorkzone.Nome ascending
                        select f;

            Matriz = query.AsEnumerable();

            return Matriz.AsEnumerable();
        }


        public tblMatrizWorkzone CreateMatriz(tblMatrizWorkzone Matriz)
        {
            var exist = GetMatrizByWZId(Matriz.idWorkzone);

            if (exist == null)
            {

                _db.tblMatrizWorkzone.Add(Matriz);

                _db.SaveChanges();

                var matrizCreated = _db.tblMatrizWorkzone
                   .OrderByDescending(w => w.DataCriacao)
                   .FirstOrDefault();

                return matrizCreated;
            }
            else
            {
                return exist;
            }
        }

        public tblMatrizWorkzone DeleteMatriz(int id)
        {
            tblMatrizWorkzone Matriz;

            var query = from f in _db.tblMatrizWorkzone
                        where f.idMatrizWZ == id
                        orderby f.tblWorkzone.Nome ascending
                        select f;

            Matriz = query.FirstOrDefault();

            _db.tblMatrizWorkzone.Remove(Matriz);
            _db.SaveChanges();

            return Matriz;
        }


        public tblMatrizWorkzone UpdateMatriz(tblMatrizWorkzone Matriz)
        {
            var matrizToUpdate = _db.tblMatrizWorkzone.Find(Matriz.idMatrizWZ);
            matrizToUpdate.idWorkzone = Matriz.idWorkzone;
            matrizToUpdate.Usuario = Matriz.Usuario;
            matrizToUpdate.DataCriacao = Matriz.DataCriacao;

            _db.Entry(matrizToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizToUpdate;
        }
    }
}