using MediatR;

namespace Application.Queries.EmployerQuery
{
    public class GetEmployerStatsQuery : IRequest<EmployerStatsDto>
    {
        public string EmployerId { get; set; }

        public GetEmployerStatsQuery(string employerId)
        {
            EmployerId = employerId;
        }
    }

    public class EmployerStatsDto
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int TotalApplicants { get; set; }
        public decimal? AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }
}
