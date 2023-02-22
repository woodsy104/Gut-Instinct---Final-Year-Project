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
        Location location = new Location(52.66482630113309, -8.625857537927548);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        Map map = new Map(mapSpan);
        Content = map;
        //LoadPins();
        /*Pin pin = new Pin
        {
            Label = "Arthurs Quay",
            Address = "Shopping Center",
            Type = PinType.Place,
            Location = new Location(52.66482630113309, -8.625857537927548)
        };
        map.Pins.Add(pin);*/
        //vm.GetApprovedPins();
        /*foreach (ApprovedPin aPin in vm.ApprovedPins.ToList())
        {
            double xPin = double.Parse(aPin.XCoordinate);
            double yPin = double.Parse(aPin.YCoordinate);
            Microsoft.Maui.Controls.Maps.Pin pin = new Microsoft.Maui.Controls.Maps.Pin
            {
                Label = aPin.Name,
                Address = "Toilet",
                Type = PinType.Place,
                Location = new Location(xPin, yPin)
            };
            map.Pins.Add(pin);
            //This loop not being hit!
        }*/
        /*List<ApprovedPin> approved = vm.ApprovedPins.ToList();
        ApprovedPin aPin = approved.ElementAt(1);
        double xPin = double.Parse(aPin.XCoordinate);
        double yPin = double.Parse(aPin.YCoordinate);
        Microsoft.Maui.Controls.Maps.Pin pin = new Microsoft.Maui.Controls.Maps.Pin
        {
            Label = aPin.Name,
            Address = "Toilet",
            Type = PinType.Place,
            Location = new Location(xPin, yPin)
        };
        map.Pins.Add(pin);*/

    }

    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
        vm.GetApprovedPins();
        //Add a function to load all pins here
        //}
        //private void LoadPins() { 
        //  Models.Pin pin = new Models.Pin();
        //pin.Location.Add(52.66482630113309);
        //pin.Location.Add(-8.625857537927548);
        //pin.Name = "Arthurs";
        //pin.Address = "Shopping Center";
        //pin.type = "place";
        

    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PinPage));
    }
}