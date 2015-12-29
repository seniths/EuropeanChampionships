using EuropeanChampionshipsUniversal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EuropeanChampionshipsUniversal.DAL
{
    public interface IUsersDataAccess
    {
        Task<List<User>> GetUsers();

        Task<bool> SaveUser(User user);
        Task<bool> AddFavoriteTeam(int idUser, int id);
        Task<bool> RemoveFavoriteTeam(int idToDelete);
        Task<User> GetUserById(int idUser);
        Task<User> GetUserByLogin(string login);
    }
}
