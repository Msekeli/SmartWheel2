using SmartWheel.Application.Services;

namespace SmartWheel.Infrastructure.Services;

public sealed class ScoreCalculator : IScoreCalculator
{
    private static readonly List<string> _correctAnswers = new()
    {
        "echo",
        "shadow",
        "time",
        "fire",
        "water"
    };

    public int Calculate(List<string> answers)
    {
        if (answers is null || answers.Count == 0)
            return 0;

        int score = 0;

        for (int i = 0; i < Math.Min(answers.Count, _correctAnswers.Count); i++)
        {
            if (string.Equals(
                    answers[i]?.Trim(),
                    _correctAnswers[i],
                    StringComparison.OrdinalIgnoreCase))
            {
                score++;
            }
        }

        return score;
    }
}