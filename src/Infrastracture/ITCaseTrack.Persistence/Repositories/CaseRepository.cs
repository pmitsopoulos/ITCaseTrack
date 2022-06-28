using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace ITCaseTrack.Persistence.Repositories
{
    public class CaseRepository : GenericRepository<Case>, ICaseRepository
    {
        private readonly DbContext db;

        public CaseRepository(DbContext db) : base(db)
        {
            this.db = db;
        }
    }
}