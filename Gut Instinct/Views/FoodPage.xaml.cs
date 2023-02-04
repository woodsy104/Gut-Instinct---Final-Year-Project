namespace Gut_Instinct.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class FoodPage : ContentPage
{
    public FoodPage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.foods.txt";

        LoadFood(Path.Combine(appDataPath, randomFileName));
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Food food)
        {
            File.WriteAllText(food.Filename, TextEditor.Text);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Food food)
        {
            // Delete the file.
            if (File.Exists(food.Filename))
                File.Delete(food.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadFood(string fileName)
    {
        Models.Food foodModel = new Models.Food();
        foodModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            foodModel.Date = File.GetCreationTime(fileName);
            foodModel.Text = File.ReadAllText(fileName);
            foodModel.Name = File.ReadAllLines(fileName)[0];
        }
        BindingContext = foodModel;
    }

    public string ItemId
    {
        set { LoadFood(value); }
    }

    private void GreenButton_Clicked(object sender, EventArgs e)
    {

    }

    private void OrangeButton_Clicked(object sender, EventArgs e)
    {

    }

    private void RedButton_Clicked(object sender, EventArgs e)
    {

    }
}