using Ardalis.Specification.EntityFrameworkCore;
using DLaura.DataAcces.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.DataAcces.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IEfRepository<T> where T : class
    {
        private readonly DonaLauraContext _dbContext;
        private IDbContextTransaction? _dbTransaction;

        public EfRepository(DonaLauraContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_dbTransaction != null)
            {
                return;
            }
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_dbTransaction == null)
            {
                return;
            }
            await _dbContext.SaveChangesAsync();
            await _dbTransaction.CommitAsync();
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_dbTransaction != null)
            {
                return;
            }
            await _dbTransaction.RollbackAsync();
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }
    }
}