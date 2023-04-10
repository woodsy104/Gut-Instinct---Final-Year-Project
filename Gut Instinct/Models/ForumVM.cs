using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public partial class ForumVM : Base
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public ForumVM() { 
            threadList = new ObservableCollection<Thread>();
            commentList = new ObservableCollection<Comment>();
            DisplayText = "No threads to be answered!";
        }

        [ObservableProperty]
        ObservableCollection<Thread> threadList;

        [ObservableProperty]
        ObservableCollection<Comment> commentList;

        [ObservableProperty]
        string titleEntryText;

        [ObservableProperty]
        string threadEntryText;

        [ObservableProperty]
        string threadOwnerText;

        [ObservableProperty]
        string displayText;

        [ObservableProperty]
        string commentText;

        public async Task InitialiseRealm() {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            
            //GetForum();
            if (ThreadList.Count == 0)
            {
                DisplayText = "Loading...";
                await Task.Delay(2000);
                GetForum();
                DisplayText = "No Threads to be answered!";
            }
        }

        [RelayCommand]
        public async void GetForum() {
            IsBusy = true;
            ThreadList.Clear();
            try
            {
                var allUsers = App.RealmApp.AllUsers;
                
                foreach (var user in allUsers) { 
                    config = new PartitionSyncConfiguration($"{user.Id}", user);
                    realm = await Realm.GetInstanceAsync(config);
                    
                    foreach (Thread thread in realm.All<Thread>().ToList()) { 
                        ThreadList.Add(thread);
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task AddThread()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            string ThreadOwnerText = await App.Current.MainPage.DisplayPromptAsync("Enter a Name", "Be anonymous or have your name displayed!");
            string TitleEntryText = await App.Current.MainPage.DisplayPromptAsync("Enter a Title", "Make it eye grabbing!");
            string ThreadEntryText = await App.Current.MainPage.DisplayPromptAsync("Details", "Add some of the fine print!");
            if (string.IsNullOrWhiteSpace(ThreadEntryText))
                return;
            IsBusy = true;
            try
            {
                var thread =
                    new Thread
                    {
                        Title = TitleEntryText,
                        Content = ThreadEntryText,
                        Partition = App.RealmApp.CurrentUser.Id,
                        Owner = ThreadOwnerText
                    };
                realm.Write(() =>
                {
                    realm.Add(thread);
                });

                ThreadEntryText = "";
                GetForum();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task DeleteThread(Thread thread)
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            IsBusy = true;
            try
            {
                if (thread.Partition == App.RealmApp.CurrentUser.Id) {
                    realm.Write(() => {
                        realm.Remove(thread);
                    });

                    ThreadList.Remove(thread);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You can't delete what isn't yours!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        public async void EditThread(Thread thread)
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            IsBusy = true;
            if (thread.Partition == App.RealmApp.CurrentUser.Id) {
                string action = await App.Current.MainPage.DisplayActionSheet("Would you like to edit title or description?", "Cancel", null, "Name", "Title", "Description");
                switch (action)
                {
                    case "Name":
                        string newName = await App.Current.MainPage.DisplayPromptAsync("Edit Name", thread.Owner);

                        if (newName is null || string.IsNullOrWhiteSpace(newName.ToString()))
                        {
                            return;
                        }

                        try
                        {
                            realm.Write(() =>
                            {
                                var foundThread = realm.Find<Thread>(thread.Id);
                                foundThread.Owner = newName.ToString();
                            }
                            );
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                        }
                        break;

                    case "Title":
                        string newTitle = await App.Current.MainPage.DisplayPromptAsync("Edit Title", thread.Title);

                        if (newTitle is null || string.IsNullOrWhiteSpace(newTitle.ToString()))
                        {
                            return;
                        }

                        try
                        {
                            realm.Write(() =>
                            {
                                var foundThread = realm.Find<Thread>(thread.Id);
                                foundThread.Title = newTitle.ToString();
                            }
                            );
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
                        }
                        break;

                    case "Description":
                        string newDescript = await App.Current.MainPage.DisplayPromptAsync("Edit Description", thread.Content);

                        if (newDescript is null || string.IsNullOrWhiteSpace(newDescript.ToString()))
                        {
                            return;
                        }

                        try
                        {
                            realm.Write(() =>
                            {
                                var foundThread = realm.Find<Thread>(thread.Id);
                                foundThread.Content = newDescript.ToString();
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
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can't edit what isn't yours!", "OK");
            }
            IsBusy = false;
        }

        [RelayCommand]
        public async Task AddComment(String threadTitle) {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            string ThreadOwnerText = await App.Current.MainPage.DisplayPromptAsync("Enter a Name", "Be anonymous or have your name displayed!");
            IsBusy = true;
            if (string.IsNullOrWhiteSpace(CommentText))
                return;
            try
            {
                var comment = new Comment
                {
                    ThreadTitle = threadTitle,
                    Body = CommentText,
                    Partition = App.RealmApp.CurrentUser.Id,
                    Owner = ThreadOwnerText
                };
                realm.Write(() => {
                    realm.Add(comment);
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            CommentText = "";
            IsBusy = false;
            loadComments(threadTitle);
        }

        [RelayCommand]
        async Task DeleteComment(Comment comment)
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            IsBusy = true;
            try
            {
                if (comment.Partition == App.RealmApp.CurrentUser.Id)
                {
                    realm.Write(() => {
                        realm.Remove(comment);
                    });

                    CommentList.Remove(comment);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You can't delete what isn't yours!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }


        [RelayCommand]
        public async void EditComment(Comment comment)
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);
            IsBusy = true;
            if (comment.Partition == App.RealmApp.CurrentUser.Id)
            {
                string newText = await App.Current.MainPage.DisplayPromptAsync("Edit", comment.Body);

                if (newText is null || string.IsNullOrWhiteSpace(newText.ToString()))
                {
                    return;
                }

                try
                {
                    realm.Write(() =>
                    {
                        var foundComment = realm.Find<Comment>(comment.Id);
                        foundComment.Body = newText.ToString();
                    }
                    );
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                IsBusy = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can't edit what isn't yours!", "OK");
            }
        }

        [RelayCommand]
        public async void loadComments(String threadTitle) {
            CommentList.Clear();
            IsBusy = true;
            try
            {
                var allUsers = App.RealmApp.AllUsers;
                foreach (var user in allUsers) {

                    config = new PartitionSyncConfiguration($"{user.Id}", user);
                    realm = await Realm.GetInstanceAsync(config);

                    foreach (Comment comment in realm.All<Comment>().ToList())
                    {
                        if (comment.ThreadTitle == threadTitle)
                        {
                            CommentList.Add(comment);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy= false;
        }
    }
}
