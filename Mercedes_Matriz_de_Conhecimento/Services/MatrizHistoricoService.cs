using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class MatrizHistoricoService
    {

        public DbConnection _db = new DbConnection();


        public IEnumerable<tblMatrizWorkzoneHistorico> GetMatrizHistorico()
        {

            IEnumerable<tblMatrizWorkzoneHistorico> matrizHistorico;
            var query = _db.tblMatrizWorkzoneHistorico;

            matrizHistorico = query;

            return matrizHistorico;
        }

        public tblMatrizWorkzoneHistorico GetMatrizHistoricoById(int idMWZ)
        {

            tblMatrizWorkzoneHistorico matrizHistorico;
            var query = _db.tblMatrizWorkzoneHistorico
                .Where(wh => wh.idMatrizHistorico == idMWZ);

            matrizHistorico = query.FirstOrDefault();

            return matrizHistorico;
        }


        public tblMatrizWorkzoneHistorico getMatrizHistoricoByWZId(int idWZ)
        {
            var query = _db.tblMatrizWorkzoneHistorico
                .Where(wh => wh.idWorkzone == idWZ)
                .OrderByDescending(wh => wh.DataCriacao);

            return query.FirstOrDefault();
        }

        public tblMatrizWorkzoneHistorico SalvarHistoricoMatrizWorkzone(tblMatrizWorkzoneHistorico matrizHistorico)
        {

            _db.tblMatrizWorkzoneHistorico.Add(matrizHistorico);

            _db.SaveChanges();

            var matrizWorkzoneHistorico = getMatrizHistoricoByWZId(matrizHistorico.idWorkzone);

            return matrizWorkzoneHistorico;
        }

        public void SalvarTreinHistorico(tblMatrizFuncTreinHistorico trainingHistorico)
        {

            _db.tblMatrizFuncTreinHistorico.Add(trainingHistorico);

            _db.SaveChanges();
        }

        public void SalvarActivityHistorico(tblMatrizFuncActivityHistorico actvityHistorico)
        {
            _db.tblMatrizFuncActivityHistorico.Add(actvityHistorico);

            _db.SaveChanges();
        }

    }
}