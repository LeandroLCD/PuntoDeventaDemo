namespace PuntoDeventa.UI.Reports.State
{
    public abstract class ExportState
    {
        private ExportState()
        {

        }
        public sealed class Loaded : ExportState
        {
            private Loaded() { }
            public static Loaded Instance { get; } = new Loaded();
        }

        public sealed class Loading : ExportState
        {

            private Loading() { }
            public static Loading Instance { get; } = new Loading();
        }

        public sealed class Success : ExportState
        {
            private Success(object data) { Data = data; }
            public object Data { get; }
            public static Success Instance(object data)
            {
                return new Success(data);
            }
        }

        public sealed class Error : ExportState
        {
            public string Message { get; }
            private Error(string message) { Message = message; }
            public static Error Instance(string message)
            {
                return new Error(message);
            }
        }
    }
}
