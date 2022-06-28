using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Requests.Commands
{
    public class DeleteRequest<TEntity> : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}