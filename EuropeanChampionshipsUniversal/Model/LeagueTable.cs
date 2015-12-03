using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LeagueTable
    {
        public LinksLeagueTable _links { get; set; }
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public LeagueTableTeam[] standing { get; set; }
    }
}
