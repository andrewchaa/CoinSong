using CoinSong.Domain.Models;

namespace CoinSong.Domain.Strategies
{
    public interface IStrategy
    {
        EvaluationResult Execute();
    }
}