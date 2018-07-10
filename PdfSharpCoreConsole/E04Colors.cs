using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace PdfSharpCoreConsole
{
    public class E04Colors
    {
        public static void Run()
        {
            // Get a fresh copy of the sample PDF file.
            const string filenameSource = @"assets/SomeLayout.pdf";
            const string filenameDest = "ColorsCMYK_tempfile.pdf";
            var file = Path.Combine(Directory.GetCurrentDirectory(), filenameDest);
            File.Copy(filenameSource, file, true);

            // Remove ReadOnly attribute from the copy.
            File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);

            // Open an existing document for editing and draw on first page.
            var document = PdfReader.Open(filenameDest, PdfDocumentOpenMode.Modify);
            document.Options.ColorMode = PdfColorMode.Cmyk;

            // Set version to PDF 1.4 (Acrobat 5) because we use transparency.
            if (document.Version < 14)
                document.Version = 14;

            var page = document.Pages[0];

            // Get an XGraphics object for drawing beneath the existing content.

            var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(1, 0.68, 0, 0.12)), new XRect(30, 60, 50, 50));
            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0, 0.70, 1, 0)), new XRect(550, 60, 50, 50));

            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0, 0, 0, 0)), new XRect(90, 100, 50, 50));
            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0, 0, 0, 0)), new XRect(150, 100, 50, 50));

            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0.7, 0, 0.70, 1, 0)), new XRect(90, 100, 50, 50));
            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0.5, 0, 0.70, 1, 0)), new XRect(150, 100, 50, 50));

            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0.35, 0.15, 0, 0.08)), new XRect(50, 360, 50, 50));
            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0.25, 0.10, 0, 0.05)), new XRect(150, 360, 50, 50));
            gfx.DrawRectangle(new XSolidBrush(XColor.FromCmyk(0.15, 0.05, 0, 0)), new XRect(250, 360, 50, 50));

            // Save the document...
            document.Save(filenameDest);
        }
    }
}
