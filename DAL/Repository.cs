using BLL.DataAccess;
using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entity;

        public Repository(DbSet<T> entity) 
        {
            _entity = entity;
        }

        public void Create(T entity)
        {
            _entity.Add(entity);
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }

        public void Update(T entity)
        {
            _entity.Update(entity);
        }

        public void Create(IEnumerable<T> entity)
        {
            _entity.AddRange(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _entity.RemoveRange(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            _entity.UpdateRange(entity);
        }
    }
}
