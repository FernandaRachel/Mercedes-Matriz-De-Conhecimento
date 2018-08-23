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
    public class MatrizFuncAtividadeService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizFuncXAtividades GetMatrizById(int id)
        {
            tblMatrizFuncXAtividades Matriz;

            var query = from f in _db.tblMatrizFuncXAtividades
                        where f.idMatrizFuncAtiv == id
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public IEnumerable<tblMatrizFuncXAtividades> GetMatrizByMWZId(int idMWZ)
        {
            IEnumerable<tblMatrizFuncXAtividades> Matriz;

            var query = from f in _db.tblMatrizFuncXAtividades
                        where f.idMatrizWorkzone == idMWZ
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query;

            return Matriz;
        }

        public tblMatrizFuncXAtividades GetMatrizByFuncXAtiv(int idActivity, int idFunc)
        {
            tblMatrizFuncXAtividades Matriz;

            var query = from f in _db.tblMatrizFuncXAtividades
                        where f.idAtividade == idActivity && f.idFuncionario == idFunc
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public IEnumerable<tblMatrizFuncXAtividades> GetMatriz()
        {
            IEnumerable<tblMatrizFuncXAtividades> Matriz;



            var query = from f in _db.tblMatrizFuncXAtividades
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.AsEnumerable();

            return Matriz.AsEnumerable();
        }


        public tblMatrizFuncXAtividades CreateMatriz(tblMatrizFuncXAtividades Matriz)
        {
            _db.tblMatrizFuncXAtividades.Add(Matriz);

            _db.SaveChanges();

            return Matriz;
        }

        public tblMatrizFuncXAtividades DeleteMatriz(int idMWz, int idFunc, int idActiv)
        {
            tblMatrizFuncXAtividades Matriz;

            var query = from f in _db.tblMatrizFuncXAtividades
                        where f.idMatrizWorkzone == idMWz &&
                       f.idFuncionario == idFunc &&
                       f.idAtividade == idActiv 
                        select f;

            Matriz = query.FirstOrDefault();

            _db.tblMatrizFuncXAtividades.Remove(Matriz);
            _db.SaveChanges();

            return Matriz;
        }


        public tblMatrizFuncXAtividades UpdateMatriz(tblMatrizFuncXAtividades Matriz)
        {
            var matrizToUpdate = _db.tblMatrizFuncXAtividades.Find(Matriz.idMatrizFuncAtiv);
            matrizToUpdate.idFuncionario = Matriz.idFuncionario;
            matrizToUpdate.idItemPerfil = Matriz.idItemPerfil;
            matrizToUpdate.idAtividade = Matriz.idAtividade;
            matrizToUpdate.idMatrizWorkzone = Matriz.idMatrizWorkzone;

            _db.Entry(matrizToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizToUpdate;
        }
    }
}