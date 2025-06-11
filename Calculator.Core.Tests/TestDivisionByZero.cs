using Xunit;
using Calculator.Core;
using System;
using Calculator.Core.Operations;

namespace Calculator.Core.Tests
{
    public class TestDivisionByZero 
    {
        private readonly CalculatorService _calculatorService;

        public TestDivisionByZero()
        {
            _calculatorService = new CalculatorService(OperationRegistry.GetOperations());
        }

        [Fact] //тест деления на 0
        public void Calculate_DivisionByZero_ReturnsErrorMessage()
        {
            string input = "10 / 0";

            string expectedErrorMessagePart = "Ошибка: Attempted to divide by zero.";

            string actualResult = _calculatorService.Calculate(input);

            Assert.Contains(expectedErrorMessagePart, actualResult);
        }

       
        [Theory] //тест на проверку вводимых аргументов
        [InlineData("abc + 5")] 
        [InlineData("5 * xyz")] 
        [InlineData("sin hello")] 
        [InlineData("10.0.0 + 5")] 
        [InlineData("2 + three")] 
        public void Calculate_NonNumericArguments_ReturnsErrorMessage(string input)
        {
            string actualResult = _calculatorService.Calculate(input);
            Assert.Contains("Ошибка: недостаточно аргументов", actualResult);
            Assert.False(double.TryParse(actualResult, out _), "Результат не должен быть числом при ошибке формата.");

        }



        [Theory] //тест напустую строку
        [InlineData(" ")]
        [InlineData("     ")] 
       
        public void Calculate_Register_ReturnCorrectResult(string input)
        {
            string actualResult = _calculatorService.Calculate(input);
            Assert.Equal("Ошибка: операция не выбрана", actualResult);
            

        }

        [Theory]
        [InlineData("+ 5", "Ошибка: недостаточно аргументов")]
       
        public void Calculate_Arguments_ReturnsErrorMessage(string input, string errorMessage)
        {
            string actualResult = _calculatorService.Calculate(input);
            Assert.Contains(errorMessage, actualResult);
        }


    }
}