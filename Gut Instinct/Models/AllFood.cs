using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models;
internal class AllFood
{
    public ObservableCollection<Food> Foods { get; set; } = new ObservableCollection<Food>();

    public AllFood() =>
        LoadFood();

    public void LoadFood()
    {
        Foods.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<Food> foods = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.foods.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new Food()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Name = File.ReadAllLines(filename)[0],
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(food => food.Date);

        // Add each note into the ObservableCollection
        foreach (Food food in foods)
            Foods.Add(food);
    }
}
