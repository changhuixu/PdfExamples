using System.IO;
using iText.IO.Font.Constants;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText7Console.Chapter3
{
    public class Ufo
    {
        private static readonly PdfFont Helvetica = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        private static readonly PdfFont HelveticaBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

        public static void Generate(string dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new MyEventHandler());
            // Initialize document
            Document document = new Document(pdf, PageSize.LETTER);
            var p = new Paragraph("List of reported UFO sightings in 20th century")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(HelveticaBold)
                .SetFontSize(14);
            document.Add(p);
            Table table = new Table(new float[] { 3, 5, 7, 4 });
           // table.SetWidth(pdf.GetDefaultPageSize().GetWidth() - 72);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            var sr = File.OpenText(@"data/ufo.csv");
            var line = sr.ReadLine();
            Process(table, line, HelveticaBold, true);
            while ((line = sr.ReadLine()) != null)
            {
                Process(table, line, Helvetica, false);
            }
            sr.Close();
            document.Add(table);
            document.Close();
        }

        private static void Process(Table table, string line, PdfFont font, bool isHeader)
        {
            var tokenizer = new StringTokenizer(line, ";");
            while (tokenizer.HasMoreTokens())
            {
                if (isHeader)
                {
                    table.AddHeaderCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)).SetFontSize(9).SetBorder
                        (new SolidBorder(DeviceRgb.BLACK, 0.5f)));
                }
                else
                {
                    table.AddCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)).SetFontSize(9).SetBorder(
                        new SolidBorder(DeviceRgb.BLACK, 0.5f)));
                }
            }
        }

        protected internal class MyEventHandler : IEventHandler
        {
            public virtual void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                int pageNumber = pdfDoc.GetPageNumber(page);
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                //Set background
                Color limeColor = new DeviceCmyk(0.208f, 0, 0.584f, 0);
                Color blueColor = new DeviceCmyk(0.445f, 0.0546f, 0, 0.0667f);
                pdfCanvas.SaveState()
                    .SetFillColor(pageNumber % 2 == 1 ? limeColor : blueColor)
                    .Rectangle(pageSize.GetLeft(), pageSize.GetBottom(), pageSize.GetWidth(), pageSize.GetHeight())
                    .Fill()
                    .RestoreState();
                //Add header and footer
                pdfCanvas.BeginText()
                    .SetFontAndSize(Helvetica, 9)
                    .MoveText(pageSize.GetWidth() / 2 - 60, pageSize.GetTop() - 20)
                    .ShowText("THE TRUTH IS OUT THERE")
                    .MoveText(60, -pageSize.GetTop() + 30)
                    .ShowText(pageNumber.ToString())
                    .EndText();
                //Add watermark
                Canvas canvas = new iText.Layout.Canvas(pdfCanvas, pdfDoc, page.GetPageSize());
                canvas.SetProperty(Property.FONT_COLOR, DeviceRgb.WHITE);
                canvas.SetProperty(Property.FONT_SIZE, 60);
                canvas.SetProperty(Property.FONT, HelveticaBold);
                //canvas.ShowTextAligned(new Paragraph("CONFIDENTIAL"), 298.0f, 421.0f, pageNumber,
                //    TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45.0f);      // not working
                pdfCanvas.Release();
            }
        }
    }
}
