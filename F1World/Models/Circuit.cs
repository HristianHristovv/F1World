namespace F1World.Models
{
    public class Circuit
    {
        public int CircuitId { get; set; }  
        public string? Name { get; set; }    
        public string? Country { get; set; } 
        public double RaceLength { get; set; }
        public string? LapRecord { get; set; }   
    }
}