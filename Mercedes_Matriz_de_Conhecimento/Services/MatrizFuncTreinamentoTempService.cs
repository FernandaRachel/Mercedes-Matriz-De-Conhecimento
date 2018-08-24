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
    public class MatrizFuncTreinamentoTempService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizFuncXTreinamentoTemp GetMatrizTempById(int id)
        {
            tblMatrizFuncXTreinamentoTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        where f.idMatrizFuncTreinTemp == id
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public IEnumerable<tblMatrizFuncXTreinamentoTemp> GetMatrizTempByIdMWZ(int idMWZ)
        {
            IEnumerable<tblMatrizFuncXTreinamentoTemp> MatrizTemp;

            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        where f.idMatrizWorkzoneTemp == idMWZ
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query;

            return MatrizTemp;
        }

        public tblMatrizFuncXTreinamentoTemp GetMatrizTempByFuncXTrain(int idTrain, int idFunc)
        {
            tblMatrizFuncXTreinamentoTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        where f.idTreinamento == idTrain && f.idFuncionario == idFunc
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public IEnumerable<tblMatrizFuncXTreinamentoTemp> GetMatrizTemp()
        {
            IEnumerable<tblMatrizFuncXTreinamentoTemp> MatrizTemp;



            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.AsEnumerable();

            return MatrizTemp.AsEnumerable();
        }


        public tblMatrizFuncXTreinamentoTemp CreateMatrizTemp(tblMatrizFuncXTreinamentoTemp MatrizTemp)
        {
            _db.tblMatrizFuncXTreinamentoTemp.Add(MatrizTemp);

            _db.SaveChanges();

            return MatrizTemp;
        }

        public tblMatrizFuncXTreinamentoTemp DeleteMatrizTemp(int idMWz, int idFunc, int idTrain)
        {
            tblMatrizFuncXTreinamentoTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        where f.idMatrizWorkzoneTemp == idMWz &&
                        f.idFuncionario == idFunc &&
                        f.idTreinamento == idTrain
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            _db.tblMatrizFuncXTreinamentoTemp.Remove(MatrizTemp);
            _db.SaveChanges();

            return MatrizTemp;
        }

        public void DeleteMatrizTempAll(int idMWz)
        {
            var query = from f in _db.tblMatrizFuncXTreinamentoTemp
                        where f.idMatrizWorkzoneTemp == idMWz
                        select f;


            _db.tblMatrizFuncXTreinamentoTemp.RemoveRange(query);
            _db.SaveChanges();
        }


        public tblMatrizFuncXTreinamentoTemp UpdateMatriz(tblMatrizFuncXTreinamentoTemp MatrizTemp)
        {
            var matrizToUpdate = _db.tblMatrizFuncXTreinamentoTemp.Find(MatrizTemp.idMatrizFuncTreinTemp);
            matrizToUpdate.idFuncionario = MatrizTemp.idFuncionario;
            matrizToUpdate.idItemPerfil = MatrizTemp.idItemPerfil;
            matrizToUpdate.idTreinamento = MatrizTemp.idTreinamento;
            matrizToUpdate.idMatrizWorkzoneTemp = MatrizTemp.idMatrizWorkzoneTemp;

            _db.Entry(matrizToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizToUpdate;
        }
    }
}