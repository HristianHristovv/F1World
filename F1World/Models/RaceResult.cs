namespace F1World
{
    public class RaceResult
    {
        public int RaceResultId { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
        public int Position { get; set; }
        public TimeSpan FinishTime { get; set; }
        public Race? Race { get; set; }
        public Pilot? Pilot { get; set; }
    }

}
