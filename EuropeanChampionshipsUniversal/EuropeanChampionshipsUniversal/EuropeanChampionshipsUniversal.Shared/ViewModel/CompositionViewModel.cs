using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Navigation;


namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class CompositionViewModel : ViewModelBase
    {
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
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            TeamPlayers tp = (TeamPlayers)e.Parameter;

            _players.Clear();
            foreach (var player in tp.players)
            {
                _players.Add(player);
            }
        }
    }
}
