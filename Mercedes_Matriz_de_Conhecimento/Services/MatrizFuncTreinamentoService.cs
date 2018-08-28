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
    public class MatrizFuncTreinamentoService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizFuncXTreinamento GetMatrizById(int id)
        {
            tblMatrizFuncXTreinamento Matriz;

            var query = from f in _db.tblMatrizFuncXTreinamento
                        where f.idMatrizFuncTrein == id
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public IEnumerable<tblMatrizFuncXTreinamento> GetMatrizByIdMWZ(int idMWZ)
        {
            IEnumerable<tblMatrizFuncXTreinamento> Matriz;

            var query = from f in _db.tblMatrizFuncXTreinamento
                        where f.idMatrizWorkzone == idMWZ
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query;

            return Matriz;
        }

        public tblMatrizFuncXTreinamento GetMatrizByFuncXTrain(int idTrain, int idFunc)
        {
            tblMatrizFuncXTreinamento Matriz;

            var query = from f in _db.tblMatrizFuncXTreinamento
                        where f.idTreinamento == idTrain && f.idFuncionario == idFunc
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            return Matriz;
        }

        public IEnumerable<tblMatrizFuncXTreinamento> GetMatriz()
        {
            IEnumerable<tblMatrizFuncXTreinamento> Matriz;



            var query = from f in _db.tblMatrizFuncXTreinamento
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.AsEnumerable();

            return Matriz.AsEnumerable();
        }



        public tblMatrizFuncXTreinamento CreateMatriz(tblMatrizFuncXTreinamento Matriz)
        {
            _db.tblMatrizFuncXTreinamento.Add(Matriz);

            _db.SaveChanges();

            return Matriz;
        }

        public List<tblMatrizFuncXTreinamento> CreateAllMatriz(List<tblMatrizFuncXTreinamento> Matriz)
        {
            
                _db.tblMatrizFuncXTreinamento.AddRange(Matriz);

                _db.SaveChanges();

            return Matriz;
        }

        public tblMatrizFuncXTreinamento DeleteMatriz(int idMWz, int idFunc, int idTrain)
        {
            tblMatrizFuncXTreinamento Matriz;

            var query = from f in _db.tblMatrizFuncXTreinamento
                        where f.idMatrizWorkzone == idMWz &&
                        f.idFuncionario == idFunc &&
                        f.idTreinamento == idTrain
                        orderby f.tblMatrizWorkzone.tblWorkzone.Nome
                        select f;

            Matriz = query.FirstOrDefault();

            _db.tblMatrizFuncXTreinamento.Remove(Matriz);
            _db.SaveChanges();

            return Matriz;
        }

        public void DeleteMatrizAll(int idMWz)
        {
            var query = from f in _db.tblMatrizFuncXTreinamento
                        where f.idMatrizWorkzone == idMWz
                        select f;

            if (query.Count() > 0)
            {
                _db.tblMatrizFuncXTreinamento.RemoveRange(query);
                _db.SaveChanges();
            }

        }

        public tblMatrizFuncXTreinamento UpdateMatriz(tblMatrizFuncXTreinamento Matriz)
        {
            var matrizToUpdate = _db.tblMatrizFuncXTreinamento.Find(Matriz.idMatrizFuncTrein);
            matrizToUpdate.idFuncionario = Matriz.idFuncionario;
            matrizToUpdate.idItemPerfil = Matriz.idItemPerfil;
            matrizToUpdate.idTreinamento = Matriz.idTreinamento;
            matrizToUpdate.idMatrizWorkzone = Matriz.idMatrizWorkzone;

            _db.Entry(matrizToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizToUpdate;
        }
    }
}