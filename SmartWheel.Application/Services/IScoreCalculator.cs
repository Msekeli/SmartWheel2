namespace SmartWheel.Application.Services;

public interface IScoreCalculator
{
    int Calculate(List<string> answers);
}