using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ITCaseTrack.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseDomainEntity
    {
        private readonly DbContext db;

        private IMongoCollection<T> Collection;


        public GenericRepository(DbContext db)
        {
            this.db = db;
            

           this.Collection = db.GetCollection<T>();

            // var prop = db.GetType()
            // .GetProperties()
            // .FirstOrDefault((x) => x.PropertyType.Equals(typeof(IMongoCollection<T>)));

            // var propVal = prop.GetValue(this.db,null);
            
            // this.GetType().GetProperty("Collection").SetValue(Collection,prop.GetValue(this.db,null));
        }

        public async Task<bool> Exists(string id)
        {
            var res = Collection.Find(x => x.Id == id);
            if(res!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Collection.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
           try
            {
                FilterDefinition<T> result = Builders<T>.Filter.Eq("Id", id);

                return await Collection.Find(result).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<IEnumerable<T>> GetBySearchTermAsync(string searchTerm)
        {
            try
            {
                var properties = typeof(T).GetProperties().Where(p=>p.PropertyType == typeof(string));

                var result = GetAllAsync().Result.Where( x => 
                {
                    var exists = false;
                    foreach(var prop in properties)
                    {
                        exists = prop.GetValue(x).ToString().ToLower().Contains(searchTerm.ToLower());
                        if(exists)
                        {
                            break;
                        }
                    }
                    return exists;
                });   
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

         public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                // var tbd = Builders<T>.Filter.Eq( "Id", id);
                await Collection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                await Collection.ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
        public Task Save()
        {
            throw new NotImplementedException();
        }
       
    }
}