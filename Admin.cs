using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Shop
{
    internal class Admin : Benutzer
    {
        public string Status { get; } = "Admin";

        public Admin(string id, string vorname, string nachname, string email, string passwort)
            : base(id, vorname, nachname, email, passwort)
        {
        }
    }
}
