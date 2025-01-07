using MyTicketsClient.ViewModels;
using System.Security.Cryptography.X509Certificates;
namespace MyTicketsClient.Views;

public partial class Register : ContentPage
{
	public Register(RegisterViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}