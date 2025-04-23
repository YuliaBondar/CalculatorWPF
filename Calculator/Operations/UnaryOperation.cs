using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Interfaces;

public class UnaryOperation : IOperation
{
    private readonly Func<double, double> _operation;//делегат

    public UnaryOperation(Func<double, double> operation)
    {
        _operation = operation;// Переданная функция сохраняется в поле
    }

    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException("Требуется 1 аргумент.");

        // Берем только первый аргумент, остальные игнорируем
        return _operation(args[0]);//вызов делегата
    }
}
