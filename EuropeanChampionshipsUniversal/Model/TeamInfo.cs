using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TeamInfo : INotifyPropertyChanged
    {
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public LinksTeamInfo _links { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(Name);
            }
        }
        public string code { get; set; }
        public string shortName { get; set; }
        public string squadMarketValue { get; set; }
        public string crestUrl { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
