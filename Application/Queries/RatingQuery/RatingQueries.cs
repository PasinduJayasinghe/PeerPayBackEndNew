using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RatingQuery
{
    public class GetRatingByIdQuery : IRequest<RatingDto?>
    {
        public string RatingId { get; set; }
    }

    public class GetJobRatingsQuery : IRequest<IEnumerable<RatingDto>>
    {
        public string JobId { get; set; }
    }

    public class GetUserRatingsQuery : IRequest<RatingListDto>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetRatingsByRaterQuery : IRequest<IEnumerable<RatingDto>>
    {
        public string RaterId { get; set; }
    }

    public class GetUserRatingStatsQuery : IRequest<RatingStatsDto>
    {
        public string UserId { get; set; }
    }

    public class CheckUserRatedJobQuery : IRequest<bool>
    {
        public string JobId { get; set; }
        public string RaterId { get; set; }
    }
}
