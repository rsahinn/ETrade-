﻿using ETrade.Application.Repositories;
using ETrade.Domain.Common;
using ETrade.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETradeAPIDBContext _context;

        public WriteRepository(ETradeAPIDBContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry=await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State==EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return Remove(await Table.FirstOrDefaultAsync(data=> data.Id==id));
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry= Table.Update(entity);
            return entityEntry.State==EntityState.Modified;
        }
        public async Task<int> SaveAsync()
       => await _context.SaveChangesAsync();
    }
}
