using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EuropeanChampionshipsUniversal.DAL
{
    public class SingletonUsers
    {
        private static HttpClient clientUnique;

        public static HttpClient GetInstance()
        {
            if (clientUnique == null)
            {
                clientUnique = new HttpClient();
                clientUnique.BaseAddress = new Uri("http://europeanchampionships.azurewebsites.net/");
                clientUnique.DefaultRequestHeaders.Accept.Clear();
                clientUnique.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return clientUnique;
        }
    }
}
