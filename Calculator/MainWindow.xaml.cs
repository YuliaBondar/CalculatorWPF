using CalculatorWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Calculator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Display_KeyDown(object sender, KeyEventArgs e)
    {
        var viewModel = (CalculatorViewModel)DataContext;

        if (e.Key == Key.Enter)
        {
            viewModel.ButtonClick("=");
            e.Handled = true; 
        }
        else if (e.Key == Key.Back)
        {
            if (viewModel.Input.Length > 0)
            {
                viewModel.Input = viewModel.Input.Substring(0, viewModel.Input.Length - 1);
            }
            e.Handled = true;
        }
    }



}