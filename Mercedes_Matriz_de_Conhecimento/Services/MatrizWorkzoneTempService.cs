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
    public class MatrizWorkzoneTempService
    {

        public DbConnection _db = new DbConnection();



        public tblMatrizWorkzoneTemp GetMatrizTempById(int id)
        {
            tblMatrizWorkzoneTemp MatrizTemp;

            var query = from f in _db.tblMatrizWorkzoneTemp
                        where f.idMatrizWZTemp == id
                        orderby f.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public tblMatrizWorkzoneTemp GetMatrizTempByWZId(int idWz)
        {
            tblMatrizWorkzoneTemp MatrizTemp;

            var query = from f in _db.tblMatrizWorkzoneTemp
                        where f.idWorkzone == idWz
                        orderby f.tblWorkzone.Nome
                        select f;

            MatrizTemp = query.FirstOrDefault();

            return MatrizTemp;
        }

        public IEnumerable<tblMatrizWorkzoneTemp> GetMatrizTemp()
        {
            IEnumerable<tblMatrizWorkzoneTemp> MatrizTemp;



            var query = from f in _db.tblMatrizWorkzoneTemp
                        orderby f.tblWorkzone.Nome ascending
                        select f;

            MatrizTemp = query.AsEnumerable();

            return MatrizTemp.AsEnumerable();
        }


        public tblMatrizWorkzoneTemp CreateMatrizTemp(tblMatrizWorkzoneTemp MatrizTemp)
        {
            var exist = GetMatrizTempByWZId(MatrizTemp.idWorkzone);

            if (exist == null)
            {

                _db.tblMatrizWorkzoneTemp.Add(MatrizTemp);

                _db.SaveChanges();

                var matrizCreated = _db.tblMatrizWorkzoneTemp
                   .OrderByDescending(w => w.DataCriacao)
                   .FirstOrDefault();

                return matrizCreated;
            }
            else
            {
                return exist;
            }
        }

        public tblMatrizWorkzoneTemp DeleteMatrizTemp(int id)
        {
            tblMatrizWorkzoneTemp MatrizTemp;

            var query = from f in _db.tblMatrizWorkzoneTemp
                        where f.idMatrizWZTemp == id
                        orderby f.tblWorkzone.Nome ascending
                        select f;

            MatrizTemp = query.FirstOrDefault();

            _db.tblMatrizWorkzoneTemp.Remove(MatrizTemp);
            _db.SaveChanges();

            return MatrizTemp;
        }


        public tblMatrizWorkzoneTemp UpdateMatrizTemp(tblMatrizWorkzoneTemp MatrizTemp)
        {
            var matrizToUpdateTemp = _db.tblMatrizWorkzoneTemp.Find(MatrizTemp.idMatrizWZTemp);
            matrizToUpdateTemp.idWorkzone = MatrizTemp.idWorkzone;
            matrizToUpdateTemp.Usuario = MatrizTemp.Usuario;
            matrizToUpdateTemp.DataCriacao = MatrizTemp.DataCriacao;

            _db.Entry(matrizToUpdateTemp).State = EntityState.Modified;
            _db.SaveChanges();


            return matrizToUpdateTemp;
        }
    }
}