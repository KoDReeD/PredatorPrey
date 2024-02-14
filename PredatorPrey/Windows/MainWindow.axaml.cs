using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PredatorPrey.Models.ViewModels;
using ScottPlot;

namespace PredatorPrey;

public partial class MainWindow : Window
{
    private GraphicsVM _data = new GraphicsVM(400, 20, 0.9, 0.023, 0.0012, 0.3);
    //модель для listbox
    private List<RowModel> rows = new List<RowModel>();
    private int rowNum = 1;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _data;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _data = DataContext as GraphicsVM;

        double[] preys = new double[150];
        double[] predators = new double[150];

        var preyPrev = _data.Prey;
        var predatorPrev = _data.Predator;

        for (int i = 0; i < 150; i++)
        {
            double preyNum = 0;
            double predatorNum = 0;
            //  если первое поколение цикла то берём введённые значения
            if (i == 0)
            {
                preyNum = preyPrev;
                predatorNum = predatorPrev;
            }
            else
            {
                preyNum = (_data.E - _data.a * predatorPrev) * preyPrev + preyPrev;
                predatorNum = (_data.o * preyNum- _data.b) * predatorPrev + predatorPrev;
            }
            
            preys[i] = preyNum;
            predators[i] = predatorNum;
            rows.Add(new RowModel()
            {
                PredatorNum = predatorNum,
                PreyNum = preyNum,
                RowNum = rowNum
            });
            rowNum++;
            
            preyPrev = preyNum;
            predatorPrev = predatorNum;
        }

        ListBoxData.Items = new List<RowModel>();
        ListBoxData.Items = rows;
        ListBoxData.InvalidateVisual();

        GraphicsPlot.Plot.AddScatter(preys,predators);
        GraphicsPlot.Refresh();
    }

    public class RowModel
    {
        public int RowNum { get; set; }
        public double PreyNum { get; set; }
        public double PredatorNum { get; set; }
    }

    private void Button_Clear_OnClick(object? sender, RoutedEventArgs e)
    {
        GraphicsPlot.Plot.Clear();
        GraphicsPlot.Refresh();
        rows = new List<RowModel>();
        ListBoxData.Items = rows;
    }
}