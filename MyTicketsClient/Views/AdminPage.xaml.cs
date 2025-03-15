using MyTicketsClient.ViewModels;

namespace MyTicketsClient.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}