namespace FuzbollLadder.Models
{
    public class PlayerStat
    {
        public Player Player { get; set; }

        public double DailyRatingDelta { get; set; }

        public double WeeklyRatingDelta { get; set; }

        public double MonthlyRatingDelta { get; set; }
    }
}