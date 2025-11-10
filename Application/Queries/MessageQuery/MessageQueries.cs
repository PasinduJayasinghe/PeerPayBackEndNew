using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MessageQuery
{
    public class GetConversationMessagesQuery : IRequest<MessageListDto>
    {
        public string ConversationId { get; set; }
        public string UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class GetUnreadMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
        public string UserId { get; set; }
    }

    public class SearchMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
        public string ConversationId { get; set; }
        public string SearchTerm { get; set; }
    }

    public class GetUserConversationsQuery : IRequest<IEnumerable<ConversationDto>>
    {
        public string UserId { get; set; }
    }

    public class GetConversationByIdQuery : IRequest<ConversationDto?>
    {
        public string ConversationId { get; set; }
    }
}
