namespace FuzbollLadder.Services
{
    public interface IRatingService
    {
        double DefaultRating { get; }

        double CalculateDelta(double winnerRating, double loserRating, double factor = 1);
    }
}