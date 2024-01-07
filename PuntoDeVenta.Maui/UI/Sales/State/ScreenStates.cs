using System.IO;

namespace PuntoDeVenta.Maui.UI.Sales.State
{
    public abstract class ScreenStates
    {
        private ScreenStates() { }

        public sealed class Preview : ScreenStates
        {
            private Preview(Stream pdfStream, string pathPdf)
            {
                PdfStream = pdfStream;
            }
            private Preview(string pathPdf)
            {
                PathPdf = pathPdf;
                PdfStream = new StreamReader(pathPdf).BaseStream;
            }

            public Stream PdfStream { get; }

            public string PathPdf { get; }
            public static Preview Instance(Stream pdfStream, string path) => new(pdfStream, path);
            public static Preview Instance(string path = null) => new(path);
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

        public ScreenStates BackState()
        {
            switch (this)
            {
                case DocumentSelection _:
                    return null;
                case PaymentSelection _:
                    return DocumentSelection.Instance;
                case Preview _:
                    return DocumentSelection.Instance;
                case Success success:
                    return success;
                default:
                    return null;
            }
        }

    }
}
