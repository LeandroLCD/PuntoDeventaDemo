using System.IO;

namespace PuntoDeventa.UI.Sales.State
{
    public abstract class ScreenStates
    {
        private ScreenStates() { }

        public sealed class Preview : ScreenStates
        {
            private Preview(Stream pdfStream)
            {
                PdfStream = pdfStream;
            }

            public Stream PdfStream { get; }
            public static Preview Instance(Stream pdfStream = null) => new Preview(pdfStream);
        }

        public sealed class DocumentSelection : ScreenStates
        {
            private DocumentSelection() { }
            public static DocumentSelection Instance { get; } = new DocumentSelection();
        }
        public sealed class PaymentSelection : ScreenStates
        {
            private PaymentSelection() { }
            public static PaymentSelection Instance { get; } = new PaymentSelection();
        }
        public sealed class Success : ScreenStates
        {
            private Success(string pdf)
            {
                PathPdf = pdf;
                PdfStream = new StreamReader(pdf).BaseStream;
            }
            private Success(Stream pdf, string pathPdf)
            {
                PdfStream = pdf;
                PathPdf = pathPdf;
            }

            public string PathPdf { get; }

            public Stream PdfStream { get; }
            public static Success Instance(string pathPdf) => new Success(pathPdf);
            public static Success Instance(Stream pdf, string path = null) => new Success(pdf, path);
        }

    }
}
