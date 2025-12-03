using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Shop
{
    internal class Produkt
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public bool Sichtbar { get; set; }
    }
}
