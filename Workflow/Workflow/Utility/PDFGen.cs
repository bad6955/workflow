using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using EO.Pdf;

namespace Workflow.Utility
{
    public static class PDFGen
    {
        public static void CreateHTMLPDF(string html, string fileName)
        {
            string path = "./PDFGen/";
            string fullPath = String.Format("{0}{1}", path, fileName);
            Byte[] pdfData = null;
            using (MemoryStream m = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(m);
                pdfData = m.ToArray();
            }
            File.WriteAllBytes(fullPath, pdfData);
        }
    }
}
