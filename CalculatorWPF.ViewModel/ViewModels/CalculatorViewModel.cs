using Calculator.Core;
using Calculator.Core.Interfaces;
using Calculator.Core.Operations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CalculatorWPF.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject
    {
        private readonly Dictionary<string, IOperation> _operations;
        private readonly CalculatorService _calculatorService;
        private readonly CalculatorCommandHandler _commandHandler;

        public CalculatorViewModel(
             Dictionary<string, IOperation> operations,
             CalculatorService calculatorService,
             CalculatorCommandHandler commandHandler)
        {
            _operations = OperationRegistry.GetOperations();
            _calculatorService = new CalculatorService(_operations);
            _commandHandler = new CalculatorCommandHandler(_calculatorService);

            FunctionNames = _operations.Keys
                .Where(name => name != "+" && name != "-" && name != "*" && name != "/")
                .OrderBy(name => name)
                .ToList();
        }

        [ObservableProperty]
        private string _input = "";

        [ObservableProperty]
        private string _displayText = "0";

        [ObservableProperty]
        private List<string> _functionNames;

        [ObservableProperty]
        private string _selectedFunction;

        [RelayCommand]
        public void ButtonClick(string content)
        {
            if (content == "=" || content == "C" || _operations.ContainsKey(content))
            {
                Input = _commandHandler.HandleButtonClick(Input, content);
                DisplayText = string.IsNullOrEmpty(Input) ? "0" : Input; 
            }
        }

        [RelayCommand]
        private void FunctionComboboxSelection(string selectedOperator)
        {
            string[] parts = Input.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            var binaryOnlyOperators = new[] { "pow", "atan2" };

            if (!binaryOnlyOperators.Contains(selectedOperator))
            {
                if (parts.Length == 1 && double.TryParse(parts[0], out _))
                {
                    Input = $"{selectedOperator} {parts[0]}";
                    DisplayText = _calculatorService.Calculate(Input.Trim());
                }
                else
                {
                    if (parts.Length > 0 && _operations.ContainsKey(parts[^1]))
                    {
                        parts[^1] = selectedOperator;
                        Input = string.Join(" ", parts);
                    }
                    else
                    {
                        Input += $" {selectedOperator} ";
                    }
                }
            }
            else
            {
                Input += $" {selectedOperator} ";
            }

            Input = Regex.Replace(Input.Trim(), @"\s+", " ");
            DisplayText = Input;
        }

        partial void OnSelectedFunctionChanged(string value)
        {
            FunctionComboboxSelection(value);
        }
        [RelayCommand]
        private void KeyDown(string key)
        {
            if (key == "Enter")
            {
                ButtonClick("=");
            }
            else if (key == "Back")
            {
                if (!string.IsNullOrEmpty(Input))
                {
                    Input = Input[..^1];
                }
            }
        }

    }
}