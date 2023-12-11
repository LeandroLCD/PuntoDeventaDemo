using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.Maui.Core.DI
{
    internal static class DependencyInjectionService
    {
        public static MauiAppBuilder UseDIService(this MauiAppBuilder builder)
        {
            //builder.Services.AddSingleton;
            //builder.Services.AddTransient;
            builder
                .RegisterCore()
                .RegisterData()
                .RegisterDomain()
                .RegisterIU();
            return builder;
        }

        private static MauiAppBuilder RegisterCore(this MauiAppBuilder builder)
        {

            return builder;
        }
        private static MauiAppBuilder RegisterData(this MauiAppBuilder builder)
        {

            return builder;
        }
        private static MauiAppBuilder RegisterDomain(this MauiAppBuilder builder)
        {

            return builder;
        }
        private static MauiAppBuilder RegisterIU(this MauiAppBuilder builder)
        {

            return builder;
        }
    }
}
