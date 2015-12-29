using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class ChampionshipsViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private ICommand goToRankingCommand;

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

        public int IDBundesliga { get; set; }
        public int IDEreDivisie { get; set; }
        public int IDLigue1 { get; set; }
        public int IDPremierLeaue { get; set; }
        public int IDPrimeiraLiga { get; set; }
        public int IDPrimeraDivision { get; set; }
        public int IDSerieA { get; set; }

        public ICommand GoToRankingCommand
        {
            get
            {
                if (this.goToRankingCommand == null)
                    this.goToRankingCommand = new RelayCommand<int>(GoToRanking);
                return this.goToRankingCommand;
            }
        }

        [PreferredConstructor]
        public ChampionshipsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            IDBundesliga = 394;
            IDEreDivisie = 404;
            IDLigue1 = 396;
            IDPremierLeaue = 398;
            IDPrimeiraLiga = 402;
            IDPrimeraDivision = 399;
            IDSerieA = 401;
            loader = new ResourceLoader();
        }

        public void GoToRanking(int tag)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                CurrentUserTeam cut = new CurrentUserTeam();
                cut.CurrentUser = currentUser;
                cut.Tag = tag;
                _navigationService.NavigateTo("RankingPage", cut);
            }
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            currentUser = (User)e.Parameter;
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
