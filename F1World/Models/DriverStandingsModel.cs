using Newtonsoft.Json;
using System.Collections.Generic;

namespace F1World.Models
{
    public class DriverStandingsModel
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("Driver")]
        public DriverModel Driver { get; set; }

        [JsonProperty("Constructors")]
        public List<TeamModel> Constructors { get; set; }
    }

    public class DriverModel
    {
        [JsonProperty("givenName")]
        public string FirstName { get; set; }

        [JsonProperty("familyName")]
        public string LastName { get; set; }
    }

    public class TeamModel
    {
        [JsonProperty("name")]
        public string TeamName { get; set; }
    }
}
