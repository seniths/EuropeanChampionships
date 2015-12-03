using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LeagueTeams
    {
        public List<IDictionary<string, string>> Links { get; set; }
        public string Count { get; set; }
        public List<LeagueTeam> Teams { get; set; }
    }
}
