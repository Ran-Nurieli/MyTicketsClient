using MyTicketsClient.ViewModels;

namespace MyTicketsClient.Views;

public partial class SellTicket : ContentPage
{
	public SellTicket(SellTicketViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}