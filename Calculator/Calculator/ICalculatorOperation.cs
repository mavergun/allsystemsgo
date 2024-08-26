namespace Calculator;

public interface ICalculatorOperation
{
    Task<int> Operate(IEnumerable<int> values);
}