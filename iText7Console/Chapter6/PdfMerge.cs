using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText7Console.Chapter6
{
    public class PdfMerge
    {
        public static void OscarCombine(string dest)
        {
            //Initialize PDF document with output intent
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PdfMerger merger = new PdfMerger(pdf);
            //Add pages from the first document
            PdfDocument firstSourcePdf = new PdfDocument(new PdfReader(@"data/Chapter6/88th_reminder_list.pdf"));
            merger.Merge(firstSourcePdf, 1, firstSourcePdf.GetNumberOfPages());
            //Add pages from the second pdf document
            PdfDocument secondSourcePdf = new PdfDocument(new PdfReader(@"data/Chapter6/88th_noms_announcement.pdf"));
            merger.Merge(secondSourcePdf, 1, secondSourcePdf.GetNumberOfPages());
            firstSourcePdf.Close();
            secondSourcePdf.Close();
            pdf.Close();
        }

        public static void OscarCombineXofY(string dest)
        {
            //Initialize PDF document with output intent
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PdfMerger merger = new PdfMerger(pdf);
            //Add pages from the first document
            PdfDocument firstSourcePdf = new PdfDocument(new PdfReader(@"data/Chapter6/88th_reminder_list.pdf"));
            merger.Merge(firstSourcePdf, iText.IO.Util.JavaUtil.ArraysAsList(1, 5, 7, 1));
            //Add pages from the second pdf document
            PdfDocument secondSourcePdf = new PdfDocument(new PdfReader(@"data/Chapter6/88th_noms_announcement.pdf"));
            merger.Merge(secondSourcePdf, iText.IO.Util.JavaUtil.ArraysAsList(1, 15));
            firstSourcePdf.Close();
            secondSourcePdf.Close();
            pdf.Close();
        }
    }
}
