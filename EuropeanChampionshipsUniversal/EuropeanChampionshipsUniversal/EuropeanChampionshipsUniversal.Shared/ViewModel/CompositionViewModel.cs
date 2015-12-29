using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;


namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class CompositionViewModel : ViewModelBase
    {
        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        private ResourceLoader loader;

        public ResourceLoader Loader
        {
            get { return loader; }
            set { loader = value; }
        }

        private ObservableCollection<TeamPlayer> _players = new ObservableCollection<TeamPlayer>();

        public ObservableCollection<TeamPlayer> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        private INavigationService _navigationService;

        [PreferredConstructor]
        public CompositionViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            loader = new ResourceLoader();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUserTeam cut = (CurrentUserTeam)e.Parameter;
            TeamPlayers tp = cut.CurrentTeamPLayers;
            currentUser = cut.CurrentUser;

            _players.Clear();
            foreach (var player in tp.players)
            {
                _players.Add(player);
            }
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

        private ICommand goToFavCommand;

        public ICommand GoToFavCommand
        {
            get
            {
                if (this.goToFavCommand == null)
                    this.goToFavCommand = new Common.RelayCommand(() => GoToFav());
                return this.goToFavCommand;
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

        private void GoToFav()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
                _navigationService.NavigateTo("FavoritePage", currentUser);
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }
    }
}
