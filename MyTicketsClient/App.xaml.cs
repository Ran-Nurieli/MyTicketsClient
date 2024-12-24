using MyTicketsClient.Models;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Services;
namespace MyTicketsClient
{
    public partial class App : Application
    {


        public User? LoggedInUser { get; set; } = null!;
        public App()
        {



            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
