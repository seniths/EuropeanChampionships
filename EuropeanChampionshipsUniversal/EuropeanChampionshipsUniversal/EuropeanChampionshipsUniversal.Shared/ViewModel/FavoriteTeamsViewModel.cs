using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class FavoriteTeamsViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Team> teams;

        public ObservableCollection<Team> Teams
        {
            get { return teams; }
            set
            {
                teams = value;
                RaisePropertyChanged("Teams");
            }
        }

        //private ObservableCollection<LeagueTeam> teamsApi=new ObservableCollection<LeagueTeam>();

        //public ObservableCollection<LeagueTeam> TeamsApi
        //{
        //    get { return teamsApi; }
        //    set
        //    {
        //        teamsApi = value;
        //        RaisePropertyChanged("TeamsApi");
                
        //    }
        //}

        private INavigationService _navigationService;

        [PreferredConstructor]
        public FavoriteTeamsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //teams = new ObservableCollection<Team>(AllTeams.GetAllTeams());
            //CallWebApiAsync();
        }

        public void GoToTeam(Team t)
        {
            _navigationService.NavigateTo("TeamPage", t);
        }

        //public void GoToTeam(LeagueTeam t)
        //{
        //    _navigationService.NavigateTo("TeamPage", t);
        //}

   //     private async Task CallWebApiAsync()
   //     {
   //         HttpClient client = new HttpClient();
   //         HttpResponseMessage response = await client.GetAsync("http://api.football-data.org/v1/soccerseasons/394/teams");
   //         string json = await response.Content.ReadAsStringAsync();
   //         var league = JsonConvert.DeserializeObject<LeagueTeams>(json);
			//foreach(var team in league.Teams)
			//	teamsApi.Add(team);
            
   //     }
    }
}
