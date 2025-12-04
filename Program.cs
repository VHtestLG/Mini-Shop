using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Mini_Shop;

namespace Mini_Shop
{
    internal class Program
    {
        static readonly string basePath = @"C:\Users\HryshkoValeriia\source\repos\Mini-Shop\Mini-Shop\";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string adminPfad = Path.Combine(basePath, "admin.json");
            string kundenPfad = Path.Combine(basePath, "kunde.json");
            string produktPfad = Path.Combine(basePath, "produkts.json");

            Benutzer aktuellerBenutzer = null;

            while (true)
            {
                Console.WriteLine("Wer meldet sich an? 1 - Admin, 2 - Kunde");
                string rolle = Console.ReadLine();

                if (rolle == "1") LoginAdmin(adminPfad, ref aktuellerBenutzer);
                else LoginOderRegistrierungKunde(kundenPfad, ref aktuellerBenutzer);

                Console.WriteLine("\nProdukte:\n");
                try
                {
                    var kategorien = JsonSerializer.Deserialize<Dictionary<string, List<Produkt>>>(File.ReadAllText(produktPfad));
                    PrintListeDerProdukte(kategorien);
                }
                catch (Exception ex) { Console.WriteLine($"Fehler beim Lesen der Produkte: {ex.Message}"); }

                Console.WriteLine("Zum Ausloggen 'logout' eingeben oder Enter für Neustart.");
                string befehl = Console.ReadLine();
                if (befehl?.ToLower() == "logout") aktuellerBenutzer = null;
                Console.Clear();
            }
        }

        static void LoginAdmin(string pfad, ref Benutzer benutzer)
        {
            Console.Write("E-Mail: "); string email = Console.ReadLine();
            Console.Write("Passwort: "); string passwort = Console.ReadLine();

            var admins = LadeListe<Admin>(pfad);
            var gefunden = admins.Find(a => a.Email == email && a.Passwort == passwort);

            if (gefunden != null) { benutzer = gefunden; Console.WriteLine("Admin eingeloggt.\n"); }
            else { Console.WriteLine("Falsche Daten! Versuche erneut.\n"); }
        }

        static void LoginOderRegistrierungKunde(string pfad, ref Benutzer benutzer)
        {
            Console.WriteLine("1 - Bereits registriert, 2 - Neuer Kunde");
            string option = Console.ReadLine();
            var kunden = LadeListe<Kunde>(pfad);

            if (option == "1") // Login
            {
                Console.Write("E-Mail: "); string email = Console.ReadLine();
                Console.Write("Passwort: "); string passwort = Console.ReadLine();
                var gefunden = kunden.Find(k => k.Email == email && k.Passwort == passwort);
                if (gefunden != null) { benutzer = gefunden; Console.WriteLine("Kunde eingeloggt.\n"); }
                else { Console.WriteLine("Falsche Daten! Versuche erneut.\n"); }
            }
            else // Registrierung
            {
                Console.Write("Vorname: "); string vorname = Console.ReadLine();
                Console.Write("Nachname: "); string nachname = Console.ReadLine();
                Console.Write("E-Mail: "); string email = Console.ReadLine();
                Console.Write("Passwort: "); string passwort = Console.ReadLine();

                if (kunden.Exists(k => k.Email == email)) { Console.WriteLine("Diese E-Mail ist bereits registriert!\n"); return; }

                string neueId = $"K{kunden.Count + 1}KN";
                var neuerKunde = new Kunde(neueId, vorname, nachname, email, passwort);
                kunden.Add(neuerKunde);
                SpeichereListe(pfad, kunden);

                benutzer = neuerKunde;
                Console.WriteLine("Kunde eingeloggt.\n");
            }
        }

        static void PrintListeDerProdukte(Dictionary<string, List<Produkt>> kategorien)
        {
            foreach (var kategorie in kategorien)
            {
                Console.WriteLine($"Kategorie: {kategorie.Key}");
                Console.WriteLine("{0,-5} | {1,-20} | {2,10}", "ID", "Name", "Preis (€)");
                Console.WriteLine(new string('-', 40));

                foreach (var produkt in kategorie.Value)
                    if (produkt.Sichtbar)
                        Console.WriteLine("{0,-5} | {1,-20} | {2,10:F2}", produkt.ID, produkt.Name, produkt.Preis);

                Console.WriteLine();
            }
        }

        static List<T> LadeListe<T>(string pfad)
        {
            if (!File.Exists(pfad)) return new List<T>();
            string json = File.ReadAllText(pfad);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        static void SpeichereListe<T>(string pfad, List<T> daten)
        {
            File.WriteAllText(pfad, JsonSerializer.Serialize(daten, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}