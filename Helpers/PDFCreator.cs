using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using Codaxy.WkHtmlToPdf;

namespace ResumeStripper.Helpers
{
    public class PDFCreator
    {
        public PdfDocument getAnonymousPdf(string HtmlCode, int id, string name)
        {
            return GetPdfFromHtml(HtmlCode, true, id, name);
        }

        public PdfDocument getNonymousPdf(string Htmlcode, int id, string name)
        {
            return GetPdfFromHtml(Htmlcode, false, id, name);
        }

        private PdfDocument GetPdfFromHtml(string HtmlCode, bool anonymous, int id, string name)
        {
            PdfOutput output = new PdfOutput();
            if (anonymous)
            {
                //Result must be anonymous
                output = new PdfOutput
                {
                    OutputFilePath = "CV_" + id.ToString() + ".pdf"
                };
            }
            else
            {
                //result must not be anonymous
                output = new PdfOutput
                {
                    OutputFilePath = "CV_" + id.ToString() + "_" + name + ".pdf"
                };
            }

            PdfDocument document = new PdfDocument();

            PdfConvert.ConvertHtmlToPdf(document, output);

            return document;
        }
    }
}