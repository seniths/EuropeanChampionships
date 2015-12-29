using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class FavoriteTeamsUser
    {
        public int idFavoriteTeamsUser { get; set; }
        public int idUser { get; set; }
        public int idTeam { get; set; }
        public int idChampionship { get; set; }
    }
}
