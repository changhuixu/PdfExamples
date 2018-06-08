using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText7Console
{
    public class PdfSplitting
    {
        public static void Split(string src)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(src));

            IList<PdfDocument> splitDocuments = new CustomPdfSplitter(pdfDoc).SplitByPageNumbers(new List<int> {3, 10, 15, 20});

            var i = 0;
            foreach (var document in splitDocuments)
            {
                document.Close();
            }
            pdfDoc.Close();
        }

    
    }

    public class CustomPdfSplitter:PdfSplitter
    {
        private int _partNumber = 1;

        public CustomPdfSplitter(PdfDocument pdfDocument) : base(pdfDocument)
        {
        }

        protected override PdfWriter GetNextPdfWriter(PageRange documentPageRange)
        {
            try
            {
                return new PdfWriter("splitDocument1_" + _partNumber++ + ".pdf");
            }
            catch (FileNotFoundException e)
            {
                throw new Exception();
            }
        }
    }
}
