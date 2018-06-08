using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iText7Console.Chapter3
{
    public class NewYorkTimes
    {
        private static readonly PdfFont TimesNewRoman = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
        private static readonly PdfFont TimesNewRomanBold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

        public static void Generate(string dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = PageSize.A5;
            // Initialize document
            Document document = new Document(pdf, ps);
            //Set column parameters
            const int offSet = 36;
            float columnWidth = (ps.GetWidth() - offSet * 2 + 10) / 3;
            float columnHeight = ps.GetHeight() - offSet * 2;
            //Define column areas
            Rectangle[] columns = {
                new Rectangle(offSet - 5, offSet, columnWidth, columnHeight),
                new Rectangle(offSet + columnWidth, offSet, columnWidth, columnHeight),
                new Rectangle(offSet + columnWidth * 2 + 5, offSet, columnWidth, columnHeight)
            };
            //
            document.SetRenderer(new ColumnDocumentRenderer(document, columns));
            var apple = new Image(ImageDataFactory.Create(@"data/ny_times_apple.jpg")).SetWidth(columnWidth);
            var articleApple = File.ReadAllText(@"data/ny_times_apple.txt", Encoding.UTF8);
            AddArticle(document, "Apple Encryption Engineers, if Ordered to Unlock iPhone, Might Resist"
                , "By JOHN MARKOFF MARCH 18, 2016", apple, articleApple);

            var facebook = new Image(ImageDataFactory.Create(@"data/ny_times_fb.jpg")).SetWidth(columnWidth);
            var articleFb = File.ReadAllText(@"data/ny_times_fb.txt", Encoding.UTF8);
            AddArticle(document, "With \"Smog Jog\" Through Beijing, Zuckerberg Stirs Debate on Air Pollution"
                , "By PAUL MOZUR MARCH 18, 2016", facebook, articleFb);

            var inst = new Image(ImageDataFactory.Create(@"data/ny_times_inst.jpg")).SetWidth(columnWidth);
            var articleInstagram = File.ReadAllText(@"data/ny_times_inst.txt", Encoding.UTF8);
            AddArticle(document, "Instagram May Change Your Feed, Personalizing It With an Algorithm"
               , "By MIKE ISAAC MARCH 15, 2016", inst, articleInstagram);

            document.Close();
        }
        private static void AddArticle(Document doc, string title, string author, Image img, string text)
        {
            var p1 = new Paragraph(title).SetFont(TimesNewRomanBold).SetFontSize(14);
            doc.Add(p1);
            doc.Add(img);
            var p2 = new Paragraph().SetFont(TimesNewRoman).SetFontSize(7).SetFontColor(DeviceGray.GRAY).Add(author);
            doc.Add(p2);
            var p3 = new Paragraph().SetFont(TimesNewRoman).SetFontSize(10).Add(text);
            doc.Add(p3);
        }

    }
}
