using System.Net;

namespace Model
{
    public class MetricData
    {
        public string Team { get; set; }
        public string League { get; set; }
        public decimal OverallForm { get; set; }
        public decimal HomeForm { get; set; }
        public decimal AwayForm { get; set; }
        public decimal HomeGoalsScored { get; set; }
        public decimal AwayGoalsScored { get; set; }
        public decimal HomeGoalsConceded { get; set; }
        public decimal AwayGoalsConceded { get; set; }
        public decimal AvgShotsAttemptedHome { get; set; }
        public decimal AvgShotsAttemptedAway { get; set; }
        public decimal AvgShotsOnTarget { get; set; }
        public decimal AvgCornersHome { get; set; }
        public decimal AvgCornersAway { get; set; }
        public decimal AvgCornersConcededHome { get; set; }
        public decimal AvgCornersConcededAway { get; set; }
        public decimal AvgGoalsPerGameHome { get; set; }
        public decimal AvgGoalsPerGameAway { get; set; }
    }
}
