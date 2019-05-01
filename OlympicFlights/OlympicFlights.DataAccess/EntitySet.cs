﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlympicFlights.DataAccess
{
    public class EntitySet<TEntity> : EntitySetBase<TEntity>
        where TEntity : class
    {
        public EntitySet(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        internal DbSet<TEntity> DbSet { get; }

        protected override IQueryable<TEntity> Queryable => DbSet;

        public override TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }
    }
}
