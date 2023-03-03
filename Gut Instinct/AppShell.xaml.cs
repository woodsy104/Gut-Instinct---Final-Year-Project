namespace Gut_Instinct;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        Routing.RegisterRoute(nameof(Views.Dashboard), typeof(Views.Dashboard));
        Routing.RegisterRoute(nameof(Views.ThreadPage), typeof(Views.ThreadPage));
        
        Routing.RegisterRoute(nameof(Views.FoodPage), typeof(Views.FoodPage));
        Routing.RegisterRoute(nameof(Views.PinPage), typeof(Views.PinPage));
        Routing.RegisterRoute(nameof(Views.AllFoodsPage), typeof(Views.AllFoodsPage));
        Routing.RegisterRoute(nameof(Views.MapPage), typeof(Views.MapPage));
    }
}
