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
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<RegisterViewModel>();

            builder.Services.AddSingleton<MyTicketServerClientApi>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
