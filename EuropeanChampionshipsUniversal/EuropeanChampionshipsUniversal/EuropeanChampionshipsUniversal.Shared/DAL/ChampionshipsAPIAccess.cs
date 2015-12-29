using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using EuropeanChampionshipsUniversal.Model;

namespace EuropeanChampionshipsUniversal.DAL
{
    public class ChampionshipsAPIAccess : IChampionshipsDataAccess
    {
        //classe statique?

        private HttpClient client;

        public ChampionshipsAPIAccess()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Auth-Token", "be1160826eb441c78cdd69683e05bc2b");
        }

        public async Task<int> GetChampionshipIdByTeamId(int idTeam)
        {
            HttpResponseMessage response = await client.GetAsync(@"http://api.football-data.org/v1/teams/" + idTeam);
            string json = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamInfo>(json);

            response = await client.GetAsync(team._links.fixtures.href);
            json = await response.Content.ReadAsStringAsync();
            var teamFixtures = JsonConvert.DeserializeObject<TeamFixtures>(json);

            int length = teamFixtures.fixtures.First()._links.soccerseason.href.Length;
            int sId = Int32.Parse(teamFixtures.fixtures.First()._links.soccerseason.href.Substring(length - 3));

            return sId;
        }

        public async Task<TeamInfo> GetTeamByLink(string href)
        {
            HttpResponseMessage response = await client.GetAsync(href);
            string json = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamInfo>(json);

            int length = ("http://api.football-data.org/v1/teams/").Length;
            int teamId = Int32.Parse(team._links.self.href.Substring(length));
            team.id = teamId;

            return team;
        }

        public async Task<TeamPlayers> GetTeamComposition(string href)
        {
            HttpResponseMessage response = await client.GetAsync(href);
            string json = await response.Content.ReadAsStringAsync();
            var teamPlayers = JsonConvert.DeserializeObject<TeamPlayers>(json);
            return teamPlayers;
        }

        public async Task<LeagueTable> GetTeamsByLeagueId(int leagueId)
        {
            HttpResponseMessage response = await client.GetAsync(@"http://api.football-data.org/v1/soccerseasons/" + leagueId + @"/leagueTable");
            string json = await response.Content.ReadAsStringAsync();
            var league = JsonConvert.DeserializeObject<LeagueTable>(json);
            return league;
        }

        public async Task<List<TeamInfo>> GetUserTeams(FavoriteTeamsUser[] teams)
        {
            List<TeamInfo> favTeams = new List<TeamInfo>();

            foreach (var item in teams)
            {
                HttpResponseMessage response = await client.GetAsync(@"http://api.football-data.org/v1/teams/" + item.idTeam);
                string json = await response.Content.ReadAsStringAsync();
                var lt = JsonConvert.DeserializeObject<TeamInfo>(json);
                lt.id = item.idTeam;
                favTeams.Add(lt);
            }

            return favTeams;
        }
    }
}
