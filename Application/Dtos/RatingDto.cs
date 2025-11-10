using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RatingDto
    {
        public string RatingId { get; set; }
        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public string RaterId { get; set; }
        public string RaterName { get; set; }
        public string RatedUserId { get; set; }
        public string RatedUserName { get; set; }
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public RatingType RatingType { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateRatingDto
    {
        public string JobId { get; set; }
        public string RaterId { get; set; }
        public string RatedUserId { get; set; }
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public RatingType RatingType { get; set; }
        public bool IsPublic { get; set; }
    }

    public class UpdateRatingDto
    {
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public bool IsPublic { get; set; }
    }

    public class RatingStatsDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public Dictionary<int, int> RatingDistribution { get; set; }
        public IEnumerable<RatingDto> RecentRatings { get; set; }
    }

    public class RatingListDto
    {
        public IEnumerable<RatingDto> Ratings { get; set; }
        public int TotalCount { get; set; }
        public double AverageRating { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
