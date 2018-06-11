using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;

namespace GrapeCityDocuments.Document
{
    public class HelloWorld
    {
        public static void Generate()
        {
            GcPdfDocument doc = new GcPdfDocument();
            // Add a page, get its graphics:
            GcPdfGraphics g = doc.NewPage().Graphics;
            // Render a string into the page:
            g.DrawString("Hello, World!",
                // Use a standard font (the 14 standard PDF fonts are built into GcPdf
                // and are always available):
                new TextFormat() { Font = StandardFonts.Times, FontSize = 12 },
                // GcPdf page coordinates start at top left corner, using 72 dpi by default:
                new PointF(72, 72));
            // Save the PDF:
            doc.Save("HelloWorld.pdf");
        }
    }
}
