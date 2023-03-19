
using Gut_Instinct.Models;
using Microsoft.Maui.Graphics.Converters;
using Syncfusion.Maui.Core.Internals;

namespace Gut_Instinct.Views;

public partial class AllFoodsPage : ContentPage
{
    FoodVM vm;
    public AllFoodsPage()
    {
        InitializeComponent();
        vm = new FoodVM();
        BindingContext= vm;
       
    }

    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
        vm.GetFoodLibrary();
        
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FoodPage));
    }


    private async void foodCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            
            Food food = (Food)e.CurrentSelection[0];

            await DisplayAlert(food.FoodName, food.Description, "OK");
            
            foodLibrary.SelectedItem = null;
        }
            
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("What do the colours mean?!", "Green = Good; " +
            "Orange = Sometimes good, Sometimes not; " + "Red = Bad, Avoid;", "OK");
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        int red = int.Parse(RedCount.Text.ToString());
        
        int orange = int.Parse(OrangeCount.Text.ToString());
        int green = int.Parse(GreenCount.Text.ToString());
        
        await Navigation.PushAsync(new ChartPage(red, orange, green));
    }
}