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
            builder.Services.AddTransient<BuyTicket>();
            builder.Services.AddTransient<SellTicket>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<NewProfile>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<PurchaseStatus>();
            builder.Services.AddTransient<LoginTabs>();

            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<BuyTicketViewModel>();
            builder.Services.AddTransient<SellTicketViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddSingleton<AppShellViewModel>();
            builder.Services.AddTransient<PurchaseStatusViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
