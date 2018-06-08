using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;

namespace iText7Console
{
    public class PdfUtils
    {
        public static void HelloWorld(string outputFilePath)
        {
            PdfWriter writer = new PdfWriter(outputFilePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            document.Add(new Paragraph("Hello World!"));
            document.Close();
        }

        public static void InsertParagraph(string outputFilePath)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(outputFilePath);

            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            // Initialize document
            Document document = new Document(pdf);

            // Create a PdfFont
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            // Add a Paragraph
            document.Add(new Paragraph("iText is:").SetFont(font));
            // Create a List
            List list = new List()
                .SetSymbolIndent(2)
                .SetListSymbol("\u2022")
                .SetFont(font);
            // Add ListItem objects
            list.Add(new ListItem("Never gonna give you up"))
                .Add(new ListItem("Never gonna let you down"))
                .Add(new ListItem("Never gonna run around and desert you"))
                .Add(new ListItem("Never gonna make you cry"))
                .Add(new ListItem("Never gonna say goodbye"))
                .Add(new ListItem("Never gonna tell a lie and hurt you"));
            // Add the list
            document.Add(list);

            //Close document
            document.Close();
        }

        public static void InsertImage(string outputFilePath)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(outputFilePath);

            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            // Initialize document
            Document document = new Document(pdf);

            // Compose Paragraph
            Image angular = new Image(ImageDataFactory.Create(@"Data/Angular.png"), 30, 650, 100);
            Image typescript = new Image(ImageDataFactory.Create(@"Data/typescript.png"), 130, 660, 80);
            Paragraph p = new Paragraph("Angular ")
                .Add(angular)
                .Add(" TypeScript ")
                .Add(typescript)
                .Add("Have you ever wondered how a headless Content Management System fits in with Progressive Web Apps?");
            // Add Paragraph to document
            document.Add(p);

            // Add Another Paragraph
            p = new Paragraph("You could install this app on your device. It uses a service worker to cache the application and data about the points of interest. The application was written in plain JavaScript.");
            document.Add(p);

            //Close document
            document.Close();
        }

        public static void DisplayDataTable(string outputFilePath)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(outputFilePath);

            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            // Initialize document
            Document document = new Document(pdf, PageSize.LETTER.Rotate());
            document.SetMargins(20, 20, 20, 20);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Table table = new Table(new float[] { 4, 1, 3, 4, 3, 3, 3, 3, 1 });
            //table.SetWidthPercent(100); // not working. 
            table.SetWidth(pdf.GetDefaultPageSize().GetWidth() - 40);
            var lines = System.IO.File.ReadAllLines(@"Data/united_states.csv").ToList();
            Process(table, lines[0], bold, true);
            lines.RemoveAt(0);
            foreach (var line in lines)
            {
                Process(table, line, font, false);
            }

            document.Add(table);

