namespace SmartWheel.Application.Services;

public interface IPrizeCalculator
{
    decimal Calculate(int score);
}