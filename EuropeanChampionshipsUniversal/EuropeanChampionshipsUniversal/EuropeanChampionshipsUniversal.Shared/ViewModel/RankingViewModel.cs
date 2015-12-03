using EuropeanChampionshipsUniversal.Common;
using EuropeanChampionshipsUniversal.DAL;
using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class RankingViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IChampionshipsDataAccess dataAccess;

        private INavigationService _navigationService;

        private ObservableCollection<LeagueTableTeam> _teams = new ObservableCollection<LeagueTableTeam>();

        public ObservableCollection<LeagueTableTeam> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                RaisePropertyChanged("Teams");
            }
        }

        private ICommand goToTeamCommand;

        public ICommand GoToTeamCommand
        {
            get
            {
                if (this.goToTeamCommand == null)
                    this.goToTeamCommand = new RelayCommand<string>(GoToTeam);
                return this.goToTeamCommand;
            }
        }

        [PreferredConstructor]
        public RankingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            dataAccess = new ChampionshipsAPIAccess();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            int tag = (int)e.Parameter;
            LoadTeams(tag);
        }

        private async Task LoadTeams(int tag)
        {
            LeagueTable t = await dataAccess.GetTeamsByLeagueId(tag);

            _teams.Clear();

            foreach (var item in t.standing)
            {
                _teams.Add(item);
            }
        }

        public async void GoToTeam(string href)
        {
            TeamInfo ti = await dataAccess.GetTeamByLink(href);
            _navigationService.NavigateTo("TeamPage", ti);
        }
    }
}
