using Calculator.Core;
using Calculator.Core.Operations;
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

        var operations = OperationRegistry.GetOperations();
        var service = new CalculatorService(operations);
        var handler = new CalculatorCommandHandler(service);

        var viewModel = new CalculatorViewModel(operations, service, handler);

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };
        mainWindow.Show();
    }

}
