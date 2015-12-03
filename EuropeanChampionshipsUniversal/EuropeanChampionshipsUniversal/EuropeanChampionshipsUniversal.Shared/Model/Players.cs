using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class TeamPlayers
    {
        public LinksTeamPlayers _links { get; set; }
        public int count { get; set; }
        public TeamPlayer[] players { get; set; }
    }
}
