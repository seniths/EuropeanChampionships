using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIUsers.DTOs
{
    public class FavoriteTeamDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdTeam { get; set; }
        public int IdChampionship { get; set; }
    }
}