using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using MyTicketsClient.Models;
using MyTicketsClient.Services;
using MyTicketsClient.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;



namespace MyTicketsClient.ViewModels;

public class LoginPageViewModel : ViewModelBase
{

    private IServiceProvider serviceProvider;
    private MyTicketServerClientApi proxy;
    public LoginPageViewModel(MyTicketServerClientApi proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        LoginCommand = new Command(OnLogin);
        RegisterCommand = new Command(OnRegister);
        email = "";
        password = "";
        InServerCall = false;
        errorMsg = "";
    }






    private string email;
    private string password;
    private string errorMsg;


    

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public string Email
    {
        get => email;
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }


    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }


    private void OnRegister()
    {
        ErrorMsg = "";
        Email = "";
        Password = "";
        // Navigate to the Register View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Register>());
    }

    private async void OnLogin()
    {
        ////Choose the way you want to blobk the page while indicating a server call
        //InServerCall = true;
        //ErrorMsg = "";
        ////Call the server to login
        //LoginInfo loginInfo = new LoginInfo { Email = Email, Password = Password };
        //User? u = await this.proxy.LoginAsync(loginInfo);

        //InServerCall = false;

        ////Set the application logged in user to be whatever user returned (null or real user)
        //((App)Application.Current).LoggedInUser = u;
        //if (u == null)
        //{
        //    ErrorMsg = "Invalid email or password";
        //}
        //else
        //{
        //    ErrorMsg = "";
        //    //Navigate to the main page
        //    AppShell shell = serviceProvider.GetService<AppShell>();
        //    //TasksViewModel tasksViewModel = serviceProvider.GetService<TasksViewModel>();
        //    //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
        //    //((App)Application.Current).MainPage = shell;
        //    //Shell.Current.FlyoutIsPresented = false; //close the flyout
        //    //Shell.Current.GoToAsync("Tasks"); //Navigate to the Tasks tab page
        //}
    }





}
