using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Requests.Commands
{
    public class CreateRequest<TCommandEntity> : IRequest<TCommandEntity>
    {
        public TCommandEntity Dto { get; set; }
    }
}