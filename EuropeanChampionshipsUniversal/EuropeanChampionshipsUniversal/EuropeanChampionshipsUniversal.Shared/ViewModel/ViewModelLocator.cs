using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public FavoriteTeamsViewModel FavoriteTeams
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FavoriteTeamsViewModel>();
            }
        }

        public TeamViewModel Team
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeamViewModel>();
            }
        }

        public CompositionViewModel Composition
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompositionViewModel>();
            }
        }

        public ChampionshipsViewModel Championships
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChampionshipsViewModel>();
            }
        }

        public RankingViewModel Ranking
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RankingViewModel>();
            }
        }

        public SubscriptionViewModel Subscription
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubscriptionViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FavoriteTeamsViewModel>();
            SimpleIoc.Default.Register<TeamViewModel>();
            SimpleIoc.Default.Register<CompositionViewModel>();
            SimpleIoc.Default.Register<ChampionshipsViewModel>();
            SimpleIoc.Default.Register<RankingViewModel>();
            SimpleIoc.Default.Register<SubscriptionViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();

            NavigationService navigationPages = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);

            navigationPages.Configure("MainPage", typeof(MainPage));
            navigationPages.Configure("FavoritePage", typeof(FavoriteTeamsPage));
            navigationPages.Configure("TeamPage", typeof(TeamPage));
            navigationPages.Configure("CompositionPage", typeof(CompositionPage));
            navigationPages.Configure("ChampionshipsPage", typeof(ChampionshipsPage));
            navigationPages.Configure("RankingPage", typeof(RankingPage));
            navigationPages.Configure("SubscriptionPage", typeof(SubscriptionPage));
            navigationPages.Configure("HomePage", typeof(HomePage));
        }
    }
}
