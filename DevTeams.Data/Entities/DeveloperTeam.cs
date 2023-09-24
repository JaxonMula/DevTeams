using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeams.Data.Entities
{
    public class DeveloperTeam
    {
        //Primary Key
        public int TeamId { get; set; }
        public string TeamName { get; set; }= string.Empty;
        //*One devloper team can have many developers

        public List<Developer> DevsOnTeam { get; set; } = new List<Developer>();
    }
}