namespace PuntoDeventa.UI.CatalogueClient.States
{
    public abstract class CatalogeState
    {
        private CatalogeState()
        {

        }
        public sealed class Loading : CatalogeState
        {
            private Loading() { }  // Constructor privado para prevenir instanciación externa
            public static Loading Instance { get; } = new Loading();
        }

        public sealed class Success : CatalogeState
        {
            public object Data { get; }
            public Success(object data)
            {
                Data = data;
            }
        }

        public sealed class Error : CatalogeState
        {
            public string Message { get; }
            public Error(string message)
            {
                Message = message;
            }
        }
    }
}
