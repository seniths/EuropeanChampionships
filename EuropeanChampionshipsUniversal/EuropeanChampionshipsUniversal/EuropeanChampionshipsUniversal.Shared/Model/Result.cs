using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class Result
    {
        public int? goalsHomeTeam { get; set; }
        public int? goalsAwayTeam { get; set; }
        public Halftime halfTime { get; set; }
    }
}
