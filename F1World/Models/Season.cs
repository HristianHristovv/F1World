namespace F1World.Models
{
    public class Season
    {
        public int SeasonId { get; set; }   
        public int Year { get; set; }   

        public int ChampionPilotId { get; set; }    
        public Pilot? ChampionPilot { get; set; }

        public int ChampionTeamId { get; set; } 
        public int ChampionTeam { get; set; }
    }
}
