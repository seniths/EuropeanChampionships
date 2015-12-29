using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class User
    {
        public FavoriteTeamsUser[] favoriteteamsusers { get; set; }
        public int idUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
