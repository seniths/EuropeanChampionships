using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeanChampionshipsUniversal.Model
{
    public class Player
    {
        public int Numero { get; set; }
        public char Position { get; set; }
        public String FullName { get; set; }
        public int Age { get; set; }

        public Player(int numero, char position, String fullName, int age)
        {
            Numero = numero;
            Position = position;
            FullName = fullName;
            Age = age;
        }
    }
}
