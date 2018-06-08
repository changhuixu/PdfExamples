using System;
using PdfiumLight;

namespace PdfiumLightConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load the pdf file and create a new document object
            PdfDocument document = new PdfDocument("C:/Users/Marc/Documents/sample.pdf");
            // Load the first page
            PdfPage page = document.GetPage(0);
            // Render the page
            var renderedPage = page.Render(
                10000, // width in px
                0, // '0' to compute height according to aspect ratio
                0, // x of the top/left of clipping rectangle
                0, // y of the top/left point of clipping rectangle
                1000, // width of clipping reactangle
                1000, // height of clipping reactangle
                PdfRotation.Rotate0, // no rotation
                PdfRenderFlags.None // no render flags
            );
        }
    }
}
