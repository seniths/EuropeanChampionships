using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AllTeams
    {
        public static IEnumerable<Team> Teams { get; set; }

        public static IEnumerable<Team> GetAllTeams()
        {
            List<Player> comp = new List<Player>
            {
                new Player(1,'G', "De Gea",23),
                new Player(12,'D', "Romero",28),
                new Player(24,'A', "Jones",25)
            };

            return Teams = new List<Team>
            {
                new Team("Manchester", new Uri("ms-appx:///Assets/logo2.png"), comp),
                new Team("Liverpool", new Uri("ms-appx:///Assets/logo2.png"), comp),
                new Team("Chelsea", new Uri("ms-appx:///Assets/logo2.png"), comp)
            };
        }
    }
}
