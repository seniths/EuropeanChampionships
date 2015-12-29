using EuropeanChampionshipsUniversal.Common;
using EuropeanChampionshipsUniversal.DAL;
using EuropeanChampionshipsUniversal.Encryption;
using EuropeanChampionshipsUniversal.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace EuropeanChampionshipsUniversal.ViewModel
{
    public class SubscriptionViewModel : ViewModelBase
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

        private string passwordRepeated;

        public string PasswordRepeated
        {
            get { return passwordRepeated; }
            set { passwordRepeated = value; }
        }

        private ResourceLoader loader;

        public ResourceLoader Loader
        {
            get { return loader; }
            set { loader = value; }
        }


        private INavigationService _navigationService;

        [PreferredConstructor]
        public SubscriptionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            da = new UsersAPIAccess();
            loader = new ResourceLoader();
        }

        private ICommand subscriptionCommand;

        public ICommand SubscriptionCommand
        {
            get
            {
                if (this.subscriptionCommand == null)
                    this.subscriptionCommand = new RelayCommand(() => Subscribe());
                return this.subscriptionCommand;
            }
        }

        private void Subscribe()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (password != null && login != null)
                {
                    if (password.Length < 5)
                        new MessageDialog(loader.GetString("PasswordLength")).ShowAsync();
                    else
                    {
                        if (password == passwordRepeated)
                            AvailableLogin();
                        else
                            new MessageDialog(loader.GetString("PasswordEquality")).ShowAsync();
                    }
                }
                else
                    new MessageDialog(loader.GetString("Empty")).ShowAsync();
            }
            else
                new MessageDialog(loader.GetString("NoConnection")).ShowAsync();
        }

        private async void AvailableLogin()
        {
            if (login.Length < 5)
                new MessageDialog(loader.GetString("LoginLength")).ShowAsync();
            else
            {
                User user = await da.GetUserByLogin(login);
                if (user != null)
                {
                    new MessageDialog(loader.GetString("LoginAvailable")).ShowAsync();
                    return;
                }

                SaveUser();
            }
        }

        private async void SaveUser()
        {
            User user = new User() { login = login, password = password };
            user.password = PasswordEncryption.cryptPwd(user.password);
            if (await da.SaveUser(user))
                _navigationService.GoBack();
            else
                new MessageDialog(loader.GetString("RegistrationError")).ShowAsync();
        }
    }
}
