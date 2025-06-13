using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public class UnaryOperation : IOperation
    {
        private readonly Func<double, double> _operation;

        public UnaryOperation(Func<double, double> operation)
        {
            _operation = operation;
        }

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется 1 аргумент.");

            return _operation(args[0]);
           
           
        }
    }
}
