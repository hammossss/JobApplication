#     internal class Program
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
  //Filtrera ansökningar efter status(LINQ) (VG del)
  //Sortera ansökningar efter datum(OrderBy) (VG del)
  //Visa statistik:
  //Totalt antal ansökningar
  //Antal per status(VG del)
  //Genomsnittlig svarstid(VG del)

  //Uppdatera status på en ansökan 
  //Ta bort en ansökan
  //Avsluta programmet

  
  
  Program JobApplication

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
    //Använd färger i konsolen

  Console.ForegroundColor = ConsoleColor.Black;
  Console.BackgroundColor = ConsoleColor.Red;
  
   }
  }
}



  internal class JobManager
    {
        // Applications | List<JobApplication> - Samling av alla ansökningar
        
   public List<JobApplication> Applications { get; set; } = new List<JobApplication>();
        // Metoder
        // AddJob() – lägger till en ny ansökan
        // UpdateStatus() – ändrar status på en befintlig ansökan
        //ShowAll() – visar alla ansökningar
        //ShowByStatus() – filtrerar med LINQ efter status(VG del)
        //ShowStatistics() – visar statistik med LINQ(Count, Average, OrderBy, Where) (VG del)

  public void AddJob(JobApplication application)
        {
            Applications.Add(application);
        }
        public void UpdateStatus(int index, JobApplication newStatus)
        {
            if (index >= 0 && index < Applications.Count)
            {
                Applications[index].Status = newStatus.Status;
            }
        }
        public void ShowAll()
        {
            foreach (var app in Applications)
            {
                Console.WriteLine(app.GetSummary());
            }
        }
        public JobApplication GetJob(int index) {
            if (index >= 0 && index < Applications.Count)
            {
                return Applications[index];
            }
            return null;
        }
        public List<JobApplication> GetApplications() {
            return Applications;
        }
        public JobApplication GetApplication(int index) {
            if (index >= 0 && index < Applications.Count)
            {
                return Applications[index];
            }
            return null;
        }
        public void ShowStatistics()
        {
            Console.WriteLine($"Total Applications: {Applications.Count}");
            var statusGroups = Applications.GroupBy(a => a.Status)
                                           .Select(g => new { Status = g.Key, Count = g.Count() });
            foreach (var group in statusGroups)
            {
                Console.WriteLine($"{group.Status}: {group.Count}");
            }
            var avgResponseTime = Applications.Where(a => a.ResponseDate.HasValue)
                                              .Select(a => (a.ResponseDate.Value - a.ApplicationDate).Days)
                                              .DefaultIfEmpty(0)
                                              .Average();
            Console.WriteLine($"Average Response Time: {avgResponseTime} days");
        }                
   }
}




  public class JobApplication
    {
        public Status Status { get; internal set; }
        //CompanyName | string
        //PositionTitle | string
        //Status | enum - (Applied, Interview, Offer, Rejected)
        //ApplicationDate | DateTime - Datum när ansökan skickades
        //ResponseDate | DateTime? - Datum när svar mottogs
        //SalaryExpectation | int - Önskad lön i kronor

   public string CompanyName { get; set; }
   public string PositionTitle { get; set; }
   public DateTime ApplicationDate { get; set; }
   public DateTime? ResponseDate { get; set; }
   public int SalaryExpectation { get; set; }
        

  // Metoder
        // GetDaysSinceApplied() – returnerar antal dagar sedan ansökan skickades.
        // GetSummary() – returnerar en kort sammanfattning av ansökan.

  public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }
        public string GetSummary()
        {
            return $"{PositionTitle} at {CompanyName}, applied on {ApplicationDate.ToShortDateString()}, status: {Status}, salary expectation: {SalaryExpectation}             SEK";
        }
    }
}












