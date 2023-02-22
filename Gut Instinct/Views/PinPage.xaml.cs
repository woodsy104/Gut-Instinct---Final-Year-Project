using Gut_Instinct.Models;

namespace Gut_Instinct.Views;

public partial class PinPage : ContentPage
{
    PinVM vm;
	public PinPage()
	{
		InitializeComponent();
        vm = new PinVM();
        BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
    }
}