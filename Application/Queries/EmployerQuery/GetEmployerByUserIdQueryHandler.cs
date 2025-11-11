using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.EmployerQuery
{
    public class GetEmployerByUserIdQueryHandler : IRequestHandler<GetEmployerByUserIdQuery, EmployerDto>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<GetEmployerByUserIdQueryHandler> _logger;

        public GetEmployerByUserIdQueryHandler(
            IEmployerRepository employerRepository,
            ILogger<GetEmployerByUserIdQueryHandler> logger)
        {
            _employerRepository = employerRepository;
            _logger = logger;
        }

        public async Task<EmployerDto> Handle(GetEmployerByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employer for UserId: {UserId}", request.UserId);

                var employer = await _employerRepository.GetByUserIdAsync(request.UserId);

                if (employer == null)
                {
                    _logger.LogWarning("Employer not found for UserId: {UserId}", request.UserId);
                    return null;
                }

                return new EmployerDto
                {
                    EmployerId = employer.EmployerId,
                    UserId = employer.UserId,
                    CompanyName = employer.CompanyName,
                    CompanyType = employer.CompanyType,
                    Description = employer.Description,
                    ContactPerson = employer.ContactPerson,
                    VerificationStatus = employer.VerificationStatus,
                    Rating = employer.Rating,
                    JobsPosted = employer.JobsPosted,
                    User = employer.User != null ? new UserDto
                    {
                        UserId = employer.User.UserId,
                        Email = employer.User.Email,
                        Phone = employer.User.Phone,
                        Name = employer.User.Name,
                        UserType = employer.User.UserType,
                        Status = employer.User.Status,
                        IsVerified = employer.User.IsVerified,
                        CreatedAt = employer.User.CreatedAt,
                        LastLogin = employer.User.LastLogin
                    } : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employer for UserId: {UserId}", request.UserId);
                throw;
            }
        }
    }
}
