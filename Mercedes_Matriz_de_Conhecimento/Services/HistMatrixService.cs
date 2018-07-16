using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    class HistMatrixService: IHistMatrixService
    {

        public DbConnection _db = new DbConnection();

        public void SavePdf(HttpPostedFileBase uploadFile, string FilePath)
        {
            HistoricoTeste thistory = new HistoricoTeste();

            byte[] tempFile = new byte[uploadFile.ContentLength];
            uploadFile.InputStream.Read(tempFile, 0, uploadFile.ContentLength);

            thistory.DateSaved = DateTime.Now;
            thistory.FileName = FilePath;
            thistory.Arquivo = tempFile;

            

            _db.HistoricoTeste.Add(thistory);
            _db.SaveChanges();

        }

        public ActionResult ReadPdf(int id)
        {
            var pdfFound = _db.HistoricoTeste.Find(id);
            
            MemoryStream ms = new MemoryStream(pdfFound.Arquivo, 0, 0, true, true);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;");
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            HttpContext.Current.Response.OutputStream.Flush();
            HttpContext.Current.Response.End();

            return new FileStreamResult(HttpContext.Current.Response.OutputStream, "application/vnd.ms-excel");
        }
    }
}
