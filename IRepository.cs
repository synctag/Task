namespace TestApplication.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllData();
        Task<T> GetByIdData(int id);
        Task<T> AddData(T entity);
        Task DeleteData(T entity);
        Task UpdateData(T entity);  

    }
}
