using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Requests.Commands
{
    public class UpdateRequest<TCommandEntity> : IRequest<Unit>
    {
        public TCommandEntity Dto { get; set; }
    }
}