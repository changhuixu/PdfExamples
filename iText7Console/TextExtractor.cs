using System;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace iText7Console
{
    public class TextExtractor
    {
        public static void Read(string src)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(src));
            for (var i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                var str = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), new LocationTextExtractionStrategy());
                Console.WriteLine(str);
            }
            pdfDoc.Close();
        }
    }
}
