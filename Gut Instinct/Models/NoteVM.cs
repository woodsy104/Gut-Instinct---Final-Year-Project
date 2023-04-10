using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class NoteVM : Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public NoteVM()
        {
            noteList = new ObservableCollection<Note>();
            EmptyNote = "No Notes Yet!";
        }

        [ObservableProperty]
        ObservableCollection<Note> noteList;

        [ObservableProperty]
        string noteEntryText;

        [ObservableProperty]
        string emptyNote;

        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

            GetNotes();
            if (NoteList.Count == 0) {
                EmptyNote = "Loading...";
                await Task.Delay(1000);
                GetNotes();
                EmptyNote = "No Notes Yet!";
            }
        }

        [RelayCommand]
        public async void GetNotes()
        {
            IsBusy = true;

            try
            {
                var tlist = realm.All<Note>().ToList();
                NoteList = new ObservableCollection<Note>(tlist);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async void EditNote(Note note)
        {
            string newText = await App.Current.MainPage.DisplayPromptAsync("Edit", note.NoteText);
            

            if (newText is null || string.IsNullOrWhiteSpace(newText.ToString()))
            {
                return;
            }

            try
            {
                realm.Write(() =>
                {
                    var foundNote = realm.Find<Note>(note.Id);
                    foundNote.NoteText = newText.ToString();
                }
                );
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task AddNote()
        {            
            if (string.IsNullOrWhiteSpace(NoteEntryText))
                return;
            IsBusy = true;
            try
            {
                var note =
                    new Note
                    {
                        NoteText = NoteEntryText,
                        Partition = App.RealmApp.CurrentUser.Id,
                        Owner = App.RealmApp.CurrentUser.Profile.Email
                    };
                realm.Write(() =>
                {
                    realm.Add(note);
                });

                NoteEntryText = "";
                GetNotes();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task DeleteTask(Note note)
        {
            IsBusy = true;
            try
            {
                realm.Write(() => {
                    realm.Remove(note);
                });

                NoteList.Remove(note);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }
    }
}
