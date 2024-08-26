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
    [TestCase("1,2,")]
    [TestCase("1,2,3")]
    [TestCase("1,tut,3")]
    [TestCase("1,tut,tut")]
    [TestCase("1,2,3,")]
    public async Task ParsingZero(string input)
    {
        var parser = new InputParser();

        var result = await parser.ParseInput(input);
        
        Assert.That(result, Is.Not.Null);
        Assert.IsTrue(result.Operation.GetType() == typeof(Operation));
        Assert.That(result.Values, Is.Not.Null);
        Assert.That(result.Values.Count(), Is.GreaterThanOrEqualTo(2));
    }

    [TestCase("1,,3")]
    [TestCase("1\n2,3")]
    [TestCase("1\n2\n")]
    [TestCase("1,2\n3")]
    [TestCase("1\n\n3")]
    public async Task ParsingWithStaticDelimiters(string input)
    {
        var parser = new InputParser();

        var result = await parser.ParseInput(input);
        
        Assert.That(result, Is.Not.Null);
        Assert.IsTrue(result.Operation.GetType() == typeof(Operation));
        Assert.That(result.Values, Is.Not.Null);
        Assert.That(result.Values.Count(), Is.EqualTo(3));
    }
    
    [TestCase("1\n-3\n3,-4")]
    public async Task ParseNegativeNumbers_ShouldThrowException(string input)
    {
        var parser = new InputParser();
        
        // validate negative numbers 
        var ex = Assert.ThrowsAsync<ArgumentException>(async () => await parser.ParseInput(input));
        
        Assert.That(ex.Message, Does.Contain("-3,-4"));
    }
}