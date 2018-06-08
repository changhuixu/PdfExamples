using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;

namespace iText7Console.Chapter6
{
    public class PdfNupping
    {
        public static void CreatePdf(string src, string dest, int row, int col)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PdfDocument sourcePdf = new PdfDocument(new PdfReader(src));
            //Original page
            PdfPage origPage = sourcePdf.GetPage(1);
            //Original page size
            Rectangle orig = origPage.GetPageSize();
            PdfFormXObject pageCopy = origPage.CopyAsFormXObject(pdf);
            //N-up page
            PageSize nUpPageSize = PageSize.LETTER.Rotate();
            PdfPage page = pdf.AddNewPage(nUpPageSize);
            PdfCanvas canvas = new PdfCanvas(page);
            //Scale page
            var width = nUpPageSize.GetWidth();
            var height = nUpPageSize.GetHeight();
            var origWidth = orig.GetWidth();
            var origHeight = orig.GetHeight();
            var transformationMatrix = AffineTransform.GetScaleInstance(width / origWidth / col, height / origHeight / row);
            canvas.ConcatMatrix(transformationMatrix);
            //Add pages to N-up page
            for (var i = 0; i < col; i++)
            {
                for (var j = 0; j < row; j++)
                {
                    canvas.AddXObject(pageCopy, i * origWidth, j * origHeight);
                }
            }

            pdf.Close();
            sourcePdf.Close();
        }
    }
}
