namespace Calculator;

public enum Operation
{
    None,
    Add
}

/// <summary>
/// Domain Model object to pass data between Parser and Calculator services 
/// </summary>
public class CalcParam
{
    public static readonly CalcParam Empty = new ();
    
    public IEnumerable<int> Values { get; }
    public Operation Operation { get; }    


    public CalcParam(
        IEnumerable<int> values,
        Operation operation)
    {
        Values = values;
        Operation = operation;
    }
    private CalcParam()
        : this(
            values: Enumerable.Empty<int>(),
            operation: Operation.None) { }
}