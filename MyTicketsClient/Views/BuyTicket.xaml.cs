using MyTicketsClient.ViewModels;
using System.Runtime.CompilerServices;
namespace MyTicketsClient.Views;

public partial class BuyTicket : ContentPage
{
	public BuyTicket(BuyTicketViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}