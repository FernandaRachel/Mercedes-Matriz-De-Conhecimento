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
    public class MatrizFuncAtividadeTempService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizFuncXAtividadesTemp GetMatrizTempById(int id)
        {
            tblMatrizFuncXAtividadesTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        where f.idMatrizFuncAtivTemp == id
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public IEnumerable<tblMatrizFuncXAtividadesTemp> GetMatrizTempByIdMWZ(int idMWZ)
        {
            IEnumerable<tblMatrizFuncXAtividadesTemp> MatrizTemp;

            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        where f.idMatrizWorkzoneTemp == idMWZ
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query;

            return MatrizTemp;
        }

        public tblMatrizFuncXAtividadesTemp GetMatrizTempByFuncXAtiv(int idMWZ,int idActivity, int idFunc)
        {
            tblMatrizFuncXAtividadesTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        where f.idMatrizWorkzoneTemp == idMWZ &&
                        f.idAtividade == idActivity 
                        && f.idFuncionario == idFunc
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public IEnumerable<tblMatrizFuncXAtividadesTemp> GetMatriz()
        {
            IEnumerable<tblMatrizFuncXAtividadesTemp> MatrizTemp;



            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        orderby f.tblMatrizWorkzoneTemp.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.AsEnumerable();

            return MatrizTemp.AsEnumerable();
        }


        public tblMatrizFuncXAtividadesTemp CreateMatrizTemp(tblMatrizFuncXAtividadesTemp MatrizTemp)
        {
            _db.tblMatrizFuncXAtividadesTemp.Add(MatrizTemp);

            _db.SaveChanges();

            return MatrizTemp;
        }

        public tblMatrizFuncXAtividadesTemp DeleteMatrizTemp(int idMWz, int idFunc, int idActiv)
        {
            tblMatrizFuncXAtividadesTemp MatrizTemp;

            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        where f.idMatrizWorkzoneTemp == idMWz &&
                       f.idFuncionario == idFunc &&
                       f.idAtividade == idActiv 
                        select f;

            MatrizTemp = query.FirstOrDefault();

            _db.tblMatrizFuncXAtividadesTemp.Remove(MatrizTemp);
            _db.SaveChanges();

            return MatrizTemp;
        }

        public void DeleteMatrizTempAll(int idMWz)
        {
            var query = from f in _db.tblMatrizFuncXAtividadesTemp
                        where f.idMatrizWorkzoneTemp == idMWz 
                        select f;


            _db.tblMatrizFuncXAtividadesTemp.RemoveRange(query);
            _db.SaveChanges();
        }

        public tblMatrizFuncXAtividadesTemp UpdateMatrizTemp(tblMatrizFuncXAtividadesTemp MatrizTemp)
        {
            var matrizTempToUpdate = _db.tblMatrizFuncXAtividadesTemp.Find(MatrizTemp.idMatrizFuncAtivTemp);
            matrizTempToUpdate.idFuncionario = MatrizTemp.idFuncionario;
            matrizTempToUpdate.idItemPerfil = MatrizTemp.idItemPerfil;
            matrizTempToUpdate.idAtividade = MatrizTemp.idAtividade;
            matrizTempToUpdate.cor = MatrizTemp.cor;
            matrizTempToUpdate.alocacaoForcada = MatrizTemp.alocacaoForcada;
            matrizTempToUpdate.idMatrizWorkzoneTemp = MatrizTemp.idMatrizWorkzoneTemp;

            _db.Entry(matrizTempToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizTempToUpdate;
        }
    }
}