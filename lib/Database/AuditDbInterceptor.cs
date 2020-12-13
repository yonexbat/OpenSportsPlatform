﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Database
{
    public class AuditDbInterceptor : ISaveChangesInterceptor
    {
        private readonly ISecurityService _securityService;
        public AuditDbInterceptor(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            
        }

        public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return result;
        }

        public ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return new ValueTask<int>(Task.FromResult(result));
        }

        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return result;
        }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return new ValueTask<InterceptionResult<int>>(Task.FromResult(result));
        }

        protected void InitTechnicalFields(DbContext dbContext)
        {
            dbContext.ChangeTracker.DetectChanges();
            DateTime now = DateTime.Now;
            string user = _securityService.GetCurrentUserid();

            foreach (EntityEntry entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is IEntity)
                {

                    IEntity entity = (IEntity)entry.Entity;
                    entity.InsertDate = now;
                    entity.UpdateDate = now;
                    entity.InsertUser = user;
                    entity.UpdateUser = user;
                }
            }
            foreach (EntityEntry entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                if (entry.Entity is IEntity)
                {

                    IEntity cranEntity = (IEntity)entry.Entity;
                    cranEntity.UpdateDate = now;
                    cranEntity.UpdateUser = user;
                }
            }
        }
    }
}