using Application.Results;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<Result> AddAsync(T entity); //bool yerine IDataResult şeklindede döndürrülebilir result true,false  message
        Task<Result> AddRangeAsync(List<T> entities);
        Result Remove(T entity);
        Result RemoveRange(List<T> entities);
        Task<Result> RemoveAsync(string id);
        Result Update(T entity);
        Task<int> SaveAsync();

    }
}
