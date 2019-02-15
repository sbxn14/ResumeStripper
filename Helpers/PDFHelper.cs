using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using Codaxy.WkHtmlToPdf;
using PdfExtract;
using SautinSoft;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ResumeStripper.Helpers
{
    public class PDFHelper
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

        public string getTextFromPdf(HttpPostedFileBase file)
        {
            string plainText = "";
            //using (var pdfStream = File.OpenRead(file.FileName))
            //using (var extractor = new Extractor())
            //{
            //    plainText = extractor.ExtractToString(pdfStream);
            //}
            //return plainText;
            //PdfFocus f = new PdfFocus();

            //f.OpenPdf(file.FileName);
            //if(f.PageCount > 0)
            //{
            //    plainText = f.ToText().ToString();
            //}
            

            return GetDocxFromPdf(file);
           // return plainText;
        }

        public string GetDocxFromPdf(HttpPostedFileBase file)
        {
            string plainText = "";
            PdfFocus f = new PdfFocus();

            f.OpenPdf(file.FileName);
            if (f.PageCount > 0)
            {
                //f.HtmlOptions.IncludeImageInHtml = true;
                //f.HtmlOptions.PreserveImages = false;
                //f.HtmlOptions.InlineCSS = true;
                //f.HtmlOptions.SingleFontSize = 14;
                //plainText = f.ToHtml();

                plainText = f.ToText();
            }
            return plainText;
        }
    }
}