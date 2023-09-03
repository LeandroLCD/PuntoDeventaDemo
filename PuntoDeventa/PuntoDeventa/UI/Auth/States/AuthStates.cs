namespace PuntoDeventa.UI.Auth.States
{
    public abstract class AuthStates
    {
        // Constructor privado para prevenir instanciación externa
        private AuthStates() { }

        public sealed class Loaded : AuthStates
        {
            // Constructor privado para prevenir instanciación externa
            private Loaded() { }
            public static Loaded Instance { get; } = new Loaded();
        }

        public sealed class Loading : AuthStates
        {
            private Loading() { }  // Constructor privado para prevenir instanciación externa
            public static Loading Instance { get; } = new Loading();
        }

        public sealed class Success : AuthStates
        {
            public object Data { get; }
            public Success(object data)
            {
                Data = data;
            }
        }

        public sealed class Error : AuthStates
        {
            public string Message { get; }
            public Error(string message)
            {
                Message = message;
            }
        }
    }
}
