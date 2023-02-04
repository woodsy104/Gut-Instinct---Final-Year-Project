namespace Gut_Instinct.Views;

public partial class AllFoodsPage : ContentPage
{
    public AllFoodsPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllFood();
    }

    protected override void OnAppearing()
    {
        ((Models.AllFood)BindingContext).LoadFood();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FoodPage));
    }

    private async void foodCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the food model
            var food = (Models.Food)e.CurrentSelection[0];

            // Should navigate to "FoodPage?ItemId=path\on\device\XYZ.foods.txt"
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}?{nameof(FoodPage.ItemId)}={food.Filename}");

            // Unselect the UI
            foodCollection.SelectedItem = null;
        }
    }
}