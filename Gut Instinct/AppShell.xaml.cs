namespace Gut_Instinct;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
        Routing.RegisterRoute(nameof(Views.FoodPage), typeof(Views.FoodPage));
        Routing.RegisterRoute(nameof(Views.PinPage), typeof(Views.PinPage));
    }
}
