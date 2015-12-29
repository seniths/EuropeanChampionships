using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class CurrentUserTeam
    {
        public User CurrentUser { get; set; }
        public TeamInfo CurrentTeam { get; set; }
        public TeamPlayers CurrentTeamPLayers { get; set; }
        public int Tag { get; set; }
    }
}
