using iText7Console.Chapter3;
using System;
using iText7Console.Chapter5;
using iText7Console.Chapter6;

namespace iText7Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //PdfUtils.HelloWorld("a.pdf");
            //PdfUtils.InsertParagraph("b.pdf");
            //PdfUtils.InsertImage("c.pdf");
            //PdfUtils.DisplayDataTable("d.pdf");
            //PdfUtils.DrawAxes("e.pdf");
            //PdfUtils.DrawGridLines("f.pdf");
            //PdfUtils.PrintStarWars("star-wars-1.pdf");
            //PdfUtils.PrintStarWarsCrawl("star-wars-2.pdf");

            //Chapter3.NewYorkTimes("ny.pdf");
            //Chapter3.PremierLeague("pl.pdf");
            //Ufo.Generate("ufo.pdf");

            //ExistingPdfManipulations.AddContent(@"data/Chapter5/ufo.pdf", @"new-ufo.pdf");
            //PdfNupping.CreatePdf("data/Chapter6/card.pdf", "20cards.pdf", 5, 4);
            //PdfMerge.OscarCombine("88th_Oscar.pdf");
            //PdfMerge.OscarCombineXofY("88th_Oscar2.pdf");
            //TextExtractor.Read(@"C:\AppData\App_Data\SHL\Archive\Environmental_A_test\Environmental_A_test.pdf");
            //PdfSplitting.Split(@"C:\AppData\App_Data\SHL\Archive\Environmental_A_test\Environmental_A_test.pdf");
            //Console.ReadKey();
            //HtmlTest.Create("html.pdf");
            BarcodeGen.Gen("barcode.pdf");
        }
    }
}
