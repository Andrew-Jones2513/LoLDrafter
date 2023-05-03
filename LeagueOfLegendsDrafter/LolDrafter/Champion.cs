using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolDrafter.enums;

namespace LolDrafter
{
    public class Champion
    {
        // Instance Fields
        private int _championID;
        private string _champName;
        private string _abilities;
        private string _badMatchup;
        private string _goodMatchup;
        private string _build;
        private string _positions;

        // Properties    
        public int ChampionID { get { return _championID; } set { _championID = value; } }
        public string ChampName { get { return _champName; } set { _champName = value; } }
        public string Abilities { get { return _abilities; } set { _abilities = value; } }
        public string BadMatchup { get { return _badMatchup; } set { _badMatchup = value; } }
        public string GoodMatchup { get { return _goodMatchup; } set { _goodMatchup = value; } }
        public string Build { get { return _build; } set { _build = value; } }
        public string Positions { get { return _positions; } set { _positions = value; } }

        // Constructor

        public Champion() { }

        public Champion(string champName, string abilities, string good, string bad, string build, string positions)
        {
            _champName = champName;
            _abilities = abilities;
            _badMatchup = bad;
            _goodMatchup = good;
            _build = build;
            _positions = positions;
        }
    }
}
