using CalculatorWPF.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Calculator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = new CalculatorViewModel(); 
        mainWindow.Show();
    }
}
