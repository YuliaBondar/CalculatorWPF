using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public class BinaryOperation : IOperation
    {
        private readonly Func<double, double, double> _operation;

        public BinaryOperation(Func<double, double, double> operation)
        {
            _operation = operation;
        }

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется 2 аргумента.");

            return _operation(args[0], args[1]);
           
            
        }
    }
}
