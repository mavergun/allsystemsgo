namespace Calculator.Parser;

public interface IInputParser
{
        public Task<CalcParam> ParseInput(string input);
}