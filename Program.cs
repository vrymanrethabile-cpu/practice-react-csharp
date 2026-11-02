using System;
using System.Linq;

namespace JobTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"\nHello, {name}! Welcome to Job Application Tracker.\n");

            JobService jobService = new JobService();
            bool running = true;

            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddJob(jobService);
                        break;
                    case "2":
                        ListJobs(jobService);
                        break;
                    case "3":
                        UpdateJobStatus(jobService);
                        break;
                    case "4":
                        DeleteJob(jobService);
                        break;
                    case "5":
                        ShowStatusCounts(jobService);
                        break;
                    case "6":
                        FilterByStatus(jobService);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("=== Job Application Tracker ===");
            Console.WriteLine("1. Add job");
            Console.WriteLine("2. List jobs");
            Console.WriteLine("3. Update status");
            Console.WriteLine("4. Delete job");
            Console.WriteLine("5. Show status counts");
            Console.WriteLine("6. Filter by status");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        static void AddJob(JobService jobService)
        {
            Console.WriteLine("\n--- Add New Job ---");

            Console.Write("Company: ");
            string company = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(company))
            {
                Console.Write("Company cannot be empty. Enter company: ");
                company = Console.ReadLine();
            }

            Console.Write("Role: ");
            string role = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(role))
            {
                Console.Write("Role cannot be empty. Enter role: ");
                role = Console.ReadLine();
            }

            Console.Write("Location: ");
            string location = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(location))
            {
                Console.Write("Location cannot be empty. Enter location: ");
                location = Console.ReadLine();
            }

            Console.Write("Date applied (yyyy-MM-dd): ");
            string dateInput = Console.ReadLine();
            DateTime dateApplied;
            while (!JobService.TryParseDate(dateInput, out dateApplied))
            {
                Console.Write("Invalid date format. Use yyyy-MM-dd: ");
                dateInput = Console.ReadLine();
            }

            Console.Write("Status (Applied, Interview, Offer, Rejected): ");
            string statusInput = Console.ReadLine();
            JobStatus status;
            while (!JobService.TryParseStatus(statusInput, out status))
            {
                Console.Write("Invalid status. Enter (Applied, Interview, Offer, Rejected): ");
                statusInput = Console.ReadLine();
            }

            Job job = new Job(company, role, location, dateApplied, status);
            jobService.AddJob(job);
            Console.WriteLine("Job added successfully!\n");
        }

        static void ListJobs(JobService jobService)
        {
            Console.WriteLine("\n--- All Jobs ---");
            var jobs = jobService.GetAllJobs();
            
            if (jobs.Count == 0)
            {
                Console.WriteLine("No jobs found.\n");
                return;
            }

            for (int i = 0; i < jobs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {jobs[i].Company} - {jobs[i].Role}");
                Console.WriteLine($"   Location: {jobs[i].Location}");
                Console.WriteLine($"   Date Applied: {jobs[i].DateApplied:yyyy-MM-dd}");
                Console.WriteLine($"   Status: {jobs[i].Status}\n");
            }
        }

        static void UpdateJobStatus(JobService jobService)
        {
            Console.WriteLine("\n--- Update Job Status ---");
            ListJobs(jobService);

            var jobs = jobService.GetAllJobs();
            if (jobs.Count == 0) return;

            Console.Write("Enter job number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > jobs.Count)
            {
                Console.WriteLine("Invalid job number.\n");
                return;
            }

            Console.Write("New status (Applied, Interview, Offer, Rejected): ");
            string statusInput = Console.ReadLine();
            JobStatus newStatus;
            while (!JobService.TryParseStatus(statusInput, out newStatus))
            {
                Console.Write("Invalid status. Enter (Applied, Interview, Offer, Rejected): ");
                statusInput = Console.ReadLine();
            }

            if (jobService.UpdateJobStatus(index - 1, newStatus))
            {
                Console.WriteLine("Status updated successfully!\n");
            }
            else
            {
                Console.WriteLine("Failed to update status.\n");
            }
        }

        static void DeleteJob(JobService jobService)
        {
            Console.WriteLine("\n--- Delete Job ---");
            ListJobs(jobService);

            var jobs = jobService.GetAllJobs();
            if (jobs.Count == 0) return;

            Console.Write("Enter job number to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > jobs.Count)
            {
                Console.WriteLine("Invalid job number.\n");
                return;
            }

            Console.Write("Are you sure? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                if (jobService.DeleteJob(index - 1))
                {
                    Console.WriteLine("Job deleted successfully!\n");
                }
                else
                {
                    Console.WriteLine("Failed to delete job.\n");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled.\n");
            }
        }

        static void ShowStatusCounts(JobService jobService)
        {
            Console.WriteLine("\n--- Status Counts ---");
            var counts = jobService.GetStatusCounts();

            foreach (var count in counts)
            {
                Console.WriteLine($"{count.Key}: {count.Value}");
            }
            Console.WriteLine($"Total: {jobService.GetAllJobs().Count}\n");
        }

        static void FilterByStatus(JobService jobService)
        {
            Console.WriteLine("\n--- Filter by Status ---");
            Console.Write("Enter status to filter (Applied, Interview, Offer, Rejected): ");
            string statusInput = Console.ReadLine();

            if (!JobService.TryParseStatus(statusInput, out JobStatus status))
            {
                Console.WriteLine("Invalid status.\n");
                return;
            }

            var filteredJobs = jobService.FilterByStatus(status);
            Console.WriteLine($"\nJobs with status {status}: {filteredJobs.Count}\n");

            foreach (var job in filteredJobs)
            {
                Console.WriteLine($"{job.Company} - {job.Role} ({job.Location})");
            }
            Console.WriteLine();
        }
    }
}
