using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataAccess
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
