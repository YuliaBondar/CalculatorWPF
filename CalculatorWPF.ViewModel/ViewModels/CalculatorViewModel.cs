using Calculator.Core;
using Calculator.Core.Interfaces;
using Calculator.Core.Operations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace CalculatorWPF.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject
    {
        private readonly Dictionary<string, IOperation> _operations;
        private readonly CalculatorService _calculatorService;

        public CalculatorViewModel()
        {
            _operations = OperationRegistry.GetOperations();
            _calculatorService = new CalculatorService(_operations);

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
            if (content == "=")
            {
                DisplayText = _calculatorService.Calculate(Input.Trim());
                Input = DisplayText;
            }
            else if (content == "C")
            {
                Input = "";
                DisplayText = "0";
            }
            else if (_operations.ContainsKey(content))
            {
                HandleOperations(content);
            }
        }

        private void HandleOperations(string content)
        {
            string[] parts = Input.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0 && _operations.ContainsKey(parts[^1]))
            {
                parts[^1] = content;
                Input = string.Join(" ", parts);
            }
            else
            {
                Input += $" {content} ";
            }

            DisplayText = Input;
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
    }
}
