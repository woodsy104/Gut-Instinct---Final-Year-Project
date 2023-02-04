using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Gut_Instinct.Views;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
        Location location = new Location(52.66482630113309, -8.625857537927548);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        Map map = new Map(mapSpan);
        Content = map;
        //LoadPins();
        Pin pin = new Pin
        {
            Label = "Arthurs Quay",
            Address = "Shopping Center",
            Type = PinType.Place,
            Location = new Location(52.66482630113309, -8.625857537927548)
        };
        map.Pins.Add(pin);
    }

    //protected override void OnAppearing()
    //{
    //Add a function to load all pins here
    //}
    //private void LoadPins() { 
      //  Models.Pin pin = new Models.Pin();
        //pin.Location.Add(52.66482630113309);
        //pin.Location.Add(-8.625857537927548);
        //pin.Name = "Arthurs";
        //pin.Address = "Shopping Center";
        //pin.type = "place";

    //}

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PinPage));
    }
}