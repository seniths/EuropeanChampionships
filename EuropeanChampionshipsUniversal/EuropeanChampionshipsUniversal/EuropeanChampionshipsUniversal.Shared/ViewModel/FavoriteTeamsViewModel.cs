using EuropeanChampionshipsUniversal.DAL;
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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class FavoriteTeamsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IChampionshipsDataAccess daTeams;
        private IUsersDataAccess daUsers;

        private ObservableCollection<TeamInfo> teams = new ObservableCollection<TeamInfo>();

        public ObservableCollection<TeamInfo> Teams
        {
            get { return teams; }
            set
            {
                teams = value;
                RaisePropertyChanged("Teams");
            }
        }

        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        private ResourceLoader loader;

        public ResourceLoader Loader
        {
            get { return loader; }
            set { loader = value; }
        }

        private INavigationService _navigationService;

        [PreferredConstructor]
        public FavoriteTeamsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            daTeams = new ChampionshipsAPIAccess();
            daUsers = new UsersAPIAccess();
            loader = new ResourceLoader();
        }

        private async void LoadUser()
        {
            currentUser = await daUsers.GetUserById(currentUser.idUser);
        }

        public void OnNavigatedTo(User parameter)
        {
            currentUser = parameter;
            LoadFavoriteTeams();
        }

        private async void LoadFavoriteTeams()
        {
            currentUser = await daUsers.GetUserById(currentUser.idUser);
            List<TeamInfo> userTeams = await daTeams.GetUserTeams(currentUser.favoriteteamsusers);


            teams.Clear();
            foreach (var item in userTeams)
            {
                teams.Add(item);
            }
        }

        public void GoToTeam(TeamInfo t)
        {
            CurrentUserTeam cut = new CurrentUserTeam() { CurrentUser = currentUser, CurrentTeam = t };
            _navigationService.NavigateTo("TeamPage", cut);
        }

        private ICommand goToHomeCommand;

        public ICommand GoToHomeCommand
        {
            get
            {
                if (this.goToHomeCommand == null)
                    this.goToHomeCommand = new Common.RelayCommand(() => GoToHome());
                return this.goToHomeCommand;
            }
        }

        private ICommand goToChampionshipsCommand;

        public ICommand GoToChampionshipsCommand
        {
            get
            {
                if (this.goToChampionshipsCommand == null)
                    this.goToChampionshipsCommand = new Common.RelayCommand(() => GoToChampionships());
                return this.goToChampionshipsCommand;
            }
        }

        private void GoToChampionships()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
                _navigationService.NavigateTo("ChampionshipsPage", currentUser);
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }

        public void GoToHome()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
                _navigationService.NavigateTo("HomePage", currentUser);
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }
    }
}
