using System;
using IronPdf;

namespace IronPdfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            Renderer.RenderHtmlAsPdf("<h1>Hello World<h1>").SaveAs("html-string.pdf");
        }
    }
}
