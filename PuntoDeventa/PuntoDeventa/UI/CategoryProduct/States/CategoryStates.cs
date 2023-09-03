namespace PuntoDeventa.UI.CategoryProduct.States
{
    public abstract class CategoryStates
    {
            // Constructor privado para prevenir instanciación externa
            private CategoryStates() { }

           
            public sealed class Loading : CategoryStates
            {
                private Loading() { }  // Constructor privado para prevenir instanciación externa
                public static Loading Instance { get; } = new Loading();
            }

            public sealed class Success : CategoryStates
            {
                public object Data { get; }
                public Success(object data)
                {
                    Data = data;
                }
            }

            public sealed class Error : CategoryStates
            {
                public string Message { get; }
                public Error(string message)
                {
                    Message = message;
                }
            }
        
    }
}
