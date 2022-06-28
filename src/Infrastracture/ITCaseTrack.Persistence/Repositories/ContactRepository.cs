using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace ITCaseTrack.Persistence.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly DbContext db;

        public ContactRepository(DbContext db) : base(db)
        {
            this.db = db;
        }
    }
}