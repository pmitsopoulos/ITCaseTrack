using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Contracts.Persistence
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        
    }
}