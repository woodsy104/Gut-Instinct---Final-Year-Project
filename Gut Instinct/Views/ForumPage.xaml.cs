using Gut_Instinct.Models;

namespace Gut_Instinct.Views;

public partial class ForumPage : ContentPage
{
	ForumVM vm;
	public ForumPage()
	{
		InitializeComponent();
		vm = new ForumVM();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
        //vm.GetForum();
    }

    private async void threadCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {

            // Get the note model
            Models.Thread thread = (Models.Thread)e.CurrentSelection[0];

            //vm.SelectThread(thread);
            string title = thread.Title;
            string content = thread.Content;
            await Navigation.PushAsync(new ThreadPage(title, content));
            forumLibrary.SelectedItem = null;
        }

    }
}