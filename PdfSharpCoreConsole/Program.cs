using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalFontSettings.FontResolver = new FontResolver();

            // PdfDocumentExamples.CrateAndWriteToPdf();
            //PdfDocumentExamples.CreateBarcodeAsText();

            //E01HelloWorld.Run();
            //E02PageSize.Run();
            //E03TextLayout.Run();
            //E04Colors.Run();
            //E05Watermark.Run();

            //Barcode.Run();
            //Barcode.Run2();
            //E03FontResolver.Run();

            //E06PdfObjects.Run();
            //E07CombineDocuments.Run1();
            //E07CombineDocuments.Run2();

            E08PdfReader.Run();
        }
    }
}
