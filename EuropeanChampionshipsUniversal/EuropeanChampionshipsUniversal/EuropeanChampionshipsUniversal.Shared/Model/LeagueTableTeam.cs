using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropeanChampionshipsUniversal.Model
{
    public class LeagueTableTeam
    {
        public LinksLeagueTableTeam _links { get; set; }
        public int position { get; set; }
        public string teamName { get; set; }
        public int playedGames { get; set; }
        public int points { get; set; }
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
        public Results home { get; set; }
        public Results away { get; set; }
    }
}
