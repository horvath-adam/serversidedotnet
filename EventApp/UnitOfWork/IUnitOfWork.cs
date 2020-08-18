using EventApp.Models;
using EventApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbContext Context();
    }
}
