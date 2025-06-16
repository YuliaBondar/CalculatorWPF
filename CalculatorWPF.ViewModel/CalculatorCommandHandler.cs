using Calculator.Core;

public class CalculatorCommandHandler
{
    private readonly CalculatorService _calculatorService;

    public CalculatorCommandHandler(CalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    public string HandleButtonClick(string input, string content)
    {
        if (content == "=")
        {
            return _calculatorService.Calculate(input.Trim());
        }
        else if (content == "C")
        {
            return ""; 
        }
        else
        {
            return HandleOperations(input, content);
        }
    }

    private string HandleOperations(string input, string content)
    {
       
        string[] parts = input.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            input += $" {content} ";
        }

        return input;
    }

}