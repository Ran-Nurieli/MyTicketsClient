namespace MyTicketsClient.Views;

public partial class LoginTabs : TabbedPage
{
	public LoginTabs(LoginPage lPage, Register rPage)
	{
		InitializeComponent();
        Children.Add(lPage);
        Children.Add(rPage);
    }
}