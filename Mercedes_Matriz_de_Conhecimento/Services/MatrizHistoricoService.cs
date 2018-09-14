using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class MatrizHistoricoService
    {

        public DbConnection _db = new DbConnection();


        public IEnumerable<tblMatrizWorkzoneHistorico> GetMatrizHistoricoByDate(DateTime dateIni, DateTime dateEnd, string wzName)
        {

            dateEnd = dateEnd.AddHours(23).AddMinutes(59);
            IEnumerable<tblMatrizWorkzoneHistorico> matrizHistorico;

            if (wzName.Length == 0)
            {
                matrizHistorico = _db.tblMatrizWorkzoneHistorico
                    .Where(m => m.DataCriacao >= dateIni && m.DataCriacao <= dateEnd);

                return matrizHistorico;
            }
            else
            {
                matrizHistorico = _db.tblMatrizWorkzoneHistorico
                    .Where(m => m.DataCriacao >= dateIni && m.DataCriacao <= dateEnd && m.nomeWorkzone == wzName);
                return matrizHistorico;
            }
        }

        public List<string> GetWorkzoneName()
        {
            var query = _db.tblMatrizWorkzoneHistorico
                 .Select(m => m.nomeWorkzone).Distinct().ToList();

            return query;
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

        public tblMatrizFuncActivityHistorico getMatrizHistoricoActivityByWZIdFuncAtiv(int idMWZ, int idFunc, int idActv)
        {
            var query = _db.tblMatrizFuncActivityHistorico
                .Where(wh => wh.idMatrizWorkzoneHistorico == idMWZ &&
                wh.idAtividade == idActv
                && wh.idFuncionario == idFunc);

            return query.FirstOrDefault();
        }

        public tblMatrizFuncTreinHistorico getMatrizHistoricoTrainingByWZIdFuncAtiv(int idMWZ, int idFunc, int idTrein)
        {
            var query = _db.tblMatrizFuncTreinHistorico
                .Where(wh => wh.idMatrizWorkzoneHistorico == idMWZ &&
                wh.idTreinamento == idTrein
                && wh.idFuncionario == idFunc);

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

        public void SalvarTreinAllHistorico(List<tblMatrizFuncTreinHistorico> trainingHistorico)
        {

            _db.tblMatrizFuncTreinHistorico.AddRange(trainingHistorico);

            _db.SaveChanges();
        }


        public void SalvarActivityHistorico(tblMatrizFuncActivityHistorico actvityHistorico)
        {
            _db.tblMatrizFuncActivityHistorico.Add(actvityHistorico);

            _db.SaveChanges();
        }

        public void SalvarActivityAllHistorico(List<tblMatrizFuncActivityHistorico> actvityHistorico)
        {
            _db.tblMatrizFuncActivityHistorico.AddRange(actvityHistorico);

            _db.SaveChanges();
        }

    }
}