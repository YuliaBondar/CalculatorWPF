using Calculator.Core.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator.Core
{
    public class CalculatorService
    {
        private readonly Dictionary<string, IOperation> operations;

        public CalculatorService(Dictionary<string, IOperation> operations)
        {
            this.operations = operations;
        }

        public string Calculate(string input)
        {
            try
            {
             
                input = Regex.Replace(input, @"(?<=\S)([+\-*/])(?=\S)", " $1 ");

                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                parts = parts.Select(p => p.Replace(',', '.')).ToArray();

                string op = parts.FirstOrDefault(p => operations.ContainsKey(p));
                if (op == null)
                {
                    return "Ошибка: операция не выбрана";
                }

                if (!operations.TryGetValue(op, out var operation))
                {
                    return "Ошибка: неизвестная операция";
                }

                double[] numbers = parts
                    .Where(p => double.TryParse(p, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                    .Select(p => double.Parse(p, CultureInfo.InvariantCulture))
                    .ToArray();

                if (operation is Operations.UnaryOperation)
                {
                    if (numbers.Length < 1)
                        return "Ошибка: операция требует 1 аргумент.";
                    return operation.Call(numbers[0]).ToString(CultureInfo.InvariantCulture);
                }
                else if (operation is Operations.BinaryOperation)
                {
                    if (numbers.Length < 2)
                        return "Ошибка: операция требует 2 аргумента.";
                    return operation.Call(numbers[0], numbers[1]).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    return "Ошибка: неизвестный тип операции.";
                }
            }
            catch (Exception ex)
            {
                return "Ошибка: " + ex.Message;
            }
        }
    }
}
