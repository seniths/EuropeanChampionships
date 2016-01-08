using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EuropeanChampionshipsUniversal.DAL
{
    public class SingletonChampionships
    {
        private static HttpClient clientUnique;

        public static HttpClient GetInstance()
        {
            if (clientUnique == null)
            {
                clientUnique = new HttpClient();
                clientUnique.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Auth-Token", "be1160826eb441c78cdd69683e05bc2b");
            }
            return clientUnique;
        }
    }
}
