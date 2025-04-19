using MyTicketsClient.ViewModels;

namespace MyTicketsClient.Views;

public partial class PurchaseStatus : ContentPage
{
	public PurchaseStatus(PurchaseStatusViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}