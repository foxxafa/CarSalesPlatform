using Application.Repositories;
using Application.Results;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly CarSalesPlatformDbContext _context;

        public WriteRepository(CarSalesPlatformDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<Result> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return new Result(entityEntry.State == EntityState.Added);
        }

        public async Task<Result> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return new Result(true);
        }

        public Result Remove(T entity)
        {
            EntityEntry<T> entityEntry=Table.Remove(entity);
            return new Result(entityEntry.State == EntityState.Deleted);
        }

        public async Task<Result> RemoveAsync(string id)
        {
            T entity= await Table.SingleOrDefaultAsync(data => data.Id.ToString() == id);
            return Remove(entity);
        }

        public Result RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return new Result(true);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public Result Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return new Result(entityEntry.State == EntityState.Modified);
        }
    }
}
