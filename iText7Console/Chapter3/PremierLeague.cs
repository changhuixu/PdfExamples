using System.IO;
using iText.IO.Font.Constants;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText7Console.Chapter3
{
    public class PremierLeague
    {

        private static readonly Color GreenColor = new DeviceCmyk(0.78f, 0, 0.81f, 0.21f);
        private static readonly Color YellowColor = new DeviceCmyk(0, 0, 0.76f, 0.01f);
        private static readonly Color RedColor = new DeviceCmyk(0, 0.76f, 0.86f, 0.01f);
        private static readonly Color BlueColor = new DeviceCmyk(0.28f, 0.11f, 0, 0);

        public static void Generate(string dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = new PageSize(792, 612);       // Letter Size
            // Initialize document
            Document document = new Document(pdf, ps);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Table table = new Table(new[] { 1.5f, 7, 2, 2, 2, 2, 3, 4, 4, 2 });
            table.SetWidth(pdf.GetDefaultPageSize().GetWidth() - 72)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            var sr = File.OpenText(@"data/premier_league.csv");
            var line = sr.ReadLine();
            Process(table, line, bold, true);
            while ((line = sr.ReadLine()) != null)
            {
                Process(table, line, font, false);
            }
            sr.Close();
            document.Add(table);
            //Close document
            document.Close();
        }
        private static void Process(Table table, string line, PdfFont font, bool isHeader)
        {
            var tokenizer = new StringTokenizer(line, ";");
            var columnNumber = 0;
            while (tokenizer.HasMoreTokens())
            {
                if (isHeader)
                {
                    var cell = new Cell().Add(new Paragraph(tokenizer.NextToken()));
                    cell.SetNextRenderer(new RoundedCornersCellRenderer(cell));
                    cell.SetPadding(5).SetBorder(null);
                    table.AddHeaderCell(cell);
                }
                else
                {
                    columnNumber++;
                    var cell = new Cell().Add(new Paragraph(tokenizer.NextToken()));
                    cell.SetFont(font).SetBorder(new SolidBorder(DeviceRgb.BLACK, 0.5f));
                    switch (columnNumber)
                    {
                        case 4:
                            cell.SetBackgroundColor(GreenColor);
                            break;
                        case 5:
                            cell.SetBackgroundColor(YellowColor);
                            break;
                        case 6:
                            cell.SetBackgroundColor(RedColor);
                            break;
                        default:
                            cell.SetBackgroundColor(BlueColor);
                            break;
                    }
                    table.AddCell(cell);
                }
            }
        }

        private class RoundedCornersCellRenderer : CellRenderer
        {
            public RoundedCornersCellRenderer(Cell modelElement) : base(modelElement)
            {
            }

            public override void DrawBorder(DrawContext drawContext)
            {
                Rectangle rectangle = GetOccupiedAreaBBox();
                float llx = rectangle.GetX() + 1;
                float lly = rectangle.GetY() + 1;
                float urx = rectangle.GetX() + GetOccupiedAreaBBox().GetWidth() - 1;
                float ury = rectangle.GetY() + GetOccupiedAreaBBox().GetHeight() - 1;
                PdfCanvas canvas = drawContext.GetCanvas();
                float r = 4;
                float b = 0.4477f;
                canvas.MoveTo(llx, lly)
                    .LineTo(urx, lly)
                    .LineTo(urx, ury - r)
                    .CurveTo(urx, ury - r * b, urx - r * b, ury, urx - r, ury)
                    .LineTo(llx + r, ury)
                    .CurveTo(llx + r * b, ury, llx, ury - r * b, llx, ury - r)
                    .LineTo(llx, lly)
                    .Stroke();
                base.DrawBorder(drawContext);
            }

        }
    }
}
