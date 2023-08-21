namespace PuntoDeventa.Domain
{
    public abstract class AuthStates
    {
        // Constructor privado para prevenir instanciación externa
        private AuthStates() { }

        public sealed class Loaded : AuthStates
        {
            private Loaded() { }

            public static Loaded Instence { get { return new Loaded(); } }
        }

        public sealed class Loading : AuthStates
        {
            private Loading() { }

            public static Loading Instence { get { return new Loading(); } }
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
