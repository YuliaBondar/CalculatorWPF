using Calculator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string op = parts.FirstOrDefault(p => operations.ContainsKey(p));
                if (op == null)
                {
                    return "Ошибка: операция не выбрана";
                }

                if (!operations.TryGetValue(op, out var operation))
                {
                    return "Ошибка: неизвестная операция";
                }

                double[] numbers = parts.Where(p => double.TryParse(p, out _)).Select(double.Parse).ToArray();

                if (operation is Operations.UnaryOperation && numbers.Length >= 1)
                {
                    return operation.Call(numbers[0]).ToString();
                }
                else if (operation is Operations.BinaryOperation && numbers.Length >= 2)
                {
                    return operation.Call(numbers[0], numbers[1]).ToString();
                }
                else
                {
                    return "Ошибка: недостаточно аргументов";
                }
            }
            catch (Exception ex)
            {
                return "Ошибка: " + ex.Message;
            }
        }
    }

}
