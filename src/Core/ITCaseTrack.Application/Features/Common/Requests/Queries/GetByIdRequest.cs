using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Requests.Queries
{
    public class GetByIdRequest<TRequestEntity> : IRequest<TRequestEntity>
    {
        public string Id { get; set; }   
    }
}