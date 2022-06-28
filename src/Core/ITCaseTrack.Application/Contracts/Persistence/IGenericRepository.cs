using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Application.Contracts.Persistence
{
    public interface IGenericRepository <T> where T: class 
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync (string id);
        Task UpdateAsync (T entity);
        Task<T> CreateAsync (T entity);
        Task <bool> Exists(string id);

        Task <IEnumerable<T>> GetBySearchTermAsync(string searchTerm);

        Task Save();
    }
}