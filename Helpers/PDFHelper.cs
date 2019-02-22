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
using Bytescout.PDFExtractor;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Text;

namespace ResumeStripper.Helpers
{
    public class PDFHelper
    {
        //public PdfDocument getAnonymousPdf(string HtmlCode, int id, string name)
        //{
        //    return GetPdfFromHtml(HtmlCode, true, id, name);
        //}

        //public PdfDocument getNonymousPdf(string Htmlcode, int id, string name)
        //{
        //    return GetPdfFromHtml(Htmlcode, false, id, name);
        //}

        //private PdfDocument GetPdfFromHtml(string HtmlCode, bool anonymous, int id, string name)
        //{
        //    PdfOutput output = new PdfOutput();
        //    if (anonymous)
        //    {
        //        //Result must be anonymous
        //        output = new PdfOutput
        //        {
        //            OutputFilePath = "CV_" + id.ToString() + ".pdf"
        //        };
        //    }
        //    else
        //    {
        //        //result must not be anonymous
        //        output = new PdfOutput
        //        {
        //            OutputFilePath = "CV_" + id.ToString() + "_" + name + ".pdf"
        //        };
        //    }

        //    PdfDocument document = new PdfDocument();

        //    PdfConvert.ConvertHtmlToPdf(document, output);

        //    return document;
        //}

        public string getTextFromPdf(string path, string fileName)
        {
            string plainText = "";
            //string filename = "";

            //if (fileName.Contains(":"))
            //{
            //    //edge filename
            //    filename = Path.GetFileName(fileName);
            //} else
            //{
            //    filename = fileName;
            //}



            //using (var pdfStream = File.OpenRead(path))
            //using (var extractor = new Extractor())
            //{
            //    plainText = extractor.ExtractToString(pdfStream);
            //}



            return plainText;
        }

        public string GetText(string path)
        {
            string plainText = "";

            using (var pdfStream = File.OpenRead(path))
            using (var extractor = new Extractor())
            {
                plainText = extractor.ExtractToString(pdfStream);
            }
            return plainText;
        }

        public string GetHTMLText(string path)
        {
            PdfFocus f = new PdfFocus();
            f.OpenPdf(path);
            f.HtmlOptions.PreserveImages = true;
            f.HtmlOptions.IncludeImageInHtml = true;
            //f.HtmlOptions.SingleFontFamily = "Arial";

            string result = f.ToHtml().ToString();
            string fix = Regex.Replace(result, @"\s+", " ");

            return fix;
           
        }
    }
}