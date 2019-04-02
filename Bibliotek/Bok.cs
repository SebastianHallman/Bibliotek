using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    class Bok
    {
        private string Titel, Författare;
        private bool Lånad;

        public Bok(string T, string F, bool L)
        {
            Titel = T;
            Författare = F;
            Lånad = L;
        }

        public string TitelGetOrSet
        {
            get { return Titel; }
            set { Titel = value; }
        }

        public string FörfattareGetOrSet
        {
            get { return Författare; }
            set { Författare = value; }
        }

        public bool LånadGetOrSet
        {
            get { return Lånad; }
            set { Lånad = value; }
        }

    }
}



