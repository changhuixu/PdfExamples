using System;
using System.Collections.Generic;
using System.Text;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Layout.Font;

namespace iText7Console
{
    public class HtmlTest
    {
        public static void Create(string dest)
        {

            PdfWriter pdfWriter = new PdfWriter(dest);

            PdfDocument pdfDoc = new PdfDocument(pdfWriter);

            // pdf conversion
            ConverterProperties props = new ConverterProperties();
            FontProvider fp = new FontProvider();
            fp.AddStandardPdfFonts();
            fp.AddDirectory(@"fonts");//The noto-nashk font file (.ttf extension) is placed in the resources

            props.SetFontProvider(fp);
            props.SetBaseUri(@"fonts");
            //Setup custom tagworker factory for better tagging of headers
            HtmlConverter.ConvertToPdf(@"<h1>Hello</h1><p>world</p>", pdfDoc, props);
            pdfDoc.Close();
        }
    }
}
