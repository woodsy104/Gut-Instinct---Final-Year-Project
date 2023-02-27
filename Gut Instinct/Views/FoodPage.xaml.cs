using Gut_Instinct.Models;
using System.Drawing;
using Realms;

namespace Gut_Instinct.Views;

public partial class FoodPage : ContentPage
{
    FoodVM vm;

    public FoodPage() { 
        InitializeComponent();
        vm = new FoodVM();
        BindingContext= vm;
    }


    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
    }

    private void Red_Button_Clicked(object sender, EventArgs e)
    {
        colourpick.Text = "Red";
    }

    private void Orange_Button_Clicked(object sender, EventArgs e)
    {
        colourpick.Text = "DarkOrange";
    }

    private void Green_Button_Clicked(object sender, EventArgs e)
    {
        colourpick.Text = "Green";
    }
}