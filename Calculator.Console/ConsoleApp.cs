using Calculator.Core.Operations;
using Calculator.Core;
using static System.Console;
using Calculator.Core.Interfaces;

namespace Calculator.Console
{
    public class ConsoleApp
    {
        private readonly Dictionary<string, IOperation> operations;
        private readonly CalculatorService calculatorService;

        public ConsoleApp()
        {
            operations = OperationRegistry.GetOperations();
            calculatorService = new CalculatorService(operations);
        }

        public void Run()
        {
            WriteLine("Доступные операции:");
            foreach (var key in operations.Keys)
            {
                WriteLine($" - {key}");
            }

            while (true)
            {
                WriteLine("\nop :");
                var ouInput = ReadLine();

                if (string.IsNullOrWhiteSpace(ouInput))
                {
                    WriteLine("Введите операцию");
                    continue;
                }

                if (ouInput.Trim().ToLower() == "exit")
                    break;

                if (!operations.TryGetValue(ouInput.Trim(), out var operation))
                {
                    WriteLine("Ошибка, операция не найдена");
                    continue;
                }

                Write("args:");
                var argsInput = ReadLine();
                if (string.IsNullOrWhiteSpace(argsInput))
                {
                    WriteLine("Ошибка, аргументы не введены.");
                    continue;
                }

                string[] parts = argsInput.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                double[] arg = new double[parts.Length];

                try
                {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        arg[i] = double.Parse(parts[i]);
                    }

                    double result = operation.Call(arg);
                    WriteLine($"Результат: {result}");
                }
                catch (FormatException)
                {
                    WriteLine("Ошибка: неверный формат аргументов.");
                }
                catch (ArgumentException ex)
                {
                    WriteLine($"Ошибка: {ex.Message}");
                }
                catch (DivideByZeroException)
                {
                    WriteLine("Ошибка: деление на ноль.");
                }
                catch (Exception ex)
                {
                    WriteLine($"Ошибка выполнения: {ex.Message}");
                }
            }

            WriteLine("Завершение операций!");
        }
    }
}