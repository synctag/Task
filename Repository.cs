
using Microsoft.EntityFrameworkCore;
using TestApplication.Data;

namespace TestApplication.Repository
{
    public class Repository<T> : IRepository<T> where T : class

    {
        protected readonly DbSet<T> _dbSet;
        private readonly MyDbContext _myDbContext;

        public Repository(MyDbContext myDbContext)
        {
            _dbSet = myDbContext.Set<T>();
            _myDbContext = myDbContext;
        }

        public async Task<T> AddData(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _myDbContext.SaveChangesAsync();  
            return entity;  
        }

        public async Task DeleteData(T entity)
        {
           _dbSet.Remove(entity);
            await _myDbContext.SaveChangesAsync();


        }

        public async Task<IEnumerable<T>> GetAllData()
        {
            return await _dbSet.ToListAsync();  
        }

        public async Task<T> GetByIdData(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateData(T entity)
        {
            _dbSet.Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Modified;
            await _myDbContext.SaveChangesAsync();

        }
    }
}
