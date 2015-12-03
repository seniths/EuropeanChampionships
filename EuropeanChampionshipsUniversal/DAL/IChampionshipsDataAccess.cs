using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IChampionshipsDataAccess
    {
        Task<LeagueTable> GetTeamsByLeagueId(int leagueId);

        Task<TeamInfo> GetTeamByLink(string href);
        
    }
}
