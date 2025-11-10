using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobCommand
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public string JobId { get; set; }
        public string EmployerId { get; set; }
    }
}
