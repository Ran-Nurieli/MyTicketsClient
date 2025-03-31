//using Microsoft.UI.Xaml.Controls;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Views;

namespace MyTicketsClient
{
    public partial class AppShell : Shell
    {

        
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            






        }

 
    }
}
