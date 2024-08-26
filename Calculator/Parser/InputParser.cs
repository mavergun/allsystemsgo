namespace Calculator.Parser;

public class InputParser : IInputParser
{
    /// <summary>
    /// Parse string with following rules
    /// always return at least 2 element values collection
    /// missing invalid numbers converted to 0
    /// more than 2 numbers will throw ArgumentException
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<CalcParam> ParseInput(
        string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return await Task.FromResult(
                new CalcParam(
                operation: Operation.Add,
                values: new int[] { 0, 0 }));
        }

        var values = ExtractNumbers(input);
        var operation = ExtractOperation(input);
        
        //handling special case with one number and no delimiter
        if (values.Count() == 1 && 
            operation == Operation.None)
        {
            operation = Operation.Add;
            values = new[]
            {
                values.ElementAt(0),
                0
            };
        }
            
        return await Task.FromResult(
            new CalcParam(
            values: values,
            operation: operation));
    }

    private IEnumerable<int> ExtractNumbers(
        string input)
    {
        string[] parts = input.Split(',', '\r');
        
        if (parts.Length > 2)
        {
            throw new ArgumentException("A maximum of two numbers is allowed.");
        }

        return parts.Select(p=> 
                int.TryParse(p, out int number) ? number : 0);
    }

    private Operation ExtractOperation(
        string input)
    {
        return input.Contains(',') ? Operation.Add : Operation.None;
    }
}