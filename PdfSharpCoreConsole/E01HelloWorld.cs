﻿using System;
using System.Collections.Generic;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    public class E01HelloWorld
    {
        public static void Run()
        {
            // Create a new PDF document.
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page in this document.
            var page = document.AddPage();

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            // Draw two lines with a red default pen.
            var width = page.Width;
            var height = page.Height;
            gfx.DrawLine(XPens.Red, 0, 0, width, height);
            gfx.DrawLine(XPens.Red, width, 0, 0, height);

            // Draw a circle with a red pen which is 1.5 point thick.
            var r = width / 5;
            gfx.DrawEllipse(new XPen(XColors.Red, 1.5), XBrushes.White, new XRect(width / 2 - r, height / 2 - r, 2 * r, 2 * r));

            // Draw two rectangles
            var rect = new XRect(36, 72, 250, 300);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            rect = new XRect(326, 72, 250, 300);
            gfx.DrawRectangle(XBrushes.AliceBlue, rect);

            // Draw one line connecting two rectangles
            gfx.DrawLine(XPens.Violet, 286, 72, 326, 72);

            // Create a font.
            var font = new XFont(FontNames.Arial, 20, XFontStyle.BoldItalic);

            // Draw the text.
            gfx.DrawString("Hello, PDFsharp!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            // Write texts at specific points
            font = new XFont(FontNames.Arial, 10);
            gfx.DrawString("(286, 72)", font, XBrushes.Blue,
                new XRect(256, 70, 0, 0), XStringFormats.BaseLineLeft);

            // Save the document...
            const string filename = "HelloWorld_tempfile.pdf";
            document.Save(filename);
        }
    }
}
