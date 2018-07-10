using System;
using System.Diagnostics;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    public static class PdfDocumentExamples
    {
        public static void CrateAndWriteToPdf()
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont(FontNames.Arial, 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            // Save the document...
            const string filename = "HelloWorld.pdf";
            document.Save(filename);
        }

        public static void CreateBarcodeAsText()
        {
            using (PdfDocument document = PdfReader.Open("BarCodeTest3.pdf", PdfDocumentOpenMode.Modify))
            {
                //create new pdf page
                PdfPage page = document.Pages[0];
                //page.Width = XUnit.FromMillimeter(210);
                //page.Height = XUnit.FromMillimeter(297);

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    //make sure the font is embedded
                    var options = new XPdfFontOptions(PdfFontEncoding.Unicode);

                    //declare a font for drawing in the PDF
                    XFont font = new XFont(FontNames.MrvCode39S, 20, XFontStyle.Regular, options);

                    XTextFormatter tf = new XTextFormatter(gfx);
                    tf.DrawString("*00112001*", font, XBrushes.Black, new XRect(10, 100, 20, 232), XStringFormats.TopLeft);
                    gfx.RotateAtTransform(270, new XPoint(page.Height / 2d, page.Width / 2d));
                    tf.DrawString("*00112001*", font, XBrushes.Black, new XRect(-30, -80, 100, 232), XStringFormats.TopLeft);
                    //var stringFormat = new XStringFormat();
                    ////create the barcode from string
                    //var point1 = new XPoint(10, 10);
                    //var point2 = new XPoint(100, 130);
                    //gfx.DrawString("*00112001*", font, XBrushes.Black, new XRect(40, 100, 250, 232), stringFormat);
                    //gfx.RotateAtTransform(-90, new XPoint(page.Height / 2d, page.Width / 2d));
                    ////create the barcode from string
                    //gfx.DrawString("*00112001*", font, XBrushes.Black, new XRect(point1, point2), stringFormat);
                }


                document.Save("BarCodeTest38.pdf");
            }
        }
    }
}
