namespace F1World
{
    public class Pilot
    {
        public int PilotId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }

}
