using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public interface IRatingService
    {
        RatingDelta CalculateDelta(double winnerRating, double loserRating, double factor = 1);
    }
}