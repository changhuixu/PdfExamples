using System;
using System.Collections.Generic;
using System.Text;
using iText.Barcodes;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText7Console
{
   public class BarcodeGen
    {
        public static void Gen(string dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc, new PageSize(60, 140));
            doc.SetMargins(5, 5, 5, 5);

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont regular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont mrv39s = PdfFontFactory.CreateFont(@"fonts/mrvcode39s.ttf", true);
            Paragraph p1 = new Paragraph();
            p1.Add(new Text("23").SetFont(bold).SetFontSize(12));
            p1.Add(new Text("000").SetFont(bold).SetFontSize(6));
            doc.Add(p1);

            Paragraph p2 = new Paragraph("T.T.C.").SetFont(regular).SetFontSize(6);
            p2.SetTextAlignment(TextAlignment.RIGHT);
            doc.Add(p2);

            // CODE 39
            Paragraph pc3 = new Paragraph("*10000100*").SetFont(mrv39s).SetFontSize(6);
            pc3.SetTextAlignment(TextAlignment.LEFT);
            pc3.SetRotationAngle(Math.PI / 2);
            doc.Add(pc3);

            Barcode39 barcode = new Barcode39(pdfDoc);
            barcode.SetCode("12345678");
            Rectangle rect = barcode.GetBarcodeSize();
            PdfFormXObject template = new PdfFormXObject(new Rectangle(rect.GetWidth(), rect.GetHeight() + 10));
            PdfCanvas templateCanvas = new PdfCanvas(template, pdfDoc);
            new Canvas(templateCanvas, pdfDoc, new Rectangle(rect.GetWidth(), rect.GetHeight() + 10))
                .ShowTextAligned(new Paragraph("DARK GRAY").SetFont(regular).SetFontSize(6), 0, rect.GetHeight() + 2, TextAlignment.LEFT);
            barcode.PlaceBarcode(templateCanvas, DeviceRgb.BLACK, DeviceRgb.BLACK);
            Image image = new Image(template);
            image.SetRotationAngle(Math.PI/4);
            image.SetAutoScale(true);
            doc.Add(image);

            Paragraph p3 = new Paragraph("SMALL").SetFont(regular).SetFontSize(6);
            p3.SetTextAlignment(TextAlignment.CENTER);
            doc.Add(p3);

            doc.Close();
        }
    }
}
