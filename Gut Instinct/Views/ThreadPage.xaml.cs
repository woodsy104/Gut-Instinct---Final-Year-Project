using Gut_Instinct.Models;
using Microsoft.Maui.Controls;

namespace Gut_Instinct.Views;

public partial class ThreadPage : ContentPage
{
	ForumVM vm;
	public ThreadPage(string title, string content)
	{
		InitializeComponent();
		vm = new ForumVM();
		BindingContext = vm;

		Label titleLabel = this.FindByName<Label>("threadTitle");
		Label contentLabel = this.FindByName<Label>("threadContent");
		titleLabel.Text = title;
		contentLabel.Text = content;

	}

	protected override async void OnAppearing() {
        Label label = this.FindByName<Label>("threadTitle");
        await vm.InitialiseRealm();
        vm.loadComments(label.Text);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		Label label = this.FindByName<Label>("threadTitle");
		await 
		vm.AddComment(label.Text);
    }
}