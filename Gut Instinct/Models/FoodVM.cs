
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Converters;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public partial class FoodVM :Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public FoodVM()
        {
            foodLibrary = new ObservableCollection<Food>();
            NameText = "";
            DescripText = "";
            FoodColour = "Blue";
            LibText = "Add some foods to your library!";
        }

        [ObservableProperty]
        ObservableCollection<Food> foodLibrary;

        [ObservableProperty]
        string libText;

        [ObservableProperty]
        string nameText;

        [ObservableProperty]
        string descripText;

        [ObservableProperty]
        string foodColour;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string redCount;

        [ObservableProperty]
        string orangeCount;

        [ObservableProperty]
        string greenCount;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

            GetFoodLibrary();
            if (FoodLibrary.Count == 0) {

                LibText = "Add some foods to your library!";
                await Task.Delay(1000);
                GetFoodLibrary();
            }
        }

        [RelayCommand]
        async Task AddFood()
        {
            if (string.IsNullOrWhiteSpace(NameText) || string.IsNullOrWhiteSpace(DescripText) || string.IsNullOrWhiteSpace(FoodColour))
                return;
            IsBusy = true;
            bool present = false;

            foreach (Food food in realm.All<Food>().ToList()) {
                if (food.FoodName.ToUpper() == NameText.ToUpper()) {
                    present = true; break;
                }
            }

            if (present == false) {
                try
                {
                    var newFood = new Food {
                        FoodName = NameText,
                        Owner = App.RealmApp.CurrentUser.Profile.Email,
                        Description = DescripText,
                        Colour = FoodColour
                    };
                    realm.Write(() =>
                    {
                        realm.Add(newFood);
                    });
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                }
                await Shell.Current.GoToAsync("/AllFoodsPage");
                IsBusy = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "That food is already in your library!", "OK");
            }
        }

        [RelayCommand]
        public async void DeleteFood(Food food) {
            IsBusy = true;
            try
            {
                realm.Write(() => {
                    realm.Remove(food);
                });
                FoodLibrary.Remove(food);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy = false;
        }


        [RelayCommand]
        public async void GetFoodLibrary()
        {
            IsRefreshing = true;
            IsBusy = true;
            int Reds = 0; int Oranges = 0; int Greens = 0;
            try
            {
                var flist = realm.All<Food>().ToList().OrderBy(f => f.FoodName);
                FoodLibrary = new ObservableCollection<Food>(flist);

                foreach (Food e in FoodLibrary) {
                    switch (e.Colour)
                    {
                        case "Green":
                            Greens++;
                            break;
                        case "DarkOrange":
                            Oranges++;
                            break;
                        case "Red":
                            Reds++;
                            break;
                    }
                }
                GreenCount = Greens.ToString();
                OrangeCount= Oranges.ToString();
                RedCount = Reds.ToString();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }

            IsRefreshing = false; IsBusy = false;
        }


        [RelayCommand]
        public async void EditFood(Food food) {
            
            
            string action = await App.Current.MainPage.DisplayActionSheet("Would you like to edit name or description?", "Cancel", null, "Name", "Description", "Colour");

            switch (action) {

                case "Name":
                    string newName = await App.Current.MainPage.DisplayPromptAsync("Edit Name", food.FoodName);

                    if (newName is null || string.IsNullOrWhiteSpace(newName.ToString()))
                    {
                        return;
                    }

                    try
                    {
                        realm.Write(() =>
                        {
                            var foundFood = realm.Find<Food>(food.Id);
                            foundFood.FoodName = newName.ToString();
                        }
                        );
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                    }
                    break;

                case "Description":
                    string newDescript = await App.Current.MainPage.DisplayPromptAsync("Edit Description", food.Description);

                    if (newDescript is null || string.IsNullOrWhiteSpace(newDescript.ToString()))
                    {
                        return;
                    }

                    try
                    {
                        realm.Write(() =>
                        {
                            var foundFood = realm.Find<Food>(food.Id);
                            foundFood.Description = newDescript.ToString();
                        }
                        );
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                    }
                    break;

                case "Colour":
                    string colour = await App.Current.MainPage.DisplayActionSheet("What colour would you like to pick", "Cancel", null, "Green", "Orange", "Red");
                    try
                    {
                        if (colour == "Orange") {
                            colour = "DarkOrange";
                        }
                        realm.Write(() =>
                        {
                            var foundFood = realm.Find<Food>(food.Id);
                            foundFood.Colour = colour.ToString();
                        }
                        );
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                    }
                    break;
            }
            GetFoodLibrary();
        }
    }
}
