using Application.Commands.RatingCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.RatingCommand
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, RatingDto>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;

        public CreateRatingCommandHandler(
            IRatingRepository ratingRepository,
            IUserRepository userRepository,
            IJobRepository jobRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _jobRepository = jobRepository;
        }

        public async Task<RatingDto> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            // Check if user has already rated this job
            var hasRated = await _ratingRepository.HasUserRatedJobAsync(request.JobId, request.RaterId);
            if (hasRated)
            {
                throw new Exception("You have already rated this job");
            }

            // Verify job exists
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            // Verify users exist
            var rater = await _userRepository.GetByIdAsync(request.RaterId);
            var ratedUser = await _userRepository.GetByIdAsync(request.RatedUserId);

            if (rater == null || ratedUser == null)
            {
                throw new Exception("User not found");
            }

            var rating = new Rating
            {
                RatingId = Guid.NewGuid().ToString(),
                JobId = request.JobId,
                RaterId = request.RaterId,
                RatedUserId = request.RatedUserId,
                RatingValue = request.RatingValue,
                Review = request.Review ?? string.Empty,
                RatingType = request.RatingType,
                IsPublic = request.IsPublic
            };

            var createdRating = await _ratingRepository.AddAsync(rating);

            return new RatingDto
            {
                RatingId = createdRating.RatingId,
                JobId = createdRating.JobId,
                JobTitle = job.Title,
                RaterId = createdRating.RaterId,
                RaterName = rater.Name,
                RatedUserId = createdRating.RatedUserId,
                RatedUserName = ratedUser.Name,
                RatingValue = createdRating.RatingValue,
                Review = createdRating.Review,
                RatingType = createdRating.RatingType,
                IsPublic = createdRating.IsPublic,
                CreatedAt = createdRating.CreatedAt
            };
        }
    }

    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (rating == null)
            {
                throw new Exception("Rating not found");
            }

            rating.RatingValue = request.RatingValue;
            rating.Review = request.Review ?? string.Empty;
            rating.IsPublic = request.IsPublic;

            await _ratingRepository.UpdateAsync(rating);
            return true;
        }
    }

    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (rating == null)
            {
                throw new Exception("Rating not found");
            }

            await _ratingRepository.DeleteAsync(request.RatingId);
            return true;
        }
    }
}
