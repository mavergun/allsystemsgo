namespace Calculator.Parser;

public class InputParser : IInputParser
{
    public const int INVALID_NUMBER = 0;
    private const string DELIMITER_IDENTIFIER = "//";
    
    private List<char> _delimeters = new List<char>
    {
        ',',
        '\n'
    };
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
                values: new int[] { INVALID_NUMBER, INVALID_NUMBER }));
        }

        //replace char from command line with the actual \n char
        //there is nothing to replace if the actual parameter was constructed in the code 
        input = input.Replace("\\n", "\n");  
        
        input = ExtractCustomDelimiter(input);
        
        var values = ExtractNumbers(input);

        ValidateNumbers(values);
        
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
        string[] parts = input.Split(_delimeters.ToArray());
        
        return parts.Select(p=> 
                int.TryParse(p, out int number) ? 
                    number <= 1000 ? number : INVALID_NUMBER
                    : INVALID_NUMBER);
    }

    private Operation ExtractOperation(
        string input)
    {
        return _delimeters.Any(input.Contains) ? Operation.Add : Operation.None;
    }

    private void ValidateNumbers(
        IEnumerable<int> numbers)
    {
        //validate against negative numbers 
        var negativeNumbers = numbers.Where(n => n < 0);

        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Following numbers are not allowed:{string.Join(",", negativeNumbers)}");
        }
    }

    /// <summary>
    /// Detect custom delimiter
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Input string without definition of custom delimiter</returns>
    private string ExtractCustomDelimiter(
        string input)
    {
        // Check for custom delimiter format
        if (input.StartsWith(DELIMITER_IDENTIFIER))
        {
            // Extract custom delimiter and the remaining string
            int delimiterEndIndex = input.IndexOf('\n');
            
            string delimiter = input.Substring(
                DELIMITER_IDENTIFIER.Length, 
                delimiterEndIndex - DELIMITER_IDENTIFIER.Length);

            if (string.IsNullOrWhiteSpace(delimiter))
            {
                throw new ArgumentException("Custom delimiter cannot be null or empty");
            }
            
            //requirement is stated single char delimiter, taking the first char 
            char addDelimiter = delimiter[0];
            
            _delimeters.Add(addDelimiter);

            //compensate for delimiter identifier ending character
            return input.Substring(delimiterEndIndex + 1);
        }

        return input;
    }
}