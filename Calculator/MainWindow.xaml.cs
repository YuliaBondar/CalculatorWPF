using System.Windows;
using CalculatorWPF.ViewModels;

namespace Calculator;

public partial class MainWindow : Window
{
    

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new CalculatorViewModel();
    }
    


}