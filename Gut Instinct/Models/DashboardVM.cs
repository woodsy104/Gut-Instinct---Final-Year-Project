using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gut_Instinct.Helpers;
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
    public partial class DashboardVM : Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public DashboardVM() { 
            appointmentList= new ObservableCollection<Appointment>();
            EmptyText = "No Appointments coming up!";
        }

        [ObservableProperty]
        ObservableCollection<Appointment> appointmentList;

        [ObservableProperty]
        string emptyText;

        [ObservableProperty]
        string appEntryText;

        [ObservableProperty]
        bool isRefreshing;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

            GetAppointments();
            if (AppointmentList.Count == 0)
            {
                EmptyText = "loading....";
                await Task.Delay(2000);
                GetAppointments();
                EmptyText = "No Appointments coming up!";
            }

        }

        [RelayCommand]
        public async void GetAppointments()
        {
            IsRefreshing = true;
            IsBusy = true;

            try
            {
                var tlist = realm.All<Appointment>().ToList().Reverse<Appointment>().OrderBy(t => t.Completed);
                AppointmentList = new ObservableCollection<Appointment>(tlist);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand]
        public async void EditAppointment(Appointment app) {
            string editApp = await App.Current.MainPage.DisplayActionSheet("Would you like to edit the Note or it's category", "Cancel", null, null, "Note", "Category");

            switch (editApp) {
               case "Note":
                    string newText = await App.Current.MainPage.DisplayPromptAsync("Edit", app.Name);

                    if (newText is null || string.IsNullOrWhiteSpace(newText.ToString()))
                    {
                        return;
                    }

                    try
                    {
                        realm.Write(() =>
                        {
                            var foundApp = realm.Find<Appointment>(app.Id);
                            foundApp.Name = GeneralHelpers.UpperCaseFirst(newText.ToString());
                        }
                        );
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    }
                    break;

                case "Category":
                    string checkApp = await App.Current.MainPage.DisplayActionSheet("What would you like to change the appointment to?", "Cancel", null, "GP", "Specialist", "Other");
                    string newColour = "";

                    if (checkApp == "GP") {
                        newColour = "LightBlue";
                    } else if (checkApp == "Specialist") {
                        newColour = "Pink";
                    }
                    else
                    {
                        newColour = "Yellow";
                    }
                    try
                    {
                        realm.Write(() =>
                        {
                            var foundApp = realm.Find<Appointment>(app.Id);
                            foundApp.Colour = newColour.ToString();
                        }
                        );
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    }
                    break;
            }
       }
        

        [RelayCommand]
        public async void CheckAppointment(Appointment app)
        {
            IsBusy = true;
            try
            {
                realm.Write(() =>
                {
                    var foundApp = realm.Find<Appointment>(app.Id);
                    foundApp.Completed = !foundApp.Completed;
                });

                await Task.Delay(2);
                var sortedList = AppointmentList.ToList().OrderBy(t => t.Completed);
                AppointmentList = new ObservableCollection<Appointment>(sortedList);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        public async void SignOut()
        { //Move signout to the flyout menu if possible or just somewhere else
            IsBusy = true;
            try
            {
                var currentuserId = App.RealmApp.CurrentUser.Id;

                await App.RealmApp.RemoveUserAsync(App.RealmApp.CurrentUser);

                var logOutUser = App.RealmApp.AllUsers.FirstOrDefault(u => u.Id == currentuserId);

                await Shell.Current.GoToAsync("///Login");
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task AddAppointment()
        {
            if (string.IsNullOrWhiteSpace(AppEntryText))
                return;
            string checkApp = await App.Current.MainPage.DisplayActionSheet("Is this appointment for the GP, Specialist or Other", "Cancel", null, "GP", "Specialist", "Other");
            string newColour = "";
            if (checkApp == "GP") {
                newColour = "LightBlue";
            } else if (checkApp == "Specialist") {
                newColour = "Pink";
            }
            else
            {
                newColour = "Yellow";
            }
            IsBusy = true;
            try
            {
                var app =
                    new Appointment
                    {
                        Name = GeneralHelpers.UpperCaseFirst(AppEntryText),
                        Partition = App.RealmApp.CurrentUser.Id,
                        Owner = App.RealmApp.CurrentUser.Profile.Email,
                        Colour = newColour
                    };
                realm.Write(() =>
                {
                    realm.Add(app);
                });

                AppEntryText = "";
                GetAppointments();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task DeleteTask(Appointment app) {
            IsBusy = true;
            try
            {
                realm.Write(()=>{
                    realm.Remove(app);
                });

                AppointmentList.Remove(app);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }
        
    }
}
