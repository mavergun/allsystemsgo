using Calculator;
using Calculator.Parser;


namespace Tests;

public class ParserTests
{
    /// <summary>
    /// Test generic functionality
    /// </summary>
    /// <param name="input"></param>
    [TestCase("1,2")]
    public async Task GeneralParsing(string input)
    {
        var parser = new InputParser();

        var result = await parser.ParseInput(input);
        
        Assert.That(result, Is.Not.Null);
        Assert.IsTrue(result.Operation.GetType() == typeof(Operation));
        Assert.That(result.Values, Is.Not.Null);
        Assert.That(result.Values.Count(), Is.GreaterThanOrEqualTo(2));
    }
    
    /// <summary>
    /// Tests for missing or invalid numbers
    /// </summary>
    /// <param name="input"></param>
    [TestCase("")]
    [TestCase("1,tut")]
    [TestCase("1,")]
    [TestCase(",2")]
    [TestCase("tut,2")]
    [TestCase(",")]
    [TestCase("2")]
    public async Task ParsingZero(string input)
    {
        var parser = new InputParser();

        var result = await parser.ParseInput(input);
        
        Assert.That(result, Is.Not.Null);
        Assert.IsTrue(result.Operation.GetType() == typeof(Operation));
        Assert.That(result.Values, Is.Not.Null);
        Assert.That(result.Values.Count(), Is.GreaterThanOrEqualTo(2));
    }
    
    /// <summary>
    /// Tests for invalid number of entries 
    /// </summary>
    /// <param name="input"></param>
    [TestCase("1,2,")]
    public void InvalidParameter(string input)
    {
        var parser = new InputParser();

        var ex = Assert.ThrowsAsync<ArgumentException>(async () => await parser.ParseInput(input));
        
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Is.EqualTo("A maximum of two numbers is allowed."));
    }
}