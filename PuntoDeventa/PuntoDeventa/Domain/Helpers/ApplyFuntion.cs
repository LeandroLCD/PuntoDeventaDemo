using System;

namespace PuntoDeVenta.Domain.Helpers
{
    public static class ApplyFuntion
    {
        public static void Apply(this object obj, Action action)
        {
            action?.Invoke();
        }
    }
}