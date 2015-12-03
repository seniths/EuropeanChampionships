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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Auth-Token", "647fd5e8aecb4f799f10bec452c5c534");
        }

        public async Task<TeamInfo> GetTeamByLink(string href)
        {
            HttpResponseMessage response = await client.GetAsync(href);
            string json = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamInfo>(json);
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
    }
}
