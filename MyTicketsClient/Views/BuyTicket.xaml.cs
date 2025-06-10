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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BuyTicketViewModel vm)
        {
            Task.Run(async () => await vm.LoadTickets());
        }
    }
}