using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            approvedPins = new ObservableCollection<ApprovedPin>();
        }

        [ObservableProperty]
        ObservableCollection<ApprovedPin> approvedPins;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

                await Task.Delay(2000);
                GetApprovedPins();
        }

        [RelayCommand]
        public async void GetApprovedPins() { 
            IsBusy= true;

            try
            {
                var pinList = realm.All<ApprovedPin>().ToList();
                ApprovedPins = new ObservableCollection<ApprovedPin>(pinList);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy= false;
        }
    }
}
