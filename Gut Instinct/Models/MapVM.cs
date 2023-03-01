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
            ActivePin pin = new ActivePin
            {
                Label = "Arthurs Quay",
                Address = "Shopping Center",
                
                Location = new Location(52.66482630113309, -8.625857537927548)
            };
            approvedPins.Add(pin);

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
            //ApprovedPins.Clear();
            try
            {
                
                    var collection = realm.All<ApprovedPin>().ToList();
                    var documents = collection.Where(d => d.Partition == "");

                    foreach (var pin in documents)
                    {
                        ActivePin newPin = new ActivePin
                        {
                            Location = new Location(pin.XCoord, pin.YCoord),
                            Address = pin.Name,
                            Label = pin.Name
                            
                        };
                        Console.WriteLine(pin.Name);
                        ApprovedPins.Add(newPin);
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
