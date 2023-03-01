namespace Gut_Instinct;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        Routing.RegisterRoute(nameof(Views.Dashboard), typeof(Views.Dashboard));
        Routing.RegisterRoute(nameof(Views.ThreadPage), typeof(Views.ThreadPage));
        Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
        Routing.RegisterRoute(nameof(Views.FoodPage), typeof(Views.FoodPage));
        Routing.RegisterRoute(nameof(Views.PinPage), typeof(Views.PinPage));
    }
}
