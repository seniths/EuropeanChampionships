using EuropeanChampionshipsUniversal.DAL;
using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class TeamViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IChampionshipsDataAccess daTeams;
        private IUsersDataAccess daUsers;

        private TeamInfo _selectedTeam = new TeamInfo();

        public TeamInfo SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                RaisePropertyChanged("SelectedTeam");
            }
        }

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

        private string imageVisibility;

        public string ImageVisibility
        {
            get { return imageVisibility; }
            set { imageVisibility = value; }
        }

        private string webViewVisibility;

        public string WebViewVisibility
        {
            get { return webViewVisibility; }
            set { webViewVisibility = value; }
        }

        private BitmapImage iconFav;

        public BitmapImage IconFav
        {
            get { return iconFav; }
            set
            {
                iconFav = value;
                RaisePropertyChanged("IconFav");
            }
        }

        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; }
        }


        private INavigationService _navigationService;

        [PreferredConstructor]
        public TeamViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            daTeams = new ChampionshipsAPIAccess();
            daUsers = new UsersAPIAccess();
            isFavorite = false;
            loader = new ResourceLoader();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUserTeam cut = (CurrentUserTeam)e.Parameter;
            _selectedTeam = cut.CurrentTeam;
            currentUser = cut.CurrentUser;
            iconFav = new BitmapImage(new Uri("ms-appx:///Assets/Star-Empty.png"));
            isFavorite = false;
            LoadUser(cut.CurrentUser.idUser);
            foreach (var item in currentUser.favoriteteamsusers)
            {
                if (item.idTeam == _selectedTeam.id)
                {
                    iconFav = new BitmapImage(new Uri("ms-appx:///Assets/Star-Full.png"));
                    isFavorite = true;
                }
            }

            if (_selectedTeam.crestUrl.EndsWith("svg"))
            {
                webViewVisibility = "Visible";
                imageVisibility = "Collapsed";
            }
            else
            {
                webViewVisibility = "Collapsed";
                imageVisibility = "Visible";
            }
        }

        private ICommand _goToCompositionCommand;

        public ICommand GoToCompositionCommand
        {
            get
            {
                if (this._goToCompositionCommand == null)
                    this._goToCompositionCommand = new RelayCommand(() => GoToComposition());
                return this._goToCompositionCommand;
            }
        }

        private ICommand goToRankingsCommand;

        public ICommand GoToRankingsCommand
        {
            get
            {
                if (this.goToRankingsCommand == null)
                    this.goToRankingsCommand = new RelayCommand(() => GoToRankings());
                return this.goToRankingsCommand;
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

        private async void GoToRankings()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                CurrentUserTeam cut = new CurrentUserTeam();
                cut.CurrentUser = currentUser;
                cut.Tag = await daTeams.GetChampionshipIdByTeamId(_selectedTeam.id);
                _navigationService.NavigateTo("RankingPage", cut);
            }
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
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

        private async void GoToComposition()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                TeamPlayers tp = await daTeams.GetTeamComposition(_selectedTeam._links.players.href);
                CurrentUserTeam cut = new CurrentUserTeam();
                cut.CurrentUser = currentUser;
                cut.CurrentTeamPLayers = tp;
                _navigationService.NavigateTo("CompositionPage", cut);
            }
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }

        private ICommand _addRemoveFavoriteCommand;

        public ICommand AddRemoveFavoriteCommand
        {
            get
            {
                if (this._addRemoveFavoriteCommand == null)
                    this._addRemoveFavoriteCommand = new RelayCommand(() => AddRemoveFavorite());
                return this._addRemoveFavoriteCommand;
            }
        }

        private async void AddRemoveFavorite()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (isFavorite)
                {
                    int idToDelete = 0;

                    foreach (var item in currentUser.favoriteteamsusers)
                    {
                        if (item.idTeam == _selectedTeam.id)
                            idToDelete = item.idFavoriteTeamsUser;
                    }
                    if (await daUsers.RemoveFavoriteTeam(idToDelete))
                    {
                        iconFav.UriSource = new Uri("ms-appx:///Assets/Star-Empty.png");
                        isFavorite = false;
                    }
                }
                else
                {
                    if (await daUsers.AddFavoriteTeam(currentUser.idUser, _selectedTeam.id))
                    {
                        iconFav.UriSource = new Uri("ms-appx:///Assets/Star-Full.png");
                        isFavorite = true;
                    }
                }

                currentUser = await daUsers.GetUserById(currentUser.idUser);
            }
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }

        private async void LoadUser(int id)
        {
            currentUser = await daUsers.GetUserById(currentUser.idUser);

            foreach (var item in currentUser.favoriteteamsusers)
            {
                if (item.idTeam == _selectedTeam.id)
                {
                    iconFav.UriSource = new Uri("ms-appx:///Assets/Star-Full.png");
                    isFavorite = true;
                }
            }
        }
    }
}
