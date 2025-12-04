using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Shop
{
    internal class Benutzer
    {
        public string Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }

        // Konstruktor mit Parametern für die abgeleiteten Klassen
        internal Benutzer(string id, string vorname, string nachname, string email, string passwort)
        {
            Id = id;
            Vorname = vorname;
            Nachname = nachname;
            Email = email;
            Passwort = passwort;

        }
    }

}
