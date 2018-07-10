using System;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    public class E02PageSize
    {
        public static void Run()
        {
            // Create a new PDF document.
            var document = new PdfDocument();

            // Create a font.
            var font = new XFont(FontNames.Arial, 25, XFontStyle.Bold);

            var pageSizes = (PageSize[])Enum.GetValues(typeof(PageSize));
            foreach (var pageSize in pageSizes)
            {
                if (pageSize == PageSize.Undefined)
                    continue;

                // One page in Portrait...
                var page = document.AddPage();
                page.Size = pageSize;
                var gfx = XGraphics.FromPdfPage(page);
                gfx.DrawString(pageSize.ToString(), font, XBrushes.DarkRed,
                    new XRect(0, 0, page.Width, page.Height),
                    XStringFormats.Center);

                // ... and one in Landscape orientation.
                page = document.AddPage();
                page.Size = pageSize;
                page.Orientation = PageOrientation.Landscape;
                gfx = XGraphics.FromPdfPage(page);
                gfx.DrawString(pageSize + " (landscape)", font,
                    XBrushes.DarkRed, new XRect(0, 0, page.Width, page.Height),
                    XStringFormats.Center);
            }

            // Save the document...
            const string filename = "PageSizes_tempfile.pdf";
            document.Save(filename);
        }
    }
}
