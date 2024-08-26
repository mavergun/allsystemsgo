using Microsoft.Extensions.DependencyInjection;

namespace Calculator;

/// <summary>
/// Factory class to get appropriate processor
/// </summary>
public class CalculatorOperationFactory : ICalculatorOperationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CalculatorOperationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICalculatorOperation GetOperation(Operation operation)
    {
        return operation switch
        {
            Operation.Add => _serviceProvider.GetRequiredService<AdditionOperation>(),
            _ => throw new InvalidOperationException("Invalid operator.")
        };
    }
}
