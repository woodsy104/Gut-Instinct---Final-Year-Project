using Gut_Instinct.Models;
using Gut_Instinct.Views;

namespace Gut_Instinct;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiMaps();

        builder.Services.AddSingleton<Dashboard>();
        builder.Services.AddSingleton<DashboardVM>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginVM>();

        return builder.Build();
    }
}
