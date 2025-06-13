using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public static class OperationRegistry
    {
        public static Dictionary<string, IOperation> GetOperations()
        {
            return new Dictionary<string, IOperation>(StringComparer.OrdinalIgnoreCase)
            {
                // Binary operations
                { "+", new BinaryOperation((a, b) => a + b) },
                { "-", new BinaryOperation((a, b) => a - b) },
                { "*", new BinaryOperation((a, b) => a * b) },
                { "/", new BinaryOperation((a, b) => {

                    double result = a / b;

                    if (b == 0)
                        {
                            return a is double ? double.NaN : throw new ArgumentException("Ошибка: Попытка деления на ноль.");
                        }
                        return a / b;
                }) },
                { "pow", new BinaryOperation((a, b) => {
                    if (a < 0 && b % 1 != 0)
                    {
                        throw new ArgumentException("Не определено");
                    }
                    return Math.Pow(a, b);
                }) },
                { "atan2", new BinaryOperation((y, x) => {
                    double result = Math.Atan2(y, x);

                    if (double.IsNaN(result))
                    {
                        throw new ArgumentException("Результат операции неопределен (NaN).");
                    }
                    return result;
                }) },

                // Unary operations
                { "sin", new UnaryOperation(Math.Sin) },
                { "cos", new UnaryOperation(Math.Cos) },
                { "sqrt", new UnaryOperation(a => a < 0 ? throw new ArgumentOutOfRangeException(nameof(a), "NAN") : Math.Sqrt(a)) },
                { "log", new UnaryOperation(a => a <= 0 ? throw new ArgumentOutOfRangeException(nameof(a), "NAN") : Math.Log(a)) },
                { "abs", new UnaryOperation(Math.Abs) },
                { "exp", new UnaryOperation(Math.Exp) }
            };
        }
    }
}