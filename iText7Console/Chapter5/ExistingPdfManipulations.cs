using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText7Console.Chapter5
{
    public class ExistingPdfManipulations
    {
        public static void AddContent(string src, string dest)
        {
            //Initialize PDF document
            var pdfDoc = new PdfDocument(new PdfReader(src), new PdfWriter(dest));
            var document = new Document(pdfDoc);
            var n = pdfDoc.GetNumberOfPages();
            for (var i = 1; i <= n; i++)
            {
                var page = pdfDoc.GetPage(i);
                var pageSize = page.GetPageSize();
                var canvas = new PdfCanvas(page);
                //Draw header text
                canvas.BeginText()
                    .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 7)
                    .MoveText(pageSize.GetWidth() / 2 - 24, pageSize.GetHeight() - 10)
                    .ShowText("I want to believe")
                    .EndText();
                //Draw footer line
                canvas.SetStrokeColor(DeviceRgb.BLACK)
                    .SetLineWidth(.2f)
                    .MoveTo(pageSize.GetWidth() / 2 - 30, 20)
                    .LineTo(pageSize.GetWidth() / 2 + 30, 20)
                    .Stroke();
                //Draw page number
                canvas.BeginText()
                    .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 7)
                    .MoveText(pageSize.GetWidth() / 2 - 7, 10)
                    .ShowText(i.ToString())
                    .ShowText(" of ")
                    .ShowText(n.ToString())
                    .EndText();
                //Draw watermark
                var p = new Paragraph("CONFIDENTIAL").SetFontSize(60);
                canvas.SaveState();
                var gs1 = new PdfExtGState().SetFillOpacity(0.2f);
                canvas.SetExtGState(gs1);
                document.ShowTextAligned(p, pageSize.GetWidth() / 2, pageSize.GetHeight() / 2, pdfDoc.GetPageNumber(page),
                    TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
                canvas.RestoreState();
            }
            pdfDoc.Close();
        }
    }
}
