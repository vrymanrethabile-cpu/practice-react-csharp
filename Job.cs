using System;

namespace JobTracker
{
    public class Job
    {
        public string Company { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public DateTime DateApplied { get; set; }
        public JobStatus Status { get; set; }

        public Job(string company, string role, string location, DateTime dateApplied, JobStatus status)
        {
            Company = company;
            Role = role;
            Location = location;
            DateApplied = dateApplied;
            Status = status;
        }
    }

    public enum JobStatus
    {
        Applied,
        Interview,
        Offer,
        Rejected
    }
}
