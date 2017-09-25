using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public interface IRatingService
    {
        double DefaultRating { get; }

        RatingDelta CalculateDelta(double winnerRating, double loserRating, double factor = 1);
    }
}