using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class JobManager
    {
        // Applications | List<JobApplication> - Samling av alla ansökningar

        public List<JobApplication> Applications { get; set; } = new List<JobApplication>();

        // Metoder
        // AddJob() – lägger till en ny ansökan
        // UpdateStatus() – ändrar status på en befintlig ansökan
        //ShowAll() – visar alla ansökningar
        //ShowByStatus() – filtrerar med LINQ efter status
        //ShowStatistics() – visar statistik med LINQ(Count, Average, OrderBy, Where) 

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
