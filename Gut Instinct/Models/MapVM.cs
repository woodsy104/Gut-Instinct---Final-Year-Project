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

                /*var collection = realm.All<ApprovedPin>().ToList();
                var documents = collection.Where(d => d.Partition == "");

                foreach (var pin in documents)
                {
                    ApprovedPins.Add(pin);
                Console.WriteLine("LIST FOUND");
                }
            Console.WriteLine(documents);
            Console.WriteLine(collection);*/
                ActivePin pin = new ActivePin
                {
                    Location = "52.66360947291635, -8.622154572741387",
                    Address = "Milk Market",
                    Label = "Toilet"
                };
                ApprovedPins = new ObservableCollection<ActivePin>();
                ApprovedPins.Add(pin);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy= false;
        }
    }
}
