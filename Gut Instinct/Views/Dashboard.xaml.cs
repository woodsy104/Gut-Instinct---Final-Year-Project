using Gut_Instinct.Models;

namespace Gut_Instinct.Views;

public partial class Dashboard : ContentPage
{
	DashboardVM vm;

    public Dashboard()
	{
		InitializeComponent();
		vm = new DashboardVM();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
		await vm.InitialiseRealm();
		vm.GetAppointments();
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("What do the colours mean?!", "Blue = GP Visit; " +
            "Pink = Specialist Visit; " + "Yellow = MISC;", "OK");
    }
}