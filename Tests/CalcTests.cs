using Calculator;
using Microsoft.Extensions.DependencyInjection;


namespace Tests;

public class CalcTests
{
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddTransient<AdditionOperation>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// Collection of entries for generic add operation
    /// </summary>
    private static IEnumerable<TestCaseData> AddTestCases
    {
        get
        {
            yield return new TestCaseData(new CalcParam(new []{1,2}, Operation.Add));
            yield return new TestCaseData(new CalcParam(new []{1,-3}, Operation.Add));
            yield return new TestCaseData(new CalcParam(new []{1,2,3}, Operation.Add));
            yield return new TestCaseData(new CalcParam(new []{1,2,3,}, Operation.Add));
        }
    }
    
    /// <summary>
    /// Tests for generic add operation
    /// </summary>
    /// <param name="input"></param>
    [TestCaseSource(nameof(AddTestCases))]
    public async Task AddOperationGeneral(CalcParam input)
    {
        var factory = new CalculatorOperationFactory(_serviceProvider);
        
        Assert.That(factory, Is.Not.Null);
        
        var addOperation = factory.GetOperation(Operation.Add);

        var result = await addOperation.Operate(input.Values);

        var expectedResult = input.Values.Sum(v => v);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}