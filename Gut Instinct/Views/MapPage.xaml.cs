using Gut_Instinct.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using static AndroidX.Navigation.Navigator;
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
        
        Location location = new Location(52.66482630113309, -8.625857537927548);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        Map map = new Map(mapSpan);
        Content = map;
        //LoadPins();
        Microsoft.Maui.Controls.Maps.Pin newPin = new Microsoft.Maui.Controls.Maps.Pin
        {
            Label = "Arthurs Quay",
            Address = "Shopping Center",
            Type = PinType.Place,
            Location = new Location(52.66482630113309, -8.625857537927548)
        };
        newPin.MarkerClicked += async (s, args) => {
            args.HideInfoWindow = true;
            string locationName = ((Microsoft.Maui.Controls.Maps.Pin)s).Label;
            await DisplayAlert($"{locationName}", " 5 star rating and is free", "Ok");
        };
        map.Pins.Add(newPin);

        Microsoft.Maui.Controls.Maps.Pin newExample = new Microsoft.Maui.Controls.Maps.Pin
        {
            Label = "Milk Market",
            Address = "Toilet",
            Type = PinType.Place,
            Location = new Location(52.663615979885186, -8.622143843905066)
        };
        newExample.MarkerClicked += async (s, args) => {
            args.HideInfoWindow = true;
            string locationName = ((Microsoft.Maui.Controls.Maps.Pin)s).Label;
            await DisplayAlert($"{locationName}", "4 star rating and costs", "Ok");
        };
        map.Pins.Add(newExample);
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
}