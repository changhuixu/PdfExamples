using System;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCoreConsole.fonts;

namespace PdfSharpCoreConsole
{
    public class E07CombineDocuments
    {
        public static void Run1()
        {
            // Get two fresh copies of the sample PDF files.
            // (Note: The input files are not modified in this sample.)
            const string filename1 = "Portable Document Format 1.pdf";
            var file1 = Path.Combine(Directory.GetCurrentDirectory(), filename1);
            File.Copy(Path.Combine("assets", "Portable Document Format.pdf"), file1, true);

            // Remove ReadOnly attribute from the copy.
            File.SetAttributes(file1, File.GetAttributes(file1) & ~FileAttributes.ReadOnly);


            const string filename2 = "Portable Document Format 2.pdf";
            var file2 = Path.Combine(Directory.GetCurrentDirectory(), filename2);
            File.Copy(Path.Combine("assets", "Portable Document Format.pdf"), file2, true);

            // Remove ReadOnly attribute from the copy.
            File.SetAttributes(file1, File.GetAttributes(file2) & ~FileAttributes.ReadOnly);

            // Open the input files.
            var inputDocument1 = PdfReader.Open(filename1, PdfDocumentOpenMode.Import);
            var inputDocument2 = PdfReader.Open(filename2, PdfDocumentOpenMode.Import);

            // Create the output document.
            var outputDocument = new PdfDocument();

            // Show consecutive pages facing. Requires Acrobat 5 or higher.
            outputDocument.PageLayout = PdfPageLayout.TwoColumnLeft;

            var font = new XFont(FontNames.SegoeWP, 10, XFontStyle.Bold);
            var format = new XStringFormat
            {
                Alignment = XStringAlignment.Center,
                LineAlignment = XLineAlignment.Far
            };
            var count = Math.Max(inputDocument1.PageCount, inputDocument2.PageCount);
            for (var idx = 0; idx < count; idx++)
            {
                // Get the page from the 1st document.
                var page1 = inputDocument1.PageCount > idx ?
                    inputDocument1.Pages[idx] : new PdfPage();

                // Get the page from the 2nd document.
                var page2 = inputDocument2.PageCount > idx ?
                  inputDocument2.Pages[idx] : new PdfPage();

                // Add both pages to the output document.
                page1 = outputDocument.AddPage(page1);
                page2 = outputDocument.AddPage(page2);

                // Write document file name and page number on each page.
                var gfx = XGraphics.FromPdfPage(page1);
                var box = page1.MediaBox.ToXRect();
                box.Inflate(0, -10);
                gfx.DrawString(String.Format("{0} • {1}", filename1, idx + 1),
                    font, XBrushes.Red, box, format);

                gfx = XGraphics.FromPdfPage(page2);
                box = page2.MediaBox.ToXRect();
                box.Inflate(0, -10);
                gfx.DrawString(String.Format("{0} • {1}", filename2, idx + 1),
                    font, XBrushes.Red, box, format);
            }

            // Save the document...
            const string filename = "CompareDocument1_tempfile.pdf";
            outputDocument.Save(filename);
        }

        public static void Run2()
        {
            // (Note: The input files are not modified in this sample.)
            const string filename1 = "Portable Document Format 1.pdf";
            var file1 = Path.Combine(Directory.GetCurrentDirectory(), filename1);
            File.Copy(Path.Combine("assets", "Portable Document Format.pdf"), file1, true);
            // Remove ReadOnly attribute from the copy.
            File.SetAttributes(file1, File.GetAttributes(file1) & ~FileAttributes.ReadOnly);

            // Open the input files.
            var inputDocument1 = PdfReader.Open(filename1, PdfDocumentOpenMode.Import);

            // Create the output document.
            var outputDocument = new PdfDocument();
            outputDocument.AddPage(inputDocument1.Pages[0]);
            outputDocument.AddPage(new PdfPage());
            for (var i = 1; i < inputDocument1.PageCount; i++)
            {
                outputDocument.AddPage(inputDocument1.Pages[i]);
            }

            // Save the document...
            const string filename = "MergeDocument_tempfile.pdf";
            outputDocument.Save(filename);
        }
    }
}
