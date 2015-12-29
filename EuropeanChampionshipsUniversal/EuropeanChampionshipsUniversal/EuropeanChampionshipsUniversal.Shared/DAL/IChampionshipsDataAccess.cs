using EuropeanChampionshipsUniversal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropeanChampionshipsUniversal.DAL
{
    public interface IChampionshipsDataAccess
    {
        Task<LeagueTable> GetTeamsByLeagueId(int leagueId);

        Task<TeamInfo> GetTeamByLink(string href);

        Task<TeamPlayers> GetTeamComposition(string href);

        Task<List<TeamInfo>> GetUserTeams(FavoriteTeamsUser[] teams);
        Task<int> GetChampionshipIdByTeamId(int idTeam);
    }
}
