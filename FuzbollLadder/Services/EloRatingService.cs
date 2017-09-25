using System;
using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public class EloRatingService : IRatingService
    {
        public double DefaultRating => 1200;

        public RatingDelta CalculateDelta(double winnerRating, double loserRating, double factor = 1)
        {
            // Normalize ratings
            var winnerRatingNorm = Math.Pow(10, winnerRating / 400);
            var loserRatingNorm = Math.Pow(10, loserRating / 400);

            // Calculate expected ratings
            var winnerRatingExpected = winnerRatingNorm / (winnerRatingNorm + loserRatingNorm);
            var loserRatingExpected = loserRatingNorm / (winnerRatingNorm + loserRatingNorm);

            // Calculate final rating
            var winnerRatingFinal = winnerRating + 75 * factor * (1 - winnerRatingExpected);
            var loserRatingFinal = loserRating - 75 * factor * loserRatingExpected;

            // Compose delta
            var delta = new RatingDelta
            {
                WinnerDelta = winnerRatingFinal - winnerRating,
                LoserDelta = loserRatingFinal - loserRating
            };

            return delta;
        }
    }
}