using SmartWheel.Application.Services;

namespace SmartWheel.Infrastructure.Services;

public sealed class PrizeCalculator : IPrizeCalculator
{
    public decimal Calculate(int score)
    {
        return score switch
        {
            5 => 100m,
            4 => 50m,
            3 => 25m,
            2 => 10m,
            1 => 5m,
            _ => 0m
        };
    }
}