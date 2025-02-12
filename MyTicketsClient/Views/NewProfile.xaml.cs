using MyTicketsClient.ViewModels;
using MyTicketsClient.Views;
namespace MyTicketsClient.Views;


public partial class NewProfile : ContentPage
{
	public NewProfile(ProfilePageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}