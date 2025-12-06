


namespace moustafapp.Server.GenericOfWork
{
    public interface IBaseRepository <T> where T : class
    {      
        Task <IEnumerable<T>> GetAllAsync();
        Task <T> GetTById(int id);
        Task <bool> GetAnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByWhereTolist(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetByWhereOrderedDescendingAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> OrderByDescending);

        Task<List<T>> GetAllWithIncludes(    
            params Expression<Func<T, object>>[] includes);
        Task <T> GetByIdWithIncludes(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllByPredicateWithIncludes(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>> [] includes  );
        Task <T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        IEnumerable<T> DeleteRange(IEnumerable<T> entities);
        
    }
}
