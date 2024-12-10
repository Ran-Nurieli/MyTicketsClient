using MyTicketsClient.ViewModels;
using System.Security.Cryptography.X509Certificates;
using MyTicketsClient.Views;

namespace MyTicketsClient.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();

    }
}