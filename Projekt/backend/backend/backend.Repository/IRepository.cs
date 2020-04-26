﻿using backend.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<bool> Exists(Expression<Func<T, bool>> func);
        Task<T> GetEntity(Expression<Func<T, bool>> func);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetBy(Expression<Func<T, bool>> func);
        Task<List<T>> GetAll();
    }
}
