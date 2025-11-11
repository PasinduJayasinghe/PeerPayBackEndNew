using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.JobQuery
{
    public class GetJobByIdQuery : IRequest<JobDto>
    {
        public string JobId { get; set; }
    }

    public class GetActiveJobsQuery : IRequest<IEnumerable<JobDto>>
    {
    }

    public class GetJobsByEmployerQuery : IRequest<IEnumerable<JobDto>>
    {
        public string EmployerId { get; set; }
    }

    public class GetJobsByCategoryQuery : IRequest<IEnumerable<JobDto>>
    {
        public string CategoryId { get; set; }
    }

    public class SearchJobsByLocationQuery : IRequest<IEnumerable<JobDto>>
    {
        public string Location { get; set; }
    }

    public class SearchJobsQuery : IRequest<IEnumerable<JobDto>>
    {
        public string SearchTerm { get; set; }
        public string Location { get; set; }
        public string CategoryId { get; set; }
        public decimal? MinPay { get; set; }
        public decimal? MaxPay { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
