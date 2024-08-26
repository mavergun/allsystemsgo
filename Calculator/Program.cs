using Calculator;
using Calculator.Parser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register services
        services.AddTransient<AdditionOperation>();
        
        // Register the factory
        services.AddTransient<ICalculatorOperationFactory, CalculatorOperationFactory>();

        // Register the parser
        services.AddTransient<IInputParser, InputParser>();
        
        services.AddTransient<CalculatorService>();
    })
    .Build();

// Resolve the Calculator service and run the application
var calculator = host.Services.GetRequiredService<CalculatorService>();
await calculator.Run();

public class CalculatorService
{
    private readonly ICalculatorOperationFactory _operationFactory;
    private readonly IInputParser _inputParser;

    public CalculatorService(ICalculatorOperationFactory operationFactory, IInputParser inputParser)
    {
        _operationFactory = operationFactory;
        _inputParser = inputParser;
    }

    public async Task Run()
    {
        Console.WriteLine("Enter numbers for calculation:");
        string input = Console.ReadLine(); 
        
        try
        {
            //if the requirement is to throw unhandled exception the next line needs to moved outside try\catch block 
            var calcParam = await _inputParser.ParseInput(input);
            
            //get processor
            var calculatorOperation =  _operationFactory.GetOperation(calcParam.Operation);
            
            int result = await calculatorOperation.Operate(calcParam.Values);
            
            //a bit more correct would be to call something like GetOperationSignature to replace "+" sign  
            Console.WriteLine($"Result: {string.Join("+", calcParam.Values)} = {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
