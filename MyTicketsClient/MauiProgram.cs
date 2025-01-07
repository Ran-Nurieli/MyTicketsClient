using Microsoft.Extensions.Logging;
using MyTicketsClient.Views;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Services;

namespace MyTicketsClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MyTicketServerClientApi>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<Register>();

            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();

            


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
