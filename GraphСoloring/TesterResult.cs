using System;

namespace GraphСoloring
{
    public class TesterResult
    {
        public TimeSpan TotalGreedyTime { get; set; }
        public TimeSpan TotalAccurateTime { get; set; }

        public int MatchedSolutions { get; set; }
        public double TotalRelativeDeviation { get; set; }

        public int CountVertix { get; set; }
        public int CountTests { get; set; }
    }
}
