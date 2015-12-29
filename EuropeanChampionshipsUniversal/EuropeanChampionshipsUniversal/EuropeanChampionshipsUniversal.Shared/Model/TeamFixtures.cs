using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class TeamFixtures
    {
        public LinksTeamFixtures _links { get; set; }
        public int count { get; set; }
        public Fixture[] fixtures { get; set; }
    }
}
