using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Domain;
using ITCaseTrack.Domain.Entities;
using ITCaseTrack.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ITCaseTrack.Persistence
{
    public class DbContext
    {
        public IMongoCollection<Case> Cases { get; }
        public IMongoCollection<Contact> Contacts { get; }
        public IMongoCollection<AppSystem> AppSystems { get; }

        private readonly IMongoDatabase MongoDatabase;
        private readonly IOptions<CaseStudyDbSettings> options;
        public DbContext(IOptions<CaseStudyDbSettings> db_settings)
        {
            this.options = db_settings;
            var dbClient = new MongoClient(db_settings.Value.ConnectionString);

            MongoDatabase = dbClient.GetDatabase(db_settings.Value.Database);
            
            

            Cases = MongoDatabase.GetCollection<Case>(db_settings.Value.CaseCollection);

            AppSystems = MongoDatabase.GetCollection<AppSystem>(db_settings.Value.SystemCollection);

            Contacts = MongoDatabase.GetCollection<Contact>(db_settings.Value.ContactCollection);
        }

        public IMongoCollection<T> GetCollection<T>() where T : BaseDomainEntity 
        {
            if(typeof(T).Equals(typeof(Case)))
            {
                return MongoDatabase.GetCollection<T>(options.Value.CaseCollection);
            }
            else if (typeof(T).Equals(typeof(AppSystem)))
            {
                return MongoDatabase.GetCollection<T>(options.Value.SystemCollection);
            }
            return MongoDatabase.GetCollection<T>(options.Value.ContactCollection);
        }
    }
}