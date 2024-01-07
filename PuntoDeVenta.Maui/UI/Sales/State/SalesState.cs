namespace PuntoDeVenta.Maui.UI.Sales.State
{
    public abstract class SalesState
    {
        private SalesState() { }

        public sealed class Loaded : SalesState
        {
            private Loaded() { }
            public static Loaded Instance { get; } = new Loaded();
        }

        public sealed class Loading : SalesState
        {

            private Loading() { }
            public static Loading Instance { get; } = new Loading();
        }

        public sealed class Success : SalesState
        {
            private Success(object data) { Data = data; }
            public object Data { get; }
            public static Success Instance(object data)
            {
                return new Success(data);
            }
        }

        public sealed class Error : SalesState
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
