using System;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore_Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
