using EuropeanChampionshipsUniversal.DAL;
using EuropeanChampionshipsUniversal.Encryption;
using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IUsersDataAccess da;

        private string login;

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
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
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            da = new UsersAPIAccess();
            loader = new ResourceLoader();

            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (settings.Values["login"] != null)
                login = settings.Values["login"].ToString();
            if (settings.Values["password"] != null)
                password = settings.Values["password"].ToString();
        }

        private ICommand connexionCommand;

        public ICommand ConnexionCommand
        {
            get
            {
                if (this.connexionCommand == null)
                    this.connexionCommand = new RelayCommand(() => Connexion());
                return this.connexionCommand;
            }
        }

        private void Connexion()
        {   
            if(NetworkInterface.GetIsNetworkAvailable())
                IsValidUser();
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();

            //loading
        }

        private async void IsValidUser()
        {
            User user = await da.GetUserByLogin(login);

            if (user != null && user.password.Equals(PasswordEncryption.cryptPwd(password)))
                GoToHome(user);
            else
                new MessageDialog(loader.GetString("ConnectionError")).ShowAsync();
        }

        private void GoToHome(User item)
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            settings.Values["login"] = login;
            settings.Values["password"] = password;
            _navigationService.NavigateTo("HomePage", item);
        }

        private ICommand goToSubscribeCommand;

        public ICommand GoToSubscribeCommand
        {
            get
            {
                if (this.goToSubscribeCommand == null)
                    this.goToSubscribeCommand = new RelayCommand(() => GoToSubscribe());
                return this.goToSubscribeCommand;
            }
        }

        private void GoToSubscribe()
        {
            _navigationService.NavigateTo("SubscriptionPage");
        }
    }
}
