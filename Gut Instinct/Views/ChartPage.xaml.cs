
using Syncfusion.Maui.Charts;

namespace Gut_Instinct.Views;

public partial class ChartPage : ContentPage
{
	public ChartPage(int red, int orange, int green)
	{
		InitializeComponent();

		SfCartesianChart foodChart = new SfCartesianChart();

        List<NumCount> colourNum = new List<NumCount>() { 
            new NumCount { Colour = "Red", Count = red},
            new NumCount { Colour = "Orange", Count = orange},
            new NumCount { Colour = "Green", Count = green}
        };


        foodChart.Title = new Label
        {
            Text = "Food Colour Comparisons"
        };

        CategoryAxis primaryAxis = new CategoryAxis();
        primaryAxis.Title = new ChartAxisTitle
        {
            Text = "Colour",
        };
        foodChart.XAxes.Add(primaryAxis);

        NumericalAxis secondaryAxis = new NumericalAxis();
        secondaryAxis.Title = new ChartAxisTitle
        {
            Text = "No. of food",
        };
        foodChart.YAxes.Add(secondaryAxis);

        ColumnSeries foods = new ColumnSeries()
        {
            Label = "No. of Foods",
            ShowDataLabels = true,
            ItemsSource = colourNum,
            XBindingPath = "Colour",
            YBindingPath = "Count",
            DataLabelSettings = new CartesianDataLabelSettings
            {
                LabelPlacement = DataLabelPlacement.Inner
            }
        };
        
        foodChart.Series.Add(foods);
        this.Content = foodChart;
    }

    public class NumCount { 
        public string Colour { get; set;}
        public int Count { get; set;}
    }
}