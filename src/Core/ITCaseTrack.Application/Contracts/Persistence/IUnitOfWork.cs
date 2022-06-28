using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository {get;}
        ICaseRepository CaseRepository {get;}
        IAppSystemRepository AppSystemRepository {get;}

    }
}