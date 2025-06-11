using MyTicketsClient.Models;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Services;
using Microsoft.Extensions.DependencyInjection;
using MyTicketsClient.Views;
namespace MyTicketsClient
{
    public partial class App : Application
    {


        public User? LoggedInUser { get; set; } = null!;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = serviceProvider.GetService<LoginTabs>();
            
        }
    }
}
