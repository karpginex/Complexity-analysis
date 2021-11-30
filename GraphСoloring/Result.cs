using System;

namespace GraphСoloring
{
    public class Result
    {
        public Result(TimeSpan time, int[] set, int countColors)
        {
            Time = time;
            Set = set;
            CountColors = countColors;
        }

        public TimeSpan Time { get; set; }
        public int[] Set { get; set; }
        public int CountColors { get; set; }
    }
}
