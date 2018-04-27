using CoinSong.Api.Domain.Models;

namespace CoinSong.Api.Domain.Strategies
{
    public interface IStrategy
    {
        EvaluationResult Execute();
    }
}