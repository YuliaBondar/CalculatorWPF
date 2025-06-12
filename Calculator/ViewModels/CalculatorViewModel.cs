using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core;
using Calculator.Core.Operations;
using Calculator.Core.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection.Metadata;
using CalculatorWPF.Command;


namespace CalculatorWPF.ViewModels
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _input = "";
        private string _displayText = "";
        private string _selectedOperator = "";
        private readonly Dictionary<string, IOperation> _operations;
        private readonly CalculatorService _calculatorService;
       public string Input
       {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
       }

        public string DisplayText
        {
            get => _displayText;
            set
            {
                _displayText = value;
                OnPropertyChanged(nameof(DisplayText));

            }
        }

        public ICommand ButtonClickCommand { get; }
        public ICommand FunctionComboBox_SelectionChanged { get; }

        private List<string> _functionNames;
        public List<string> FunctionNames
        {
            get => _functionNames;
            set
            {
                _functionNames = value;
                OnPropertyChanged(nameof(FunctionNames));
            }
        }

        private string _selectedFunction;
        public string SelectedFunction
        {
            get => _selectedFunction;
            set
            {
                _selectedFunction = value;
                OnPropertyChanged(nameof(SelectedFunction));
                FunctionComboboxSelection(value); // вызываем при выборе
            }
        }

        public CalculatorViewModel()
        {
            _operations = OperationRegistry.GetOperations();
            _calculatorService = new CalculatorService(_operations);

            ButtonClickCommand = new RelayCommand<string>(Button_Click);
            FunctionComboBox_SelectionChanged = new RelayCommand<string>(FunctionComboboxSelection);

            FunctionNames = _operations.Keys
                .Where(name => name != "+" && name != "-" && name != "*" && name != "/") 
                .OrderBy(name => name)
                .ToList();
        }


        private void Button_Click(string content)
        {
            if(content == "=")
            {
                DisplayText = _calculatorService.Calculate(Input.Trim());
                Input = DisplayText;
            }
            else if(content == "C")
            {
                Input = " ";
                DisplayText = " ";
            }
            else if( content == "-"){

                HandleMinus();
            }

            else if(content == ".")
            {
                HandleDecimal();
            }
            else if(_operations.ContainsKey(content))
            {
                HandleOperations(content);
            }
            else if (char.IsDigit(content[0]))
            {
                HandleDigit(content);
            }
        }

        private void HandleMinus()
        {
            string[] parts = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (Input == "" || (parts.Length > 0 && _operations.ContainsKey(parts[^1])))
            {
                Input += "-";
            }
            else
            {
                Input += " - ";
            }
            DisplayText = Input;
        }

        private void HandleDecimal()
        {
            string[] parts = Input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string lastPart = parts.Length > 0 ? parts[^1] : "";

            if (!lastPart.Contains("."))
            {
                Input += ".";
                DisplayText = Input;
            }
        }

        private void HandleOperations(string content)
        {
            _selectedOperator = content;
            string[] parts = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

        private void HandleDigit(string content)
        {
            if (Input.Length > 0 && Input[^1] != ' ' && !char.IsDigit(Input[^1]) && Input[^1] != '.')
            {
                Input += " ";
            }

            Input += content;
            DisplayText = Input;
        }


        private void FunctionComboboxSelection(string selectedOperator)
        {
            _selectedOperator = selectedOperator;
            string[] parts = Input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var binaryOnlyOperators = new[] { "pow", "atan2" };

            if (!binaryOnlyOperators.Contains(_selectedOperator))
            {
                if (parts.Length == 1 && double.TryParse(parts[0], out _))
                {
                    Input = $"{_selectedOperator} {parts[0]}";
                }
                else
                {
                    if (parts.Length > 0 && _operations.ContainsKey(parts[^1]))
                    {
                        parts[^1] = _selectedOperator;
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

            DisplayText = Input;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
