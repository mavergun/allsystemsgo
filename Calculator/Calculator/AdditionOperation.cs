namespace Calculator;

/// <summary>
/// Implementation of the Addition processor
/// </summary>
public class AdditionOperation : ICalculatorOperation
{
    public async Task<int> Operate(IEnumerable<int> values)
    {
        return await Task.FromResult( 
            values.Sum(num=>num));
    }
}