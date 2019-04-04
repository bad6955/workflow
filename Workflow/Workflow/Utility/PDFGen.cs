using System;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Workflow.Utility
{
    public static class PDFGen
    {
        public static void CreateHTMLPDF(string html, string fileName)
        {
            string path = "./PDFGen/";
            Directory.CreateDirectory(path);
            string fullPath = String.Format("{0}{1}.pdf", path, fileName);
            Byte[] pdfData = null;
            using (MemoryStream m = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(m);
                pdfData = m.ToArray();
            }
            File.WriteAllBytes(fullPath, pdfData);
        }
    }
}
