using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumFinance
{
    class ConceptParent
    {
        public ConceptParent()
        {
            Console.WriteLine("Szülő konstruktor.");
        }

        public void print()
        {
            Console.WriteLine("Szülő osztály koncepciója.");
        }
    }
}
