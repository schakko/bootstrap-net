// <author company="EDV Consulting Wohlers GmbH" name="Daniel Vogelsang">
namespace Ecw.Windows.Printing
{
    using System.Printing;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public class PrintDocumentViewer : DocumentViewer
    {
        protected override void OnPrintCommand()
        {
            // get a print dialog, defaulted to default printer and default printer's preferences.
            var printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;

            string s = Document.GetType().ToString();

            // get a reference to the FixedDocumentSequence for the viewer.
            //FixedDocumentSequence docSeq = this.Document as FixedDocumentSequence;
            var docSeq = (FixedDocument) Document;

            // set the default page orientation based on the desired output.
            printDialog.PrintTicket = (PrintTicket) docSeq.PrintTicket;

            if (printDialog.ShowDialog() == true)
            {
                // set the print ticket for the document sequence and write it to the printer.
                docSeq.PrintTicket = printDialog.PrintTicket;

                var writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
                writer.WriteAsync(docSeq, printDialog.PrintTicket);
            }
        }
    }
}