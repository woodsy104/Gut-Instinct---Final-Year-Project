
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
            IsBusy =  false;
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
        public Task ColourSetOrange()
        {
            //FoodColour = "Orange";
            return null;
        }

        [RelayCommand]
        public Task ColourSetGreen()
        {
            //FoodColour = "Green";
            return null;
        }

        [RelayCommand]
        public async void GetFoodLibrary()
        {
            IsRefreshing = true;
            IsBusy = true;

            try
            {
                var flist = realm.All<Food>().ToList();
                FoodLibrary = new ObservableCollection<Food>(flist);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }

            IsRefreshing = false; IsBusy = false;
        }

        /*[RelayCommand] //Come back to this when trying to get foodpage working
        public async void SelectFood(Food food) {
            IsBusy = true;
            try{
                //await Shell.Current.GoToAsync("///FoodPage");
                //await App.Current.MainPage.DisplayPromptAsync("Edit", food.FoodName);
                //string newDescription = await App.Current.MainPage.DisplayPromptAsync("Edit", food.Description);
                Food foundFood = realm.Find<Food>(food.Id);
                NameText = foundFood.FoodName;
                DescripText = foundFood.Description;
                FoodColour = foundFood.Colour;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy = false;
        }*/

        [RelayCommand]
        public async void EditFood(Food food) {
            
            
            string action = await App.Current.MainPage.DisplayActionSheet("Would you like to edit name or description?", "Cancel", null, null, "Name", "Description");

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
            }
        }
    }
}
