using Calculator.Interfaces;
using System;
using System.Collections.Generic;

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
            { "/", new BinaryOperation((a, b) => b == 0 ? throw new DivideByZeroException() : a / b) },
            { "pow", new BinaryOperation(Math.Pow) },
            { "atan2", new BinaryOperation(Math.Atan2) },

            // Unary operations
            { "sin", new UnaryOperation(Math.Sin) },
            { "cos", new UnaryOperation(Math.Cos) },
            { "sqrt", new UnaryOperation(Math.Sqrt) },
            { "log", new UnaryOperation(Math.Log) },
            { "abs", new UnaryOperation(Math.Abs) },
            { "exp", new UnaryOperation(Math.Exp) }
        };
    }
}