using System;

namespace FuzbollLadder.Services
{
    public class EloRatingService : IRatingService
    {
        public double DefaultRating => 1200;

        public double CalculateDelta(double winnerRating, double loserRating, double factor = 1)
        {
            // Normalize ratings
            var winnerRatingNorm = Math.Pow(10, winnerRating / 400);
            var loserRatingNorm = Math.Pow(10, loserRating / 400);

            // Calculate expected ratings
            var winnerRatingExpected = winnerRatingNorm / (winnerRatingNorm + loserRatingNorm);

            // Calculate final rating
            var winnerRatingFinal = winnerRating + 75 * factor * (1 - winnerRatingExpected);

            return winnerRatingFinal - winnerRating;
        }
    }
}