using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        [PreferredConstructor]
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private ICommand _goToFavoriteCommand;

        public ICommand GoToFavoriteCommand
        {
            get
            {
                if (this._goToFavoriteCommand == null)
                    this._goToFavoriteCommand = new RelayCommand(() => GoToFavorite());
                return this._goToFavoriteCommand;
            }
        }

        private ICommand _goToChampionshipsCommand;

        public ICommand GoToChampionshipsCommand
        {
            get
            {
                if (this._goToChampionshipsCommand == null)
                    this._goToChampionshipsCommand = new RelayCommand(() => GoToChampionships());
                return this._goToChampionshipsCommand;
            }
        }

        private void GoToFavorite()
        {
            _navigationService.NavigateTo("FavoritePage");
        }

        private void GoToChampionships()
        {
            _navigationService.NavigateTo("ChampionshipsPage");
        }
    }
}
