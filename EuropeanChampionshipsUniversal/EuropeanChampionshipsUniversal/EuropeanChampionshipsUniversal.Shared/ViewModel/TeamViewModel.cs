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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class TeamViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IChampionshipsDataAccess dataAccess;

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

        private INavigationService _navigationService;

        [PreferredConstructor]
        public TeamViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            dataAccess = new ChampionshipsAPIAccess();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _selectedTeam = (TeamInfo)e.Parameter;
            if (_selectedTeam.crestUrl.EndsWith("svg"))
            {
                webViewVisibility = "Visible";
                imageVisibility= "Collapsed";
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

        private async void GoToComposition()
        {
             TeamPlayers tp = await dataAccess.GetTeamComposition(_selectedTeam._links.players.href);
            _navigationService.NavigateTo("CompositionPage", tp);
        }
    }
}
