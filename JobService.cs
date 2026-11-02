using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JobTracker
{
    public class JobService
    {
        private List<Job> jobs;
        private const string FilePath = "jobs.json";

        public JobService()
        {
            jobs = LoadJobs();
        }

        public void AddJob(Job job)
        {
            jobs.Add(job);
            SaveJobs();
        }

        public List<Job> GetAllJobs()
        {
            return jobs;
        }

        public bool UpdateJobStatus(int index, JobStatus newStatus)
        {
            if (index >= 0 && index < jobs.Count)
            {
                jobs[index].Status = newStatus;
                SaveJobs();
                return true;
            }
            return false;
        }

        public bool DeleteJob(int index)
        {
            if (index >= 0 && index < jobs.Count)
            {
                jobs.RemoveAt(index);
                SaveJobs();
                return true;
            }
            return false;
        }

        public Dictionary<JobStatus, int> GetStatusCounts()
        {
            return jobs.GroupBy(j => j.Status)
                       .ToDictionary(g => g.Key, g => g.Count());
        }

        public List<Job> FilterByStatus(JobStatus status)
        {
            return jobs.Where(j => j.Status == status).ToList();
        }

        private void SaveJobs()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jobs, options);
            File.WriteAllText(FilePath, json);
        }

        private List<Job> LoadJobs()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Job>>(json) ?? new List<Job>();
            }
            return new List<Job>();
        }

        public static bool TryParseStatus(string input, out JobStatus status)
        {
            if (Enum.TryParse(input, true, out status) && Enum.IsDefined(typeof(JobStatus), status))
            {
                return true;
            }
            status = JobStatus.Applied;
            return false;
        }

        public static bool TryParseDate(string input, out DateTime date)
        {
            return DateTime.TryParse(input, out date);
        }
    }
}
