using Application.Dtos;
using MediatR;

namespace Application.Queries.AdminQuery
{
    public class GetAdminDashboardQuery : IRequest<AdminDashboardDto>
    {
        public int RecentItemsCount { get; set; } = 10;
    }
}
