namespace Calculator;

public interface ICalculatorOperationFactory
{
    ICalculatorOperation GetOperation(Operation operation);
}