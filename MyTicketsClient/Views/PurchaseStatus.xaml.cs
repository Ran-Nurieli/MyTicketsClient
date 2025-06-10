using MyTicketsClient.ViewModels;

namespace MyTicketsClient.Views;

public partial class PurchaseStatus : ContentPage
{
	public PurchaseStatus(PurchaseStatusViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PurchaseStatusViewModel vm)
        {
            Task.Run(async () => await vm.UpdateTickets());
        }
    }
}
