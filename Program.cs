using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Mini_Shop;

namespace Mini_Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Legen Sie die UTF-8-Codierung fest, damit das Euro-Zeichen € korrekt angezeigt wird
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                // JSON aus der Datei lesen 
                string json = File.ReadAllText("produkts.json");

                // JSON in ein Wörterbuch deserialisieren, wobei der Schlüssel die Kategorie und der Wert die Produktliste ist
                var kategorien = JsonSerializer.Deserialize<Dictionary<string, List<Produkt>>>(json);

                // Produkte in Konsole ausgeben
                Console.WriteLine("Liste der Produkte:\n");

                foreach (var kategorie in kategorien)
                {
                    Console.WriteLine($"Kategorie: {kategorie.Key}");
                    Console.WriteLine("{0,-5} | {1,-20} | {2,10}", "ID", "Name", "Preis (€)");
                    Console.WriteLine(new string('-', 40));

                    foreach (var produkt in kategorie.Value)
                    {

                        if (produkt.Sichtbar)
                            Console.WriteLine("{0,-5} | {1,-20} | {2,10:F2}", produkt.ID, produkt.Name, produkt.Preis);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fechler: {ex.Message}");
            }
        }
    }
}
