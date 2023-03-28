using Gut_Instinct.Models;

namespace Gut_Instinct.Views;

public partial class AllNotesPage : ContentPage
{
    NoteVM vm;
    public AllNotesPage()
    {
        InitializeComponent();
        vm = new NoteVM();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        await vm.InitialiseRealm();
        vm.GetNotes();
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Note)e.CurrentSelection[0];

            
            await DisplayAlert(note.NoteText,null, "OK");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}