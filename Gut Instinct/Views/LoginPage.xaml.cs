using Gut_Instinct.Models;

namespace Gut_Instinct.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}