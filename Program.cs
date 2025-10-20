using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //Skapa ett nytt C# Console Project.
            //JobApplication – representerar en enskild ansökan.
            //JobManager – ansvarar för att hantera alla ansökningar(listan, logik och LINQ).
            //I Program.cs ska du skapa ett menysystem som körs i en while-loop och använder switch för val.
            //Implementera minst tre LINQ-operationer för att filtrera, sortera eller beräkna data.
            //Programmet ska kunna registrera nya jobb, uppdatera status, filtrera med LINQ, visa statistik och sortera data.
            //Menyval:

            // Lägg till ny ansökan
            //Visa alla ansökningar
            //Filtrera ansökningar efter status(LINQ) 
            //Sortera ansökningar efter datum(OrderBy) 
            //Visa statistik:
            //Totalt antal ansökningar
            //Antal per status
            //Genomsnittlig svarstid

            //Uppdatera status på en ansökan 
            //Ta bort en ansökan
            //Avsluta programmet

            JobManager manager = new JobManager();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Meny:");
                Console.WriteLine("1. Lägg till ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Filtrera ansökningar efter status (LINQ)");
                Console.WriteLine("4. Sortera ansökningar efter datum (OrderBy)");
                Console.WriteLine("5. Visa statistik");
                Console.WriteLine("6. Uppdatera status på en ansökan");
                Console.WriteLine("7. Ta bort en ansökan");
                Console.WriteLine("8. Avsluta programmet");
                Console.Write("Välj ett alternativ (1-8): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Lägg till ny ansökan
                        JobApplication newApp = new JobApplication();
                        Console.Write("Företagsnamn: ");
                        newApp.CompanyName = Console.ReadLine();
                        Console.Write("Positionstitel: ");
                        newApp.PositionTitle = Console.ReadLine();
                        newApp.ApplicationDate = DateTime.Now;
                        newApp.Status = Status.Applied;
                        Console.Write("Löneförväntan (SEK): ");
                        newApp.SalaryExpectation = int.Parse(Console.ReadLine());
                        manager.AddJob(newApp);
                        Console.WriteLine("Ansökan tillagd.");
                        break;
                    case "2":
                        // Visa alla ansökningar
                        manager.ShowAll();
                        break;
                    case "3":
                        // Filtrera ansökningar efter status (LINQ)
                        Console.Write("Ange status att filtrera efter (Applied, Interview, Offer, Rejected): ");
                        string statusFilter = Console.ReadLine();
                        var filteredApps = manager.GetApplications().Where(a => a.Status.ToString().Equals(statusFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                        foreach (var app in filteredApps)
                        {
                            Console.WriteLine(app.GetSummary());
                        }
                        break;
                    case "4":
                        // Sortera ansökningar efter datum (OrderBy)
                        var sortedApps = manager.GetApplications().OrderBy(a => a.ApplicationDate).ToList();
                        foreach (var app in sortedApps)
                        {
                            Console.WriteLine(app.GetSummary());
                        }
                        break;
                    case "5":
                        // Visa statistik
                        manager.ShowStatistics();
                        break;
                    case "6":
                        // Uppdatera status på en ansökan
                        Console.Write("Ange index för ansökan att uppdatera: ");
                        int updateIndex = int.Parse(Console.ReadLine());
                        var appToUpdate = manager.GetJob(updateIndex);
                        Console.WriteLine(appToUpdate.GetSummary());
                        break;
                    case "7":
                        // Ta bort en ansökan
                        Console.Write("Ange index för ansökan att ta bort: ");
                        int removeIndex = int.Parse(Console.ReadLine());
                        var appToRemove = manager.GetJob(removeIndex);
                        if (appToRemove != null)
                        {
                            manager.Applications.RemoveAt(removeIndex);
                            Console.WriteLine("Ansökan borttagen.");
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt index.");
                        }
                        break;
                    case "8":
                        // Avsluta programmet
                        exit = true;
                        break;

                }

                //färger i konsolen

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Red;

            }

        }
    }
}

  