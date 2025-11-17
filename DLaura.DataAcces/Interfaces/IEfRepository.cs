using Ardalis.Specification;
using System.Linq.Expressions;


namespace DLaura.DataAcces.Interfaces
{
    public interface IEfRepository<T> : IRepositoryBase<T> where T : class
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        
    }
}
