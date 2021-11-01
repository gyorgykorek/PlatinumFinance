using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumFinance
{
    class Income
    {
        String type;
        int osszeg;
        String szallito, megjegyzes, gyakorisag;

        public Income(String type, int osszeg, string szallito, string megjegyzes, string gyakorisag)
        {
            this.Type = type;
            this.Osszeg = osszeg;
            this.szallito = szallito;
            this.megjegyzes = megjegyzes;
            this.megjegyzes = gyakorisag;
        }

        public string Szallito { get => szallito; set => szallito = value; }
        public string Megjegyzes { get => megjegyzes; set => megjegyzes = value; }
        public string Gyakorisag { get => gyakorisag; set => gyakorisag = value; }
        public string Type { get => type; set => type = value; }
        public int Osszeg { get => osszeg; set => osszeg = value; }
    }
}
