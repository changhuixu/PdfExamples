using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Pdf.IO;

namespace PdfSharpCoreConsole
{
    public class E08PdfReader
    {
        public static void Run()
        {
            var document = PdfReader.Open("Clinical_A_test.pdf", PdfDocumentOpenMode.ReadOnly);

            foreach (var page in document.Pages)
            {
                //var parser = new CParser(page);
                // var seq1 = parser.ReadContent();
                var seq = ContentReader.ReadContent(page);
                var lines = ExtractText(seq);

                if (lines.Any(l => l.Contains("INVOICE")))
                {
                    var containsInvoice = true;
                }
                var ele = page.Contents.Elements;

            }
        }

        private static IEnumerable<string> ExtractText(CObject cObject)
        {
            var textList = new List<string>();
            if (cObject is COperator)
            {
                var cOperator = cObject as COperator;
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                    {
                        textList.AddRange(ExtractText(cOperand));
                    }
                }
            }
            else if (cObject is CSequence)
            {
                var cSequence = cObject as CSequence;
                foreach (var element in cSequence)
                {
                    textList.AddRange(ExtractText(element));
                }
            }
            else if (cObject is CString)
            {
                var cString = cObject as CString;
                textList.Add(cString.Value);
            }
            return textList;
        }
    }
}
