using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Realms;
using Realms.Sync;
using System.Collections.ObjectModel;

namespace Gut_Instinct.Models
{
    public partial class MapVM :Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public MapVM()
        {
            approvedPins = new ObservableCollection<ActivePin>();
           
        }

        [ObservableProperty]
        ObservableCollection<ActivePin> approvedPins;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

                await Task.Delay(100);
                GetApprovedPins();
        }

        [RelayCommand]
        public async void GetApprovedPins() { 
            IsBusy= true;
            ApprovedPins.Clear();
            try
            {
                var allUsers = App.RealmApp.AllUsers;
                foreach (var user in allUsers)
                {
                    config = new PartitionSyncConfiguration($"{user.Id}", user);
                    realm = await Realm.GetInstanceAsync(config);

                    foreach (ActivePin pin in realm.All<ActivePin>().ToList())
                    {
                        ApprovedPins.Add(pin);
                    }   
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy= false;
        }
    }
}
