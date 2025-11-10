using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class JobDto
    {
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string CompanyName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public PayType PayType { get; set; }
        public int DurationDays { get; set; }
        public string[] RequiredSkills { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
        public JobStatus Status { get; set; }
        public string Location { get; set; }
        public JobType JobType { get; set; }
        public int MaxApplicants { get; set; }
        public int ApplicationCount { get; set; }
    }

    public class CreateJobDto
    {
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public PayType PayType { get; set; }
        public int DurationDays { get; set; }
        public string[] RequiredSkills { get; set; }
        public DateTime Deadline { get; set; }
        public string Location { get; set; }
        public JobType JobType { get; set; }
        public int MaxApplicants { get; set; }
    }

    public class UpdateJobDto
    {
        public string JobId { get; set; }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public PayType PayType { get; set; }
        public int DurationDays { get; set; }
        public string[] RequiredSkills { get; set; }
        public DateTime Deadline { get; set; }
        public string Location { get; set; }
        public JobType JobType { get; set; }
        public int MaxApplicants { get; set; }
        public JobStatus Status { get; set; }
    }

    public class JobSearchDto
    {
        public string SearchTerm { get; set; }
        public string Location { get; set; }
        public string CategoryId { get; set; }
        public decimal? MinPay { get; set; }
        public decimal? MaxPay { get; set; }
    }
}
