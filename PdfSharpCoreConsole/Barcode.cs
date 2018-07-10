using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    public class Barcode
    {
        public static void Run()
        {
            using (PdfDocument document = PdfReader.Open("BarCodeTest3.pdf", PdfDocumentOpenMode.Modify))
            {
                //create new pdf page
                PdfPage page = document.Pages[0];
                //declare a font for drawing in the PDF
                XFont font = new XFont(FontNames.MrvCode39S, 15, XFontStyle.Regular);
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    gfx.DrawString("*00112001*", font, XBrushes.Red,
                        new XRect(72, 216, 0, 0), XStringFormats.BaseLineLeft);
                    gfx.DrawString("*00112001*", font, XBrushes.BlueViolet,
                        new XRect(0, 120, 0, 0), XStringFormats.BaseLineLeft);
                    // Define a rotation transformation at the center of the page.
                    gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                    gfx.RotateTransform(-90);
                    gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

                    gfx.DrawString("*00112001*", font, XBrushes.Red,
                        new XRect(72, 216, 0, 0), XStringFormats.BaseLineLeft);
                    // x1 = W/2 - H/2 + y0;
                    // y1 = H/2 + W/2 - x0;
                    // letter size  x1 = y0 - 90;   y1 = 702 - x0;
                    // eg, (x1,y1)=(30,762)  ==> (x0,y0)=(-60,120)
                    gfx.DrawString("*00112001*", font, XBrushes.RoyalBlue,
                        new XRect(-60, 120, 0, 0), XStringFormats.BaseLineLeft);
                }


                document.Save("BarCodeTest31.pdf");
            }
        }

        public static void Run2()
        {
            using (PdfDocument document = PdfReader.Open("BarCodeTest3.pdf", PdfDocumentOpenMode.Modify))
            {
                //create new pdf page
                PdfPage page = document.Pages[0];
                //declare a font for drawing in the PDF
                XFont font = new XFont(FontNames.MrvCode39S, 15, XFontStyle.Regular);
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    gfx.DrawString("*00112001*", font, XBrushes.Red,
                        new XRect(360, 360, 0, 0), XStringFormats.BaseLineLeft);
                    gfx.DrawString("*00112001*", font, XBrushes.BlueViolet,
                        new XRect(0, 120, 0, 0), XStringFormats.BaseLineLeft);
                    // Define a rotation transformation at the center of the page.
                    //gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                    gfx.RotateTransform(-20);
                    //gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

                    gfx.DrawString("*00112001*", font, XBrushes.Red,
                        new XRect(360, 360, 0, 0), XStringFormats.BaseLineLeft);
                    // x1 = y0;
                    // y1 = -x0;
                    // eg, (x1,y1)=(30,762)  ==> (x0,y0)=(-60,120)
                    gfx.DrawString("*00112001*", font, XBrushes.RoyalBlue,
                        new XRect(762, -30, 0, 0), XStringFormats.BaseLineLeft);
                }


                document.Save("BarCodeTest33.pdf");
            }
        }
    }
}
