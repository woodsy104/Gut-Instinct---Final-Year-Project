using Gut_Instinct.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Gut_Instinct.Views;

public partial class MapPage : ContentPage
{
    MapVM vm;
    public MapPage()
    {
        InitializeComponent();
        vm = new MapVM();
        BindingContext = vm;

    }


    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
        vm.GetApprovedPins();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PinPage));
    }


    private async void Pin_InfoWindowClicked(object sender, PinClickedEventArgs e)
    {
        e.HideInfoWindow = true;
        string locationRating = ((Microsoft.Maui.Controls.Maps.Pin)sender).Label;
        string address = ((Microsoft.Maui.Controls.Maps.Pin)sender).Address;
        await DisplayAlert($"{address}", $"{locationRating}", "Ok");
    }
}