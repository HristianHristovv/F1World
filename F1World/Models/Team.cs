namespace F1World
{
    public class Team
    {
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public string? BaseLocation { get; set; }
        public int FoundedYear { get; set; }
        public string? Principal { get; set; }
        public ICollection<Pilot>? Pilots { get; set; }
    }
}