using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Requests.Queries
{
    public class GetBySearchTermRequest<TRequestEntity> : IRequest<List<TRequestEntity>>
    {
        public string SearchTerm { get; set; }
    }
}