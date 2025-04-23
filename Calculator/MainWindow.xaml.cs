using Calculator.Interfaces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

public partial class MainWindow : Window
{
    private string input = "";
    private string selectedOperator = "";
    private readonly Dictionary<string, IOperation> operations;
    private readonly CalculatorService calculatorService;

    public MainWindow()
    {
        InitializeComponent();
        operations = OperationRegistry.GetOperations();
        calculatorService = new CalculatorService(operations);

        //автозаполнение
        var excludedOperators = new[] { "+", "-", "*", "/" };

        foreach (var op in operations.Keys)
        {
            if (!excludedOperators.Contains(op))
            {
                FunctionComboBox.Items.Add(new ComboBoxItem { Content = op });
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var content = (sender as Button)?.Content.ToString();

        if (content == "=")
        {
            Display.Text = calculatorService.Calculate(input);
            input = Display.Text;
        }
        else if (content == "C")
        {
            input = "";
            selectedOperator = "";
            Display.Text = "";
        }
        else if (operations.ContainsKey(content))
        {
            selectedOperator = content;
            input += $" {content} ";
            Display.Text = input;
        }
        else
        {
            input += content;
            Display.Text = input;
        }
    }

    private void FunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (FunctionComboBox.SelectedItem is ComboBoxItem item)
        {
            selectedOperator = item.Content.ToString();

            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Операции, которые обрабатываются как бинарные
            var binaryOnlyOperators = new[] { "pow", "atan2" };

            if (!binaryOnlyOperators.Contains(selectedOperator))
            {
                if (parts.Length == 1 && double.TryParse(parts[0], out _))
                {
                    // Унарная операция: ставим оператор перед числом
                    input = $"{selectedOperator} {parts[0]}";
                }
                else
                {
                    // Общий случай для унарной — просто добавляем в конец
                    input += $" {selectedOperator} ";
                }
            }
            else
            {
                // Для pow, atan2 — только в конец
                input += $" {selectedOperator} ";
            }

            Display.Text = input;
            FunctionComboBox.SelectedIndex = -1;
        }
    }


}