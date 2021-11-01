using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumFinance
{
    class Balance
    {
        int egyenleg;

        public int Egyenleg { get => egyenleg; set => egyenleg = value; }
        public Balance(int egyenleg)
        {
            this.egyenleg = egyenleg;
        }


        public override string ToString()
        {
            return this.egyenleg+"";
        }

    }
}
