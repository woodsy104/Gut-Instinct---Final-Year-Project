using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms;
using Realms.Sync;

namespace Gut_Instinct.Models
{
    public partial class PinVM : Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public PinVM()
        {
            PinText = "Add a new toilet here!!";
        }

        [ObservableProperty]
        string pinText;

        [ObservableProperty]
        string address;

        [ObservableProperty]
        bool isFree;

        [ObservableProperty]
        int stars;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

            await Task.Delay(1000);
            PinText = "Add a new toilet here!!";

        }

        [RelayCommand]
        async Task AddPin()
        {
            if (string.IsNullOrWhiteSpace(Address))
                return;
            IsBusy= true;
            try
            {
                var newPin = new Pin
                {
                    Address = Address,
                    Partition = App.RealmApp.CurrentUser.Id,
                    Owner = App.RealmApp.CurrentUser.Profile.Email,
                    Free = IsFree,
                    Stars = Stars
                };
                realm.Write(() =>
                {
                    realm.Add(newPin);
                });
                Address = "";
                //Stars = 0;
                //IsFree = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            await Shell.Current.GoToAsync("/MapPage");
            IsBusy = false;
        }
    }
}
