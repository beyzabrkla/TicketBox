using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketBox.Domain.Interfaces;
using TicketBox.Domain.Specifications;
using TicketBox.Persistance.Context;
using TicketBox.Persistance.Specifications;

namespace TicketBox.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TicketContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(TicketContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //var mı yok mu kontrolü büyük verili projelerde bulunan ilk değerde çalışmayı durdurur
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
