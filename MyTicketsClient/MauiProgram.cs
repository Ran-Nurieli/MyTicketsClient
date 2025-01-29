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
            builder.Services.AddTransient<SellTicket>();
            builder.Services.AddTransient<HomePage>();


            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<SellTicketViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
