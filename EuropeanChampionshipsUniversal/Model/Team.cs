using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Team
    {
        public String Name { get; set; }
        public Uri Logo { get; set; }
        public List<Player> Composition { get; set; }

        public Team(String name, Uri logo, List<Player> composition)
        {
            Name = name;
            Logo = logo;
            Composition = composition;
        }
    }
}
