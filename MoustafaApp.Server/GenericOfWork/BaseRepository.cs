
namespace moustafapp.Server.GenericOfWork
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class

    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetTById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }


        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
      

        public async Task<IEnumerable<T>> GetAllByWhereTolist(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<T>> GetByWhereOrderedDescendingAsync(
             Expression<Func<T, bool>> predicate,
             Expression<Func<T, object>> OrderByDescending)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .OrderByDescending(OrderByDescending)
                .ToListAsync();
        }


        public async Task<List<T>> GetAllWithIncludes(
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }


        public async Task<T> GetByIdWithIncludes(
         Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstAsync(predicate);

        }



         public async Task<List<T>> GetAllByPredicateWithIncludes(
         Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>> [] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(predicate).ToListAsync();
        }



        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }



        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }



        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

       public IEnumerable<T> DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return entities;
       }
       
    }
}
