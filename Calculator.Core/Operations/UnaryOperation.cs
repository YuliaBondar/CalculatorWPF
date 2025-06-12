using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public class UnaryOperation : IOperation
    {
        private readonly Func<double, double> _operation;

        public UnaryOperation(Func<double, double> operation)
        {
            _operation = operation;// Переданная функция сохраняется в поле
        }

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется 1 аргумент.");

            double result = _operation(args[0]);
            if (double.IsNaN(result))
            {
                throw new ArgumentException("Результат операции является неопределенным (NaN).");
            }
            return result;
        }
    }
}
