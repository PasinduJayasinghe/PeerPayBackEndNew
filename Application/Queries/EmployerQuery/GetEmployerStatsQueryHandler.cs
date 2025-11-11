using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.EmployerQuery
{
    public class GetEmployerStatsQueryHandler : IRequestHandler<GetEmployerStatsQuery, EmployerStatsDto>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<GetEmployerStatsQueryHandler> _logger;

        public GetEmployerStatsQueryHandler(
            IJobRepository jobRepository,
            IRatingRepository ratingRepository,
            ILogger<GetEmployerStatsQueryHandler> logger)
        {
            _jobRepository = jobRepository;
            _ratingRepository = ratingRepository;
            _logger = logger;
        }

        public async Task<EmployerStatsDto> Handle(GetEmployerStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting stats for employer: {EmployerId}", request.EmployerId);

                // Get all jobs for this employer
                var jobs = await _jobRepository.GetJobsByEmployerAsync(request.EmployerId);
                var jobsList = jobs.ToList();

                // Calculate stats
                var totalJobs = jobsList.Count;
                var activeJobs = jobsList.Count(j => j.Status == JobStatus.Active);
                var totalApplicants = jobsList.Sum(j => j.Applications?.Count ?? 0);

                // Get rating stats
                var averageRating = await _ratingRepository.GetAverageRatingAsync(request.EmployerId);
                var totalRatings = await _ratingRepository.GetTotalRatingsCountAsync(request.EmployerId);

                return new EmployerStatsDto
                {
                    TotalJobs = totalJobs,
                    ActiveJobs = activeJobs,
                    TotalApplicants = totalApplicants,
                    AverageRating = averageRating > 0 ? (decimal?)averageRating : null,
                    TotalRatings = totalRatings
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting stats for employer: {EmployerId}", request.EmployerId);
                throw;
            }
        }
    }
}
