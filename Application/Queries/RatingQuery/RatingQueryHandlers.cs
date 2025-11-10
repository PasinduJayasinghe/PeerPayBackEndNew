using Application.Dtos;
using Application.Interfaces;
using Application.Queries.RatingQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RatingQuery
{
    public class GetRatingByIdQueryHandler : IRequestHandler<GetRatingByIdQuery, RatingDto?>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetRatingByIdQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<RatingDto?> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (rating == null)
            {
                return null;
            }

            return new RatingDto
            {
                RatingId = rating.RatingId,
                JobId = rating.JobId,
                JobTitle = rating.Job?.Title ?? "Unknown Job",
                RaterId = rating.RaterId,
                RaterName = rating.Rater?.Name ?? "Unknown",
                RatedUserId = rating.RatedUserId,
                RatedUserName = rating.RatedUser?.Name ?? "Unknown",
                RatingValue = rating.RatingValue,
                Review = rating.Review,
                RatingType = rating.RatingType,
                IsPublic = rating.IsPublic,
                CreatedAt = rating.CreatedAt
            };
        }
    }

    public class GetJobRatingsQueryHandler : IRequestHandler<GetJobRatingsQuery, IEnumerable<RatingDto>>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetJobRatingsQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<RatingDto>> Handle(GetJobRatingsQuery request, CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetByJobIdAsync(request.JobId);

            return ratings.Select(r => new RatingDto
            {
                RatingId = r.RatingId,
                JobId = r.JobId,
                JobTitle = r.Job?.Title ?? "Unknown Job",
                RaterId = r.RaterId,
                RaterName = r.Rater?.Name ?? "Unknown",
                RatedUserId = r.RatedUserId,
                RatedUserName = r.RatedUser?.Name ?? "Unknown",
                RatingValue = r.RatingValue,
                Review = r.Review,
                RatingType = r.RatingType,
                IsPublic = r.IsPublic,
                CreatedAt = r.CreatedAt
            }).ToList();
        }
    }

    public class GetUserRatingsQueryHandler : IRequestHandler<GetUserRatingsQuery, RatingListDto>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetUserRatingsQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<RatingListDto> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetByRatedUserIdAsync(
                request.UserId,
                request.PageNumber,
                request.PageSize
            );

            var averageRating = await _ratingRepository.GetAverageRatingAsync(request.UserId);
            var totalCount = await _ratingRepository.GetTotalRatingsCountAsync(request.UserId);

            var ratingDtos = ratings.Select(r => new RatingDto
            {
                RatingId = r.RatingId,
                JobId = r.JobId,
                JobTitle = r.Job?.Title ?? "Unknown Job",
                RaterId = r.RaterId,
                RaterName = r.Rater?.Name ?? "Unknown",
                RatedUserId = r.RatedUserId,
                RatedUserName = r.RatedUser?.Name ?? "Unknown",
                RatingValue = r.RatingValue,
                Review = r.Review,
                RatingType = r.RatingType,
                IsPublic = r.IsPublic,
                CreatedAt = r.CreatedAt
            }).ToList();

            return new RatingListDto
            {
                Ratings = ratingDtos,
                TotalCount = totalCount,
                AverageRating = averageRating,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }

    public class GetRatingsByRaterQueryHandler : IRequestHandler<GetRatingsByRaterQuery, IEnumerable<RatingDto>>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetRatingsByRaterQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<RatingDto>> Handle(GetRatingsByRaterQuery request, CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetByRaterIdAsync(request.RaterId);

            return ratings.Select(r => new RatingDto
            {
                RatingId = r.RatingId,
                JobId = r.JobId,
                JobTitle = r.Job?.Title ?? "Unknown Job",
                RaterId = r.RaterId,
                RaterName = r.Rater?.Name ?? "Unknown",
                RatedUserId = r.RatedUserId,
                RatedUserName = r.RatedUser?.Name ?? "Unknown",
                RatingValue = r.RatingValue,
                Review = r.Review,
                RatingType = r.RatingType,
                IsPublic = r.IsPublic,
                CreatedAt = r.CreatedAt
            }).ToList();
        }
    }

    public class GetUserRatingStatsQueryHandler : IRequestHandler<GetUserRatingStatsQuery, RatingStatsDto>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;

        public GetUserRatingStatsQueryHandler(
            IRatingRepository ratingRepository,
            IUserRepository userRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
        }

        public async Task<RatingStatsDto> Handle(GetUserRatingStatsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var averageRating = await _ratingRepository.GetAverageRatingAsync(request.UserId);
            var totalRatings = await _ratingRepository.GetTotalRatingsCountAsync(request.UserId);
            var distribution = await _ratingRepository.GetRatingDistributionAsync(request.UserId);
            var recentRatings = await _ratingRepository.GetByRatedUserIdAsync(request.UserId, 1, 5);

            return new RatingStatsDto
            {
                UserId = request.UserId,
                UserName = user.Name,
                AverageRating = averageRating,
                TotalRatings = totalRatings,
                RatingDistribution = distribution,
                RecentRatings = recentRatings.Select(r => new RatingDto
                {
                    RatingId = r.RatingId,
                    JobId = r.JobId,
                    JobTitle = r.Job?.Title ?? "Unknown Job",
                    RaterId = r.RaterId,
                    RaterName = r.Rater?.Name ?? "Unknown",
                    RatedUserId = r.RatedUserId,
                    RatedUserName = r.RatedUser?.Name ?? "Unknown",
                    RatingValue = r.RatingValue,
                    Review = r.Review,
                    RatingType = r.RatingType,
                    IsPublic = r.IsPublic,
                    CreatedAt = r.CreatedAt
                }).ToList()
            };
        }
    }

    public class CheckUserRatedJobQueryHandler : IRequestHandler<CheckUserRatedJobQuery, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public CheckUserRatedJobQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<bool> Handle(CheckUserRatedJobQuery request, CancellationToken cancellationToken)
        {
            return await _ratingRepository.HasUserRatedJobAsync(request.JobId, request.RaterId);
        }
    }
}
