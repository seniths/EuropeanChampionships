using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LeagueTeam
    {
        public IDictionary<string, Link> Links { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string SquadMarketValue { get; set; }
        public string CrestUrl { get; set; }
    }
}