            //Close document
            document.Close();
        }

        private static void Process(Table table, string line, PdfFont font, bool isHeader)
        {
            StringTokenizer tokenizer = new StringTokenizer(line, ";");
            while (tokenizer.HasMoreTokens())
            {
                if (isHeader)
                {
                    table.AddHeaderCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
                }
                else
                {
                    table.AddCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
                }
            }
        }

        public static void DrawAxes(string outputFilePath)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outputFilePath));
            PageSize ps = PageSize.LETTER.Rotate();
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);

            //Replace the origin of the coordinate system to the center of the page
            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2); // no effect
            DrawAxes(canvas, ps);
            //Close document
            pdf.Close();
        }

        private static void DrawAxes(PdfCanvas canvas, PageSize ps)
        {
            //Draw X axis
            canvas.MoveTo(-(ps.GetWidth() / 2 - 15), 0).LineTo(ps.GetWidth() / 2 - 15, 0).Stroke();
            //Draw X axis arrow
            canvas.SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND)
                .MoveTo(ps.GetWidth() / 2 - 25, -10)
                .LineTo(ps.GetWidth() / 2 - 15, 0)
                .LineTo(ps.GetWidth() / 2 - 25, 10)
                .Stroke()
                .SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.MITER);
            //Draw Y axis
            canvas.MoveTo(0, -(ps.GetHeight() / 2 - 15)).LineTo(0, ps.GetHeight() / 2 - 15).Stroke();
            //Draw Y axis arrow
            canvas.SaveState()
                .SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND)
                .MoveTo(-10, ps.GetHeight() / 2 - 25)
                .LineTo(0, ps.GetHeight() / 2 - 15)
                .LineTo(10, ps.GetHeight() / 2 - 25)
                .Stroke()
                .RestoreState();
            //Draw X serif
            for (int i = -((int)ps.GetWidth() / 2 - 61); i < ((int)ps.GetWidth() / 2 - 60); i += 40)
            {
                canvas.MoveTo(i, 5).LineTo(i, -5);
            }
            //Draw Y serif
            for (int j = -((int)ps.GetHeight() / 2 - 57); j < ((int)ps.GetHeight() / 2 - 56); j += 40)
            {
                canvas.MoveTo(5, j).LineTo(-5, j);
            }
            canvas.Stroke();
        }

        public static void DrawGridLines(string outputFilePath)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outputFilePath));
            PageSize ps = PageSize.LETTER.Rotate();
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);
            //Replace the origin of the coordinate system to the center of the page
            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2);
            Color grayColor = new DeviceCmyk(0f, 0f, 0f, 0.875f);
            Color greenColor = new DeviceCmyk(1f, 0f, 1f, 0.176f);
            Color blueColor = new DeviceCmyk(1f, 0.156f, 0f, 0.118f);
            canvas.SetLineWidth(0.5f).SetStrokeColor(blueColor);
            //Draw horizontal grid lines
            for (int i = -((int)ps.GetHeight() / 2 - 57); i < ((int)ps.GetHeight() / 2 - 56); i += 40)
            {
                canvas.MoveTo(-(ps.GetWidth() / 2 - 15), i).LineTo(ps.GetWidth() / 2 - 15, i);
            }
            //Draw vertical grid lines
            for (int j = -((int)ps.GetWidth() / 2 - 61); j < ((int)ps.GetWidth() / 2 - 60); j += 40)
            {
                canvas.MoveTo(j, -(ps.GetHeight() / 2 - 15)).LineTo(j, ps.GetHeight() / 2 - 15);
            }
            canvas.Stroke();
            //Draw axes
            canvas.SetLineWidth(3).SetStrokeColor(grayColor);
            DrawAxes(canvas, ps);
            //Draw plot
            canvas.SetLineWidth(2)
                .SetStrokeColor(greenColor)
                .SetLineDash(10, 10, 8)
                .MoveTo(-(ps.GetWidth() / 2 - 15), -(ps.GetHeight() / 2 - 15))
                .LineTo(ps.GetWidth() / 2 - 15, ps.GetHeight() / 2 - 15)
                .Stroke();
            //Close document
            pdf.Close();
        }

        public static void PrintStarWars(string outputFilePath)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outputFilePath));
            //Add new page
            PageSize ps = PageSize.LETTER;
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);
            var text = new List<string>
            {
                "         Episode V         ",
                "  THE EMPIRE STRIKES BACK  ",
                "It is a dark time for the",
                "Rebellion. Although the Death",
                "Star has been destroyed,",
                "Imperial troops have driven the",
                "Rebel forces from their hidden",
                "base and pursued them across",
                "the galaxy.",
                "Evading the dreaded Imperial",
                "Starfleet, a group of freedom",
                "fighters led by Luke Skywalker",
                "has established a new secret",
                "base on the remote ice world",
                "of Hoth..."
            };
            //Replace the origin of the coordinate system to the top left corner
            canvas.ConcatMatrix(1, 0, 0, 1, 0, ps.GetHeight());
            canvas.BeginText()
                .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD), 14)
                .SetLeading(14 * 1.5f)
                .MoveText(200, -40);
            foreach (var s in text)
            {
                //Add text and move to the next line
                canvas.NewlineShowText(s);
            }
            canvas.EndText();
            //Close document
            pdf.Close();
        }

        public static void PrintStarWarsCrawl(string dest)
        {
            var text = new List<string>
            {
                "            Episode V      ",
                "    THE EMPIRE STRIKES BACK  ",
                "It is a dark time for the",
                "Rebellion. Although the Death",
                "Star has been destroyed,",
                "Imperial troops have driven the",
                "Rebel forces from their hidden",
                "base and pursued them across",
                "the galaxy.",
                "Evading the dreaded Imperial",
                "Starfleet, a group of freedom",
                "fighters led by Luke Skywalker",
                "has established a new secret",
                "base on the remote ice world",
                "of Hoth..."
            };
            var maxStringWidth = text.Select(fragment => fragment.Length).Max();

            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            //Add new page
            PageSize ps = PageSize.A4;
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);
            //Set black background
            canvas.Rectangle(0, 0, ps.GetWidth(), ps.GetHeight()).SetColor(DeviceRgb.BLACK, true).Fill();
            //Replace the origin of the coordinate system to the top left corner
            canvas.ConcatMatrix(1, 0, 0, 1, 0, ps.GetHeight());
            Color yellowColor = new DeviceCmyk(0f, 0.0537f, 0.769f, 0.051f);
            float lineHeight = 5;
            float yOffset = -40;
            canvas.BeginText()
                .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD), 1)
                .SetColor(yellowColor, true);
            for (var j = 0; j < text.Count; j++)
            {
                var line = text[j];
                float xOffset = ps.GetWidth() / 2 - 45 - 8 * j;
                float fontSizeCoeff = 6 + j;
                float lineSpacing = (lineHeight + j) * j / 1.5f;
                var stringWidth = line.Length;
                for (var i = 0; i < stringWidth; i++)
                {
                    var angle = (maxStringWidth / 2 - i) / 2f;
                    var charXOffset = (4 + (float)j / 2) * i;
                    canvas.SetTextMatrix(
                        fontSizeCoeff, 0,
                        angle, fontSizeCoeff / 1.5f,
                        xOffset + charXOffset, yOffset - lineSpacing
                        )
                        .ShowText(line[i].ToString());
                }
            }
            canvas.EndText();
            //Close document
            pdf.Close();
        }
    }
}
