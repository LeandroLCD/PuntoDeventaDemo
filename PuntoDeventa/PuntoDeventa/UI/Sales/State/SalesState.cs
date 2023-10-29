namespace PuntoDeventa.UI.Sales.State
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
            public object Data { get; }
            public Success(object data)
            {
                Data = data;
            }
        }

        public sealed class Error : SalesState
        {
            public string Message { get; }
            public Error(string message)
            {
                Message = message;
            }
        }
    }
}
